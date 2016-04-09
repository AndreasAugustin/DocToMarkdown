//  *************************************************************
// <copyright file="ParaMarkdownParserTest.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Tests
{
    using System;
    using System.Xml.Linq;

    using DocToMarkdown;

    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Tests for the <see cref="ParaMarkdownNodeParser"/> class.
    /// </summary>
    [TestFixture]
    public class ParaMarkdownParserTest
    {
        #region fields

        private const String NodeString = "<para>Here's how you could make a second paragraph in a description. " +
                                          "<see cref=\"T:GenericMath.LinearAlgebra.Polynomial[T, TStruct]\"/>" +
                                          " for information about output statements.</para>";

        #endregion

        #region properties

        private XElement XmlInput
        {
            get
            {
                return XElement.Parse(NodeString);
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Test for creating a new object.
        /// </summary>
        [Test]
        [Category("Unit test: parser")]
        public void Init_CreateObject_IsNotNull()
        {
            var parserPoolStub = Substitute.For<IParserPool>();

            var obj = new ParaMarkdownNodeParser(parserPoolStub);

            Assert.IsNotNull(obj);
        }

        /// <summary>
        /// Parses the pars input element result equals expected.
        /// </summary>
        [Test]
        [Category("Unit test: parser")]
        public void Parse_ParseInputElement_ResultEqualsExpected()
        {
            var input = this.XmlInput;
            var seeResult = String.Format(
                                "[{0}](#{1})",
                                "T:GenericMath.LinearAlgebra.Polynomial[T, TStruct]",
                                "T:GenericMath.LinearAlgebra.Polynomial[T, TStruct]".ToLower());

            var parserPoolMock = Substitute.For<IParserPool>();
            parserPoolMock.Parse<SeeMarkdownNodeParser>(Arg.Any<XElement>()).Returns(seeResult);

            var parser = new ParaMarkdownNodeParser(parserPoolMock);

            var result = parser.ParseToMarkdown(input);

            var expected = "*Here's how you could make a second paragraph in a description. "
                           + "[T:GenericMath.LinearAlgebra.Polynomial[T, TStruct]](#t:genericmath.linearalgebra.polynomial[t, tstruct]) "
                           + "for information about output statements.*";
           
            StringAssert.AreEqualIgnoringCase(expected, result);
        }

        #endregion
    }
}
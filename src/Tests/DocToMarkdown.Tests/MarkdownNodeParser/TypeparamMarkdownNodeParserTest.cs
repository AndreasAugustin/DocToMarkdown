//  *************************************************************
// <copyright file="TypeparamMarkdownNodeParserTest.cs" company="None">
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

    using DocToMarkdown.Common;

    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Test for the <see cref="TypeparamMarkdownNodeParser"/> class.
    /// </summary>
    [TestFixture]
    public class TypeparamMarkdownNodeParserTest
    {
        #region fields

        private const String NodeString = "<typeparam name=\"T\">The element type of the array</typeparam>";

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
            var environmentStub = Substitute.For<IEnvironment>();

            var obj = new TypeparamMarkdownNodeParser(parserPoolStub, environmentStub);

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
            var parserPoolStub = Substitute.For<IParserPool>();
            var environmentMock = Substitute.For<IEnvironment>();
            environmentMock.NewLine.Returns(Environment.NewLine);

            var parser = new TypeparamMarkdownNodeParser(parserPoolStub, environmentMock);

            var result = parser.ParseToMarkdown(input);

            var expected = String.Format(
                               "> **Type parameter** {0}: {1} {2}",
                               "T",
                               "The element type of the array",
                               environmentMock.NewLine);

            StringAssert.AreEqualIgnoringCase(expected, result);
        }

        #endregion
    }
}
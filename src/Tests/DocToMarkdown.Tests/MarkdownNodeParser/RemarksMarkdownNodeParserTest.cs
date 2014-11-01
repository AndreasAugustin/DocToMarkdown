//  *************************************************************
// <copyright file="RemarksMarkdownNodeParserTest.cs" company="None">
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
    using NUnit.Framework;
    using NSubstitute;

    /// <summary>
    /// Test for the <see cref="RemarksMarkdownNodeParserTest"/>
    /// </summary>
    [TestFixture]
    public class RemarksMarkdownNodeParserTest
    {
        #region fields

        private const String InputString = @"<remarks>You may have some additional information about this class.</remarks>";

        #endregion

        #region properties

        private XElement XmlInput
        {
            get
            {
                return XElement.Parse(InputString);
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Creates new instance and checks if not null.
        /// </summary>
        [Test]
        [Category("Unit test: parser")]
        public void Init_CreateInstance_IsNotNull()
        {
            var environmentStub = Substitute.For<IEnvironment>();
            var parserPoolStub = Substitute.For<IParserPool>();

            var obj = new RemarksMarkdownNodeParser(parserPoolStub, environmentStub);

            Assert.IsNotNull(obj);
        }

        /// <summary>
        /// Parses the parse input element and checks if it equals the expected.
        /// </summary>
        [Test]
        [Category("Unit test: parser")]
        public void Parse_ParseInputElement_EqualsExpected()
        {
            var input = this.XmlInput;

            var environmentMock = Substitute.For<IEnvironment>();
            environmentMock.NewLine.Returns(Environment.NewLine);
            var parserPoolStub = Substitute.For<IParserPool>();

            var parser = new RemarksMarkdownNodeParser(parserPoolStub, environmentMock);

            var result = parser.ParseToMarkdown(input);

            var expected = String.Format(
                               "{0}**Remarks:**{0}>You may have some additional information about this class.{0}",
                               environmentMock.NewLine);

            StringAssert.AreEqualIgnoringCase(expected, result);
        }

        #endregion
    }
}
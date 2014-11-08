//  *************************************************************
// <copyright file="ExceptionMarkdownNodeParserTest.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    using DocToMarkdown.Common;

    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Test for the <see cref="ExceptionMarkdownNodeParser"/> class.
    /// </summary>
    [TestFixture]
    public class ExceptionMarkdownNodeParserTest
    {
        #region fields

        private const String NodeString = "<exception cref=\"System.Exception\">Thrown when...</exception>";

        #endregion

        #region properties

        private XElement XmlInput
        {
            get
            {
                return XElement.Parse(NodeString);
            }
        }

        private IEnumerable<TestCaseData> TestDataSource
        {
            get
            {
                var markdownTypeExpected = String.Format(
                                               ">**Exception:** [{0}](#{0}):{2}> {1}{2}{2}",
                                               "System.Exception",
                                               "Thrown when...",
                                               Environment.NewLine);

                var gitHubFlavoredMArkdownExpected = String.Format(
                                                         ">**Exception:** *{0}*:{2}> {1}{2}{2}",
                                                         "System.Exception",
                                                         "Thrown when...",
                                                         Environment.NewLine);

                yield return new TestCaseData(
                    MarkdownType.Markdown,
                    markdownTypeExpected);

                yield return new TestCaseData(
                    MarkdownType.GithubFlavoredMarkdown,
                    gitHubFlavoredMArkdownExpected);
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Test for creating a new object.
        /// </summary>
        /// <param name="markdownType">The markdown type.</param>
        [Test]
        [Category("Unit test: parser")]
        [TestCase(MarkdownType.GithubFlavoredMarkdown)]
        [TestCase(MarkdownType.Markdown)]
        public void Init_CreateObject_IsNotNull(Int32 markdownType)
        {
            var parserPoolStub = Substitute.For<IParserPool>();
            var environmentStub = Substitute.For<IEnvironment>();

            var obj = new ExceptionMarkdownNodeParser(parserPoolStub, environmentStub, (MarkdownType)markdownType);

            Assert.IsNotNull(obj);
        }

        /// <summary>
        /// Parses the pars input element result equals expected.
        /// </summary>
        /// <param name="markdownType">The markdown type.</param>
        /// <param name="expected">The expected result.</param>
        [Test]
        [Category("Unit test: parser")]
        [TestCaseSource("TestDataSource")]
        public void Parse_ParseInputElement_ResultEqualsExpected(Int32 markdownType, String expected)
        {
            var input = this.XmlInput;
            var parserPoolStub = Substitute.For<IParserPool>();
            var environmentMock = Substitute.For<IEnvironment>();
            environmentMock.NewLine.Returns(Environment.NewLine);

            var parser = new ExceptionMarkdownNodeParser(parserPoolStub, environmentMock, (MarkdownType)markdownType);

            var result = parser.ParseToMarkdown(input);

            StringAssert.AreEqualIgnoringCase(expected, result);
        }

        #endregion
    }
}
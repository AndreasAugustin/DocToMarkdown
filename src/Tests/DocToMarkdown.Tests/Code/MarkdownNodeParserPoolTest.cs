//  *************************************************************
// <copyright file="MarkdownNodeParserPoolTest.cs" company="None">
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
    /// Test for the <see cref="MarkdownNodeParserPool"/> class.
    /// </summary>
    [TestFixture]
    public class MarkdownNodeParserPoolTest
    {
        #region fields

        private const String NodeString = "<doc></doc>";

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
                yield return new TestCaseData(MarkdownType.Markdown);
                yield return new TestCaseData(MarkdownType.GithubFlavoredMarkdown);
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
            var loggerManagerStub = Substitute.For<ILoggerManager>();
            var environmentStub = Substitute.For<IEnvironment>();

            var obj = new MarkdownNodeParserPool(environmentStub, (MarkdownType)markdownType, loggerManagerStub);

            Assert.IsNotNull(obj);
        }

        /// <summary>
        /// Parses the pars input element result equals expected.
        /// </summary>
        /// <param name="markdownType">The markdown type.</param>
        [Test]
        [Category("Unit test: parser")]
        [TestCaseSource("TestDataSource")]
        public void Parse_ParseInputElement_ResultEqualsExpected(Int32 markdownType)
        {
            var input = this.XmlInput;
            var loggerManagerStub = Substitute.For<ILoggerManager>();
            var environmentStub = Substitute.For<IEnvironment>();

            var parser = new MarkdownNodeParserPool(environmentStub, (MarkdownType)markdownType, loggerManagerStub);

            Assert.Throws<KeyNotFoundException>(() => parser.Parse(input));
        }

        #endregion
    }
}
//  *************************************************************
// <copyright file="SeealsoMarkdownNodeParserTest.cs" company="None">
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
    /// Test for the <see cref="SeealsoMarkdownNodeParser"/> class.
    /// </summary>
    [TestFixture]
    public class SeealsoMarkdownNodeParserTest
    {
        #region fields

        private const String InputString = @"<seealso cref=""System.Console.WriteLine(System.String)""/>";

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
            var markdownTypeStub = MarkdownType.GithubFlavoredMarkdown;

            var obj = new SeealsoMarkdownNodeParser(environmentStub, markdownTypeStub);

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

            var markdownType = MarkdownType.GithubFlavoredMarkdown;

            var parser = new SeealsoMarkdownNodeParser(environmentMock, markdownType);

            var result = parser.ParseToMarkdown(input);

            var expected = String.Format(
                               "**System.Console.WriteLine(System.String)**{0}",
                               environmentMock.NewLine);

            StringAssert.AreEqualIgnoringCase(expected, result);
        }

        #endregion
    }
}
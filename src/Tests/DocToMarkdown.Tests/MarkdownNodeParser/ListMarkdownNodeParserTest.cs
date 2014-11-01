//  *************************************************************
// <copyright file="ListMarkdownNodeParserTest.cs" company="None">
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
    /// Test for the <see cref="ListMarkdownNodeParser"/>
    /// </summary>
    [TestFixture]
    public class ListMarkdownNodeParserTest
    {
        #region fields

        private const String ListXMLString = "<list type=\"bullet\"> \n" +
                                             "<item>\n<description>Item 1.</description> " +
                                             "\n</item> \n<item> \n<description>Item 2.</description> \n</item> \n" +
                                             "</list>";

        #endregion

        #region properties

        private XElement XmlElementInput
        {
            get
            {
                return XElement.Parse(ListXMLString); 
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Creates a parser and checks if the object is not null.
        /// </summary>
        [Test]
        [Category("unit test: parser")]
        public void InitParser_CreateObject_CheckForNull()
        {
            var environentStub = Substitute.For<IEnvironment>();
            var parserPoolStub = Substitute.For<IParserPool>();

            var parser = new ListMarkdownNodeParser(environentStub, parserPoolStub, 0);

            Assert.IsNotNull(parser);
        }

        /// <summary>
        /// Tests the parser with a valid <see cref="XElement"/> with list.
        /// </summary>
        [Category("Unit test: paster")]
        [Test]
        public void ParseToMarkdown_ParseListElement_StringEqualsExpected()
        {
            var input = XmlElementInput;

            var environentStub = Substitute.For<IEnvironment>();
            environentStub.NewLine.Returns(Environment.NewLine);
            var parserPoolStub = Substitute.For<IParserPool>();

            var parser = new ListMarkdownNodeParser(environentStub, parserPoolStub, 0);

            var result = parser.ParseToMarkdown(input);

            var expected = String.Format("{0}- Item 1.{0}- Item 2.", environentStub.NewLine);

            Assert.AreEqual(expected, result);
        }

        #endregion
    }
}
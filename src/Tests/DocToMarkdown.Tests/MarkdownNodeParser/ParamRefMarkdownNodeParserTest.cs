//  *************************************************************
// <copyright file="ParamRefMarkdownNodeParserTest.cs" company="None">
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

    using NUnit.Framework;

    /// <summary>
    /// Test for the <see cref="ParamRefMarkdownNodeParser"/> class.
    /// </summary>
    [TestFixture]
    public class ParamRefMarkdownNodeParserTest
    {
        #region fields

        private const String InputString = @"<paramref name=""Int1""/>";

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
            var obj = new ParamRefMarkdownNodeParser();

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

            var parser = new ParamRefMarkdownNodeParser();

            var result = parser.ParseToMarkdown(input);

            const String expected = "*Int1*";

            StringAssert.AreEqualIgnoringCase(expected, result);
        }

        #endregion
    }
}
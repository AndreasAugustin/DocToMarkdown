//  *************************************************************
// <copyright file="TypeparamrefMarkdownNodeParserTest.cs" company="None">
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

    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Test for the <see cref="TypeparamrefMarkdownNodeParser"/>
    /// </summary>
    [TestFixture]
    public class TypeparamrefMarkdownNodeParserTest
    {
        #region fields

        private const String InputString = @"<typeparamref name=""T""/>";

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
            var obj = new TypeparamrefMarkdownNodeParser();

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

            var parser = new TypeparamrefMarkdownNodeParser();

            var result = parser.ParseToMarkdown(input);

            const String Expected = "*T*";

            StringAssert.AreEqualIgnoringCase(Expected, result);
        }

        #endregion
    }
}
//  *************************************************************
// <copyright file="ValueMarkdownNodeParserTest.cs" company="None">
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
    /// Test for the <see cref="ValueMarkdownNodeParser"/> class.
    /// </summary>
    [TestFixture]
    public class ValueMarkdownNodeParserTest
    {
        #region fields

        private const String InputString = @"<value>The Name property gets/sets the value " +
                                           "of the string field, _name.</value>";

        #endregion

        #region properties

        private XElement InputXmlElement
        {
            get{ return XElement.Parse(InputString); }
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

            var obj = new ValueMarkdownNodeParser(environmentStub);

            Assert.IsNotNull(obj);
        }

        /// <summary>
        /// Parses the parse input element and checks if it equals the expected.
        /// </summary>
        [Test]
        [Category("Unit test: parser")]
        public void Parse_ParseInputElement_EqualsExpected()
        {
            var input = this.InputXmlElement;
            var environmentStub = Substitute.For<IEnvironment>();
            environmentStub.NewLine.Returns(Environment.NewLine);

            var parser = new ValueMarkdownNodeParser(environmentStub);

            var result = parser.ParseToMarkdown(input);

            var expected = String.Format(
                               "{0}> **Value:** The Name property gets/sets the value of the string field, _name.{0}",
                               environmentStub.NewLine);

            StringAssert.AreEqualIgnoringCase(expected, result);
        }

        #endregion
    }
}
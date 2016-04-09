//  *************************************************************
// <copyright file="PermissionMarkdownNodeParserTest.cs" company="None">
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
    /// Test for the <see cref="PermissionMarkdownNodeParser"/> class.
    /// </summary>
    [TestFixture]
    public class PermissionMarkdownNodeParserTest
    {
        #region fields

        private const String InputString = @"<permission cref=""System.Security.PermissionSet"">" +
                                           "Everyone can access this method.</permission>";

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

            var obj = new PermissionMarkdownNodeParser(environmentStub);

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

            var parser = new PermissionMarkdownNodeParser(environmentMock);

            var result = parser.ParseToMarkdown(input);

            var expected = String.Format(
                               "{0}**Permision:**>*System.Security.PermissionSet*: Everyone can access this method.{0}",
                               environmentMock.NewLine);

            StringAssert.AreEqualIgnoringCase(expected, result);
        }

        #endregion
    }
}
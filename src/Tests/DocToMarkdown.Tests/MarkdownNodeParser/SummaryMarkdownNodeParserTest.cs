//  *************************************************************
// <copyright file="SummaryMarkdownNodeParserTest.cs" company="None">
//     Copyright (c) 2014 andy. All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Tests
{
    using System;
    using System.Xml.Linq;

    using DocToMarkdown;
    using DocToMarkdown.Common;

    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Test for the <see cref="SummaryMarkdownNodeParser"/> class
    /// </summary>
    [TestFixture]
    public class SummaryMarkdownNodeParserTest
    {
        #region private fields

        private XElement MockElement
        {
            get
            {
                const String Element = @"<summary>
                                Initializes a new instance of the <see cref=""T:GenericMath.LinearAlgebra.Polynomial[T, TStruct]""/> class.
                                </summary>";

                return XElement.Parse(Element);
            }
        }

        #endregion

        /// <summary>
        /// Test for the SummaryMarkdownNodeParser.
        /// Tries to create a new object and checks for null.
        /// </summary>
        [Category("Unit test: parser")]
        [Test]
        public void SummaryMarkdownNodeParser_Instanciating_IsNotNull()
        {
            var xmlParserStub = Substitute.For<IParserPool>();
            var environmentStub = Substitute.For<IEnvironment>();
            environmentStub.NewLine.Returns(Environment.NewLine);

            var summaryMardownNodeParser = new SummaryMarkdownNodeParser(xmlParserStub, environmentStub);

            Assert.IsNotNull(summaryMardownNodeParser);
        }

        /// <summary>
        /// Parses the parse xml element ignoring the see tag and checks if it equals the expected.
        /// </summary>
        [Category("Unit test: parser")]
        [Test]
        public void Parse_ParseXelement_IgnoreSee_EqualsExpected()
        {
            var xmlParserStub = Substitute.For<IParserPool>();
            xmlParserStub.Parse<SeeMarkdownNodeParser>(Arg.Any<XElement>()).Returns(String.Empty);

            var environmentStub = Substitute.For<IEnvironment>();
            environmentStub.NewLine.Returns(Environment.NewLine);

            var summaryMardownNodeParser = new SummaryMarkdownNodeParser(xmlParserStub, environmentStub);

            var element = this.MockElement;

            var result = summaryMardownNodeParser.ParseToMarkdown(element);

            var expected = String.Format(
                               "> Initializes a new instance of the  class.{0}",
                               Environment.NewLine);
            StringAssert.AreEqualIgnoringCase(expected, result);
        }

        /// <summary>
        /// Parses the parse xml element and checks if it equals the expected.
        /// </summary>
        [Category("Unit test: parser")]
        [Test]
        public void Parse_ParseXelement_EqualsExpected()
        {
            var element = this.MockElement;

            var environmentStub = Substitute.For<IEnvironment>();
            environmentStub.NewLine.Returns(Environment.NewLine);

            var seeResult = String.Format(
                                "[{0}](#{1}){2}",
                                "T:GenericMath.LinearAlgebra.Polynomial[T, TStruct]",
                                "T:GenericMath.LinearAlgebra.Polynomial[T, TStruct]".ToLower(),
                                Environment.NewLine);

            var xmlParserStub = Substitute.For<IParserPool>();
            xmlParserStub.Parse<SeeMarkdownNodeParser>(Arg.Any<XElement>()).Returns(seeResult);

            var summaryMardownNodeParser = new SummaryMarkdownNodeParser(xmlParserStub, environmentStub);

            var result = summaryMardownNodeParser.ParseToMarkdown(element);
                       
            var expected = String.Format(
                               "> Initializes a new instance of the [T:GenericMath.LinearAlgebra.Polynomial[T, TStruct]](#t:genericmath.linearalgebra.polynomial[t, tstruct]){0} class.{0}",
                               Environment.NewLine);
            StringAssert.AreEqualIgnoringCase(expected, result);
        }
    }
}
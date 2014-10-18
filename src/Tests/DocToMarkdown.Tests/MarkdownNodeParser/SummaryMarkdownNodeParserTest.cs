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
        #region fields

        private IDependencies _dependenciesFake;

        #endregion

        #region properties

        public IDependencies DependenciesFake
        {
            get
            {
                if (_dependenciesFake != null)
                {
                    return _dependenciesFake;
                }

                _dependenciesFake = Substitute.For<IDependencies>();
                _dependenciesFake.Environment.NewLine.Returns(Environment.NewLine);

                return _dependenciesFake;
            }
        }

        #endregion

        [Category("Parser test")]
        [Test]
        public void SummaryMarkdownNodeParser_Instanciating_IsNotNull()
        {
            var dependenciesStub = this.DependenciesFake;

            var xmlParserStub = Substitute.For<IParserPool>();

            var summaryMardownNodeParser = new SummaryMarkdownNodeParser(xmlParserStub, dependenciesStub);

            Assert.IsNotNull(summaryMardownNodeParser);
        }
    }
}
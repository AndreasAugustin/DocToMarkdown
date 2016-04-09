//  *************************************************************
// <copyright file="ConfigurationAdapterTest.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Common.Tests
{
    using System;
    using System.Xml.Linq;

    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Test for the <see cref="ConfigurationAdapter"/> class.
    /// </summary>
    [TestFixture]
    public class ConfigurationAdapterTest
    {
        #region fields

        private const String ConfigurationString = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
                                                   "<configuration>" +
                                                   "<appSettings>" +
                                                   "<add key=\"xmlSource.folder.path\" value=\"\"/>" +
                                                   "<add key=\"markupTarget.folder.path\" value=\"\"/>" +
                                                   "<add key=\"markdownType\" value=\"0\"/>" +
                                                   "<add key=\"logger.global.threshold\" value=\"7\"></add>" +
                                                   "</appSettings>" +
                                                   "</configuration>";

        #endregion

        #region properties

        private XDocument ConfigurationDocument
        {
            get
            {
                return XDocument.Parse(ConfigurationString);
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Creates a new object and checks if it is not null.
        /// </summary>
        [Test]
        [Category("Unit test: configuration")]
        public void Init_CreateClass_ResultIsNotNull()
        {
            var loggerManagerStub = Substitute.For<ILoggerManager>();
            var configuration = new ConfigurationAdapter(loggerManagerStub, String.Empty);
            Assert.IsNotNull(configuration);
        }

        /// <summary>
        /// Calls an index and checks if the result equals the expected value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="expectedValue">The expected value.</param>
        [Test]
        [Category("Unit test: configuration")]
        [TestCase("markdownType", "0")]
        [TestCase("logger.global.threshold", "7")]
        public void Indexer_GetKey_ResultEqualsExpected(String key, String expectedValue)
        {
            var loggerManagerStub = Substitute.For<ILoggerManager>();
            var configuration = new ConfigurationAdapter(loggerManagerStub, String.Empty);
            configuration.ConfigDocument = this.ConfigurationDocument;

            var result = configuration[key];
            Assert.AreEqual(expectedValue, result);
        }

        #endregion
    }
}
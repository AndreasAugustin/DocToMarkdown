//  *************************************************************
// <copyright file="NLogManagerAdapterTest.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Common.Tests
{
    using System;
    using System.Collections.Generic;

    using DocToMarkdown.Common;

    using NUnit.Framework;

    /// <summary>
    /// Test for the <see cref="NLogManagerAdapter"/>
    /// </summary>
    [TestFixture]
    public class NLogManagerAdapterTest
    {
        #region properties

        private IEnumerable<TestCaseData> TestDataSource
        {
            get
            {
                yield return new TestCaseData(LogLevel.Debug);
                yield return new TestCaseData(LogLevel.Error);
                yield return new TestCaseData(LogLevel.Fatal);
                yield return new TestCaseData(LogLevel.Info);
                yield return new TestCaseData(LogLevel.Off);
                yield return new TestCaseData(LogLevel.Trace);
                yield return new TestCaseData(LogLevel.Warn);
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Tests the class creation.
        /// </summary>
        [Test]
        [Category("Unit test: Logger manager")]
        public void Init_CreateInstance_CheckForNull()
        {
            var logManager = new NLogManagerAdapter();

            Assert.IsNotNull(logManager);

            logManager.ShutDown();
        }

        /// <summary>
        /// Tests the get logger method.
        /// </summary>
        [Test]
        [Category("Unit test: Logger manager")]
        public void GetLogger_CreateInstance_GetLogger_CheckResultForNull()
        {
            var logManager = new NLogManagerAdapter();

            var result = logManager.GetLogger("Test");

            Assert.IsNotNull(result);

            const String ExpectedName = "Test";
            Assert.AreEqual(ExpectedName, result.Name);

            logManager.ShutDown();
        }

        /// <summary>
        /// Tests the global threshold property.
        /// </summary>
        /// <param name="loglevel">The log level.</param>
        [Test]
        [Category("Unit test: Logger manager")]
        [TestCaseSource("TestDataSource")]
        public void GlobalThreshold_SetGlobalThreshold_ValueEqualsExpected(LogLevel loglevel)
        {
            var input = loglevel;
            var logManager = new NLogManagerAdapter();

            logManager.GlobalThreshold = input;
            var result = logManager.GlobalThreshold;

            var expected = loglevel;

            Assert.AreEqual(expected, result);

            logManager.ShutDown();
        }

        #endregion
    }
}
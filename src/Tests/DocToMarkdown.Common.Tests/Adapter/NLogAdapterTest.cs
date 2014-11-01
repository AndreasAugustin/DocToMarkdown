//  *************************************************************
// <copyright file="NLogAdapterTest.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Tests
{
    using System;
    using System.IO;

    using DocToMarkdown.Common;

    using NUnit.Framework;

    /// <summary>
    /// Test for the <see cref="NLogAdapter"/> class.
    /// This is an integration test.
    /// </summary>
    [TestFixture]
    public class NLogAdapterTest
    {
        #region fields

        private NLogManagerAdapter _loggerManagerMock;

        #endregion

        #region properties

        /// <summary>
        /// Because the constructor in the Logger class of NLog is internal and the 
        /// constructor of <see cref="NLogAdapter"/> class needs an instance of Logger,
        /// I am not able to mock it the natural way.
        /// Therefore I need the LoggerManagerMock to get an instance of a <see cref="NLogAdapter"/> class. 
        /// </summary>
        /// <value>The logger manager mock.</value>
        private NLogManagerAdapter LoggerManagerMock
        {
            get { return this._loggerManagerMock ?? (this._loggerManagerMock = new NLogManagerAdapter()); }
        }

        #endregion

        #region methods

        [Test]
        [Category("Integration test: Logger")]
        public void Info_LogMessage_LoggedMessageEqualsExpected()
        {
            var logger = GetLogger();

            const String logMessage = "Information";

            logger.Info(logMessage);

            var environmentPath = Environment.CurrentDirectory;
            var logDirectoryPath = Path.Combine(environmentPath, "logs");
            var logFileAbsolutePath = Path.Combine(logDirectoryPath, "test.log");

            try
            {
                var lines = File.ReadAllLines(@logFileAbsolutePath);
                Assert.IsNotNull(lines);
                Assert.IsTrue(lines.Length > 0);

                var expected = String.Format("INFO {0}", logMessage);
                Assert.AreEqual(expected, lines[0]);
            }
            finally
            {
                Directory.Delete(logDirectoryPath, true);
            }
        }

        #endregion

        #region helper methods

        private NLogAdapter GetLogger()
        {
            var logger = LoggerManagerMock.GetLogger("Test") as NLogAdapter;

            Assert.IsNotNull(logger);

            return logger;
        }

        #endregion
    }
}
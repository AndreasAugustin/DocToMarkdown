//  *************************************************************
// <copyright file="LogLevelConverterExtensionTest.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Common.Tests
{
    using System.Collections.Generic;

    using NUnit.Framework;

    /// <summary>
    /// Test for the <see cref="LogLevelConverterExtension"/>
    /// </summary>
    [TestFixture]
    public class LogLevelConverterExtensionTest
    {
        #region properties

        private IEnumerable<TestCaseData> TestDataSource
        {
            get
            {
                yield return new TestCaseData(LogLevel.Debug, NLog.LogLevel.Debug);
                yield return new TestCaseData(LogLevel.Error, NLog.LogLevel.Error);
                yield return new TestCaseData(LogLevel.Fatal, NLog.LogLevel.Fatal);
                yield return new TestCaseData(LogLevel.Info, NLog.LogLevel.Info);
                yield return new TestCaseData(LogLevel.Off, NLog.LogLevel.Off);
                yield return new TestCaseData(LogLevel.Trace, NLog.LogLevel.Trace);
                yield return new TestCaseData(LogLevel.Warn, NLog.LogLevel.Warn);
            }
        }

        #endregion

        /// <summary>
        /// Converts from <see cref="LogLevel"/> to NLog.LogLevel. The converted result should equal the expected.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="expected">The expected.</param>
        [Test]
        [Category("Unit test: nlog")]
        [TestCaseSource("TestDataSource")]
        public void ConvertFromLogLevel_Convert_ResultEqualsExpected(LogLevel input, NLog.LogLevel expected)
        {
            var result = input.ConvertToNLogLogLevel();

            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Converts from NLog.LogLevel to <see cref="LogLevel"/>. The converted result should equal the expected.
        /// </summary>
        /// <param name="expected">The expected.</param>
        /// <param name="input">The input.</param>
        [Test]
        [Category("Unit test: nlog")]
        [TestCaseSource("TestDataSource")]
        public void ConvertFromNLogLevel_Convert_ResultEqualsExpected(LogLevel expected, NLog.LogLevel input)
        {
            var result = input.ConvertFromLogLevel();

            Assert.AreEqual(expected, result);
        }
    }
}
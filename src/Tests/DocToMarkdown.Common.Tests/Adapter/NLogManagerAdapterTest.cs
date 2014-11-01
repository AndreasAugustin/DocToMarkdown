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
    using DocToMarkdown.Common;

    using NUnit.Framework;

    /// <summary>
    /// Test for the <see cref="NLogManagerAdapter"/>
    /// </summary>
    [TestFixture]
    public class NLogManagerAdapterTest
    {
        /// <summary>
        /// Tests the class creation.
        /// </summary>
        [Test]
        [Category("Unit test: Logger manager")]
        public void Init_CreateInstance_CheckForNull()
        {
            var logManager = new NLogManagerAdapter();

            Assert.IsNotNull(logManager);
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
        }
    }
}
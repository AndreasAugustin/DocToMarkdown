//  *************************************************************
// <copyright file="ILoggerManager.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Common
{
    using System;

    /// <summary>
    /// Interface for the logger manager
    /// </summary>
    public interface ILoggerManager : IDisposable
    {
        #region properties

        /// <summary>
        /// Gets or sets the global threshold.
        /// Loglevels below this threshold are not logged.
        /// </summary>
        /// <value>The global threshold.</value>
        LogLevel GlobalThreshold { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Gets the specified logger.
        /// </summary>
        /// <returns>The logger.</returns>
        /// <param name="loggerName">The logger name.</param>
        ILogger GetLogger(String loggerName);

        /// <summary>
        /// Disables the logging.
        /// </summary>
        /// <returns>The logging.</returns>
        IDisposable DisableLogging();

        /// <summary>
        /// Enables the logging.
        /// </summary>
        void EnableLogging();

        /// <summary>
        /// Determines whether this instance is logging enabled.
        /// </summary>
        /// <returns><c>true</c> if this instance is logging enabled; otherwise, <c>false</c>.</returns>
        Boolean IsLoggingEnabled();

        /// <summary>
        /// Dispose all targets and shut down.
        /// </summary>
        void ShutDown();

        #endregion
    }
}
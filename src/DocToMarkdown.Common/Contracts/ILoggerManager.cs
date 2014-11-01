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
    public interface ILoggerManager
    {
        #region methods

        /// <summary>
        /// Gets the specified logger.
        /// </summary>
        /// <returns>The logger.</returns>
        /// <param name="loggerName">The logger name.</param>
        ILogger GetLogger(String loggerName);

        #endregion
    }
}
//  *************************************************************
// <copyright file="NLogManagerAdapter.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Common
{
    using System;
    using System.Collections.Generic;

    using NLog;

    /// <summary>
    /// Adapter for the <see cref="LogManager"/>
    /// </summary>
    public sealed class NLogManagerAdapter : ILoggerManager
    {
        #region fields

        private Dictionary<String, ILogger> _loggerPool = new Dictionary<String, ILogger>();

        #endregion

        #region methods

        /// <summary>
        /// Gets the specified logger.
        /// </summary>
        /// <returns>The logger.</returns>
        /// <param name="loggerName">The logger name.</param>
        public ILogger GetLogger(String loggerName)
        {
            if (!this._loggerPool.ContainsKey(loggerName))
            {
                var logger = LogManager.GetLogger(loggerName);
                this._loggerPool.Add(loggerName, new NLogAdapter(logger));
            }

            return this._loggerPool[loggerName];
        }

        #endregion
    }
}
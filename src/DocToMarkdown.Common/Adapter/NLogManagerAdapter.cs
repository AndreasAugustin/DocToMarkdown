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
    public sealed class NLogManagerAdapter : ILoggerManager, IDisposable
    {
        #region fields

        private Dictionary<String, ILogger> _loggerPool = new Dictionary<String, ILogger>();

        private Boolean _alreadyDisposed = false;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the global threshold.
        /// Loglevels below this threshold are not logged.
        /// </summary>
        /// <value>The global threshold.</value>
        public LogLevel GlobalThreshold
        {
            get
            {
                return LogManager.GlobalThreshold.ConvertFromLogLevel();
            }
            set
            {
                LogManager.GlobalThreshold = value.ConvertToNLogLogLevel();
            }
        }

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

        /// <summary>
        /// Disables the logging.
        /// </summary>
        /// <returns>The logging.</returns>
        public IDisposable DisableLogging()
        {   
            return LogManager.DisableLogging();
        }

        /// <summary>
        /// Enables the logging.
        /// </summary>
        public void EnableLogging()
        {
            LogManager.EnableLogging();
        }

        /// <summary>
        /// Determines whether this instance is logging enabled.
        /// </summary>
        /// <returns><c>true</c> if this instance is logging enabled; otherwise, <c>false</c>.</returns>
        public Boolean IsLoggingEnabled()
        {
            return LogManager.IsLoggingEnabled();
        }

        /// <summary>
        /// Dispose all targets and shut down.
        /// </summary>
        public void ShutDown()
        {
            LogManager.Shutdown();
        }

        /// <summary>
        /// Releases all resource used by the <see cref="DocToMarkdown.Common.NLogManagerAdapter"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="DocToMarkdown.Common.NLogManagerAdapter"/>. The <see cref="Dispose"/> method leaves the
        /// <see cref="DocToMarkdown.Common.NLogManagerAdapter"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the
        /// <see cref="DocToMarkdown.Common.NLogManagerAdapter"/> so the garbage collector can reclaim the memory that
        /// the <see cref="DocToMarkdown.Common.NLogManagerAdapter"/> was occupying.</remarks>
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="DocToMarkdown.Common.NLogManagerAdapter"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="DocToMarkdown.Common.NLogManagerAdapter"/>.</returns>
        public override String ToString()
        {
            return String.Format("[NLogManagerAdapter]");
        }

        #endregion

        #region helper methods

        private void Dispose(Boolean isDisposing)
        {
            if (this._alreadyDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                var disposable = LogManager.DisableLogging();
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            this._alreadyDisposed = true;
        }

        #endregion
    }
}
//  *************************************************************
// <copyright file="NLogAdapter.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Common
{
    using System;

    using NLog;

    /// <summary>
    /// Adapter for the <see cref="LogManager"/>
    /// </summary>
    internal sealed class NLogAdapter : ILogger
    {
        #region fields

        private readonly Logger _logger;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.Common.NLogAdapter"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public NLogAdapter(Logger logger)
        {
            this._logger = logger;
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public String Name
        {
            get { return this._logger.Name; }
        }

        #endregion

        #region ILogger implementation

        /// <summary>
        /// Trace with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Trace(String message)
        {
            this._logger.Trace(message);
        }

        /// <summary>
        /// Debug with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(String message)
        {
            this._logger.Debug(message);
        }

        /// <summary>
        /// Info with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(String message)
        {
            this._logger.Info(message);
        }

        /// <summary>
        /// Warn with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(String message)
        {
            this._logger.Warn(message);
        }

        /// <summary>
        /// Error with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(String message)
        {
            this._logger.Error(message);
        }

        /// <summary>
        /// Fatal with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Fatal(String message)
        {
            this._logger.Fatal(message);
        }

        #endregion
    }
}
//  *************************************************************
// <copyright file="ILogger.cs" company="None">
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
    /// Interface for loggers.
    /// </summary>
    public interface ILogger
    {
        #region properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        String Name { get; }

        #endregion

        #region methods

        /// <summary>
        /// Trace with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Trace(String message);

        /// <summary>
        /// Debug with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(String message);

        /// <summary>
        /// Info with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(String message);

        /// <summary>
        /// Warn with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warn(String message);

        /// <summary>
        /// Error with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(String message);

        /// <summary>
        /// Fatal with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Fatal(String message);

        #endregion
    }
}
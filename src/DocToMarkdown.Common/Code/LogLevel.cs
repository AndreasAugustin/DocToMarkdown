//  *************************************************************
// <copyright file="LogLevel.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Common
{
    /// <summary>
    /// Different log levels for the <see cref="ILogger"/>.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// The logging is off
        /// </summary>
        Off = 0,

        /// <summary>
        /// Fatal log level.
        /// </summary>
        Fatal = 1,

        /// <summary>
        /// Error log level.
        /// </summary>
        Error = 2,

        /// <summary>
        /// Warn log level.
        /// </summary>
        Warn = 3,

        /// <summary>
        /// Info log level.
        /// </summary>
        Info = 4,

        /// <summary>
        /// Debug log level.
        /// </summary>
        Debug = 5,

        /// <summary>
        /// Trace log level.
        /// </summary>
        Trace = 6
    }
}
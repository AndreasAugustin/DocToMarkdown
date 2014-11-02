//  *************************************************************
// <copyright file="LogLevelToNLogLogLevelExtension.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Common
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension method to convert <see cref="LogLevel"/> to the NLog.LogLevel.
    /// </summary>
    public static class LogLevelConverterExtension
    {
        #region fields

        private static Dictionary<LogLevel, NLog.LogLevel> _logLevelMappingDictionary;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes the <see cref="DocToMarkdown.Common.LogLevelToNLogLogLevelExtension"/> class.
        /// </summary>
        static LogLevelConverterExtension()
        {
            InitLogLevelMappingDict();
        }

        #endregion

        #region methods

        /// <summary>
        /// Converts the <see cref="LogLevel"/>to the NLog.LogLevel.
        /// </summary>
        /// <returns>The NLog.LogLevel.</returns>
        /// <param name="loglevel">Loglevel.</param>
        public static NLog.LogLevel ConvertToNLogLogLevel(this LogLevel loglevel)
        {
            CheckLogLevelMappingDictForKey(loglevel);

            return _logLevelMappingDictionary[loglevel];
        }

        /// <summary>
        /// Converts from NLog.LogLevel to <see cref="LogLevel"/>.
        /// </summary>
        /// <returns>The from log level.</returns>
        /// <param name="nloglevel">Nloglevel.</param>
        public static LogLevel ConvertFromLogLevel(this NLog.LogLevel nloglevel)
        {
            CheckLogLevelMappingDictForValue(nloglevel);

            return _logLevelMappingDictionary.FirstOrDefault(v => v.Value == nloglevel).Key;
        }

        #endregion

        #region helper methods

        private static void CheckLogLevelMappingDictForValue(NLog.LogLevel nloglevel)
        {
            if (!_logLevelMappingDictionary.ContainsValue(nloglevel))
            {
                throw new KeyNotFoundException(nloglevel.ToString());
            }
        }

        private static void CheckLogLevelMappingDictForKey(LogLevel loglevel)
        {
            if (!_logLevelMappingDictionary.ContainsKey(loglevel))
            {
                throw new KeyNotFoundException(loglevel.ToString());
            }
        }

        private static void InitLogLevelMappingDict()
        {
            _logLevelMappingDictionary = new Dictionary<LogLevel, NLog.LogLevel>();
            _logLevelMappingDictionary.Add(LogLevel.Off, NLog.LogLevel.Off);
            _logLevelMappingDictionary.Add(LogLevel.Fatal, NLog.LogLevel.Fatal);
            _logLevelMappingDictionary.Add(LogLevel.Error, NLog.LogLevel.Error);
            _logLevelMappingDictionary.Add(LogLevel.Warn, NLog.LogLevel.Warn);
            _logLevelMappingDictionary.Add(LogLevel.Info, NLog.LogLevel.Info);
            _logLevelMappingDictionary.Add(LogLevel.Debug, NLog.LogLevel.Debug);
            _logLevelMappingDictionary.Add(LogLevel.Trace, NLog.LogLevel.Trace);
        }

        #endregion
    }
}
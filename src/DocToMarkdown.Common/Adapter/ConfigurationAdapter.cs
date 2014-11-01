//  *************************************************************
// <copyright file="ConfigurationAdapter.cs" company="None">
//     Copyright (c) 2014 andy. All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Common
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Adapter for the <see cref="ConfigurationManager"/> class to get the application data.
    /// </summary>
    public class ConfigurationAdapter : IConfiguration
    {
        #region fields

        private ILogger _logger;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.Common.ConfigurationAdapter"/> class.
        /// </summary>
        /// <param name="loggerManager">Logger manager.</param>
        public ConfigurationAdapter(ILoggerManager loggerManager)
        {
            this._logger = loggerManager.GetLogger("Configuration");
        }

        #endregion

        #region indexers

        /// <summary>
        /// Gets the <see cref="ConfigurationManager.AppSettings"/> with the specified key.
        /// </summary>
        /// <param name="key">The key in the <see href="App.config"/>App.config file.</param>
        /// <returns>The configuration entry.</returns>
        public String this[String key]
        {
            get
            {
                var value = ConfigurationManager.AppSettings[key];

                if (String.IsNullOrEmpty(value))
                {
                    var errorMessage = String.Format(
                                           "The value for {0} in the configuration is not set.",
                                           key);

                    this._logger.Error(String.Format(errorMessage));

                    throw new ConfigurationErrorsException(errorMessage);
                }

                this._logger.Info(String.Format("Configuration for key: {0} valid with value: {1}", key, value));

                return value;
            }
        }

        #endregion
    }
}
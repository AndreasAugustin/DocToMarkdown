//  *************************************************************
// <copyright file="ConfigurationAdapter.cs" company="None">
//     Copyright (c) 2014 andy. All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Adapter for the <see cref="ConfigurationManager"/> class to get the application data.
    /// </summary>
    public class ConfigurationAdapter : IConfiguration
    {
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
                    throw new ConfigurationErrorsException(String.Format(
                            "The value for {0} in the configuration is not set.",
                            key));
                }

                return value;
            }
        }

        #endregion
    }
}
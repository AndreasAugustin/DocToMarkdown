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
                return ConfigurationManager.AppSettings[key];
            }
        }

        #endregion
    }
}
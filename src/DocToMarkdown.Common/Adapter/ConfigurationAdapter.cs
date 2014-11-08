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
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Adapter for getting values from the App.config file.
    /// </summary>
    public class ConfigurationAdapter : IConfiguration
    {
        #region fields

        private const String ConfigFileName = "App.config";
        private String _baseDirectory;
        private ILogger _logger;
        private XDocument _configDocument;
        private XElement _configurationElement;
        private XElement _appSettingsElement;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.Common.ConfigurationAdapter"/> class.
        /// </summary>
        /// <param name="loggerManager">Logger manager.</param>
        /// <param name = "baseDirectory">The base directory for the App.config file.</param>
        public ConfigurationAdapter(ILoggerManager loggerManager, String baseDirectory)
        {
            this._logger = loggerManager.GetLogger("Configuration");
            this._baseDirectory = baseDirectory;
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the config document.
        /// Made internal for dependency injection out of testing purposes.
        /// </summary>
        /// <value>The config document.</value>
        internal XDocument ConfigDocument
        {
            get
            {
                return this._configDocument ?? (this._configDocument = XDocument.Load(Path.Combine(
                        this._baseDirectory,
                        ConfigFileName)));
            }

            set
            {
                this._configDocument = value;
            }
        }

        private XElement ConfigurationElement
        {
            get
            {
                return this._configurationElement ?? (this._configurationElement = this.ConfigDocument.Element("configuration"));
            }
        }

        private XElement AppSettingsElement
        {
            get
            {
                if (this._appSettingsElement != null)
                {
                    return this._appSettingsElement;
                }

                this._appSettingsElement = this.ConfigurationElement.Element("appSettings");

                this.CheckDistinctAppSettingKeys();

                return this._appSettingsElement;
            }
        }

        #endregion

        #region indexers

        /// <summary>
        /// Gets the <see cref="ConfigurationManager.AppSettings"/> with the specified key.
        /// </summary>
        /// <param name="key">The key in the <see href="App.config"/>App.config file.</param>
        /// <returns>The configuration entry.</returns>
        /// <exception cref="ConfigurationErrorsException">Thrown when the configuration value 
        /// for the given key is null or empty.</exception>
        public String this[String key]
        {
            get
            {
                var element = this.AppSettingsElement.Elements().FirstOrDefault(e => e.Attribute("key").Value == key);

                if (element == null)
                {
                    var errorMessage = String.Format("The key {0} was not found in the config file", key);
                }

                var value = element.Attribute("value").Value;
                if (String.IsNullOrEmpty(value))
                {
                    var errorMessage = String.Format(
                                           "The value for {0} in the configuration is not set.",
                                           key);

                    this._logger.Error(String.Format(errorMessage));

                    throw new ApplicationException(errorMessage);
                }

                this._logger.Info(String.Format("Configuration for key: {0} valid with value: {1}", key, value));

                return value;
            }
        }

        #endregion

        #region helper methods

        private void CheckDistinctAppSettingKeys()
        {
            var appSettingKeys = this.AppSettingsElement.Elements("add").Select(e => e.Attribute("key").Value).ToList();

            if (appSettingKeys.Count() != appSettingKeys.Distinct().Count())
            {
                const String Message = "The keys in the application file are not distinct";
                this._logger.Fatal(Message);

                throw new ApplicationException(Message);
            }
        }

        #endregion
    }
}
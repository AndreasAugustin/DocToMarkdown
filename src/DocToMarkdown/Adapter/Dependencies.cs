//  *************************************************************
// <copyright file="Dependencies.cs" company="None">
//     Copyright (c) 2014 andy.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown
{
    using System;

    /// <summary>
    /// Contains the dependencies for the application.
    /// </summary>
    internal class Dependencies : IDependencies
    {
        #region fields

        private IConfiguration _configuration;

        private IEnvironment _environment;

        #endregion

        #region IDependencies implementation

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration
        {
            get
            {
                return this._configuration ?? (this._configuration = new ConfigurationAdapter());
            }
        }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <value>The environment.</value>
        public IEnvironment Environment
        {
            get
            {
                return this._environment ?? (this._environment = new EnvironmentAdapter());
            }
        }

        #endregion
    }
}
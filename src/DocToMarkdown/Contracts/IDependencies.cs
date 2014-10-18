//  *************************************************************
// <copyright file="IDependencies.cs" company="None">
//     Copyright (c) 2014 andy.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown
{
    /// <summary>
    /// Interface for the dependencies for the application.
    /// </summary>
    internal interface IDependencies
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <value>The environment.</value>
        IEnvironment Environment { get; }
    }
}
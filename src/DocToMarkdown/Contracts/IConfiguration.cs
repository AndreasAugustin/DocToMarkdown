//  *************************************************************
// <copyright file="IConfiguration.cs" company="None">
//     Copyright (c) 2014 andy. All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown
{
    using System;

    /// <summary>
    /// Interface for the configuration.
    /// </summary>
    public interface IConfiguration
    {
        #region indexers

        /// <summary>
        /// Gets the <see cref="IConfiguration"/> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The configuration entry.</returns>
        String this[String key] { get; }

        #endregion
    }
}
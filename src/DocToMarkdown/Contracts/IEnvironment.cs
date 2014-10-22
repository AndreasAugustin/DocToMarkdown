//  *************************************************************
// <copyright file="IEnvironment.cs" company="None">
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
    /// Interface for environment.
    /// </summary>
    public interface IEnvironment
    {
        /// <summary>
        /// Gets the new line.
        /// </summary>
        /// <value>The new line.</value>
        String NewLine { get; }
    }
}
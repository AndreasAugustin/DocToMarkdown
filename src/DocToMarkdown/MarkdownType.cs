//  *************************************************************
// <copyright file="MarkdownType.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown
{
    using System.ComponentModel;

    /// <summary>
    /// Gives information which markdown type is needed.
    /// </summary>
    internal enum MarkdownType
    {
        /// <summary>
        /// The github flavored markdown.
        /// </summary>
        [Description("Github flavored markdown")]
        GithubFlavoredMarkdown,
              
        /// <summary>
        /// The markdown.
        /// </summary>
        [Description("Standard markdown")]
        Markdown,

        /// <summary>
        /// The markdown classic.
        /// </summary>
        [Description("Classic markdown")]
        MarkdownClassic,

        /// <summary>
        /// The markdown extra.
        /// </summary>
        [Description("Extra markdown")]
        MarkdownExtra
    }
}
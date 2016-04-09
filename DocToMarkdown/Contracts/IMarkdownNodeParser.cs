//  *************************************************************
// <copyright file="IMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown
{
    using System;
    using System.Xml.Linq;

    /// <summary>
    /// Interface for markdown nodes.
    /// </summary>
    internal interface IMarkdownNodeParser
    {
        /// <summary>
        /// Parses to markdown.
        /// The <paramref name="element"/> is the element to parse.
        /// </summary>
        /// <returns>The parsed markdown.</returns>
        /// <param name="element">The element.</param>
        String ParseToMarkdown(XElement element);
    }
}
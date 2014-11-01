//  *************************************************************
// <copyright file="ListMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
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
    /// List markdown node parser.
    /// </summary>
    /// <example>
    /// For using the <c>list</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/y3ww3c7e.aspx"/>
    /// </example>
    internal class ListMarkdownNodeParser : IMarkdownNodeParser
    {
        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.ListMarkdownNodeParser"/> class.
        /// </summary>
        internal ListMarkdownNodeParser()
        {
            // Nothing to do
        }

        #endregion

        #region methods

        /// <summary>
        /// Parses to markdown.
        /// </summary>
        /// <returns>The parsed markdown.</returns>
        /// <param name="element">The element.</param>
        public String ParseToMarkdown(XElement element)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
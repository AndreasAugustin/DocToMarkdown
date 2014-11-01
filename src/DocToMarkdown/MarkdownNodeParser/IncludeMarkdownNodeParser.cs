//  *************************************************************
// <copyright file="IncludeMarkdownNodeParser.cs" company="None">
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
    /// Include markdown node parser.
    /// </summary>
    /// <example>
    /// For using the <c>include</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/9h8dy30z.aspx"/>
    /// </example>
    internal class IncludeMarkdownNodeParser : IMarkdownNodeParser
    {
        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.IncludeMarkdownNodeParser"/> class.
        /// </summary>
        internal IncludeMarkdownNodeParser()
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
        public string ParseToMarkdown(XElement element)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
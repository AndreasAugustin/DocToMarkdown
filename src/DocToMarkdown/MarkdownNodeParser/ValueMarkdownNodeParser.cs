//  *************************************************************
// <copyright file="ValueMarkdownNodeParser.cs" company="None">
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
    /// Value markdown node parser.
    /// </summary>
    /// <example>
    /// For using the <c>value</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/azda5z79.aspx"/>
    /// </example>
    internal class ValueMarkdownNodeParser : IMarkdownNodeParser
    {
        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.ValueMarkdownNodeParser"/> class.
        /// </summary>
        internal ValueMarkdownNodeParser()
        {
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
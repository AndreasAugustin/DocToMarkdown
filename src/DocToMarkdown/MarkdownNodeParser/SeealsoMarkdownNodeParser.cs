﻿//  *************************************************************
// <copyright file="SeealsoMarkdownNodeParser.cs" company="None">
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
    /// See also markdown node parser.
    /// </summary>
    /// <example>
    /// For using the <c>seealso</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/xhd7ehkk.aspx"/>
    /// </example>
    internal class SeealsoMarkdownNodeParser : IMarkdownNodeParser
    {
        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="SeealsoMarkdownNodeParser"/> class.
        /// </summary>
        internal SeealsoMarkdownNodeParser()
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
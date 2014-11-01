//  *************************************************************
// <copyright file="ParamRefMarkdownNodeParser.cs" company="None">
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
    /// Parameter reference markdown node parser.
    /// </summary>
    /// <example>
    /// For using the <c>paramref</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/wb7x2fhw.aspx"/>
    /// </example>
    internal class ParamRefMarkdownNodeParser : IMarkdownNodeParser
    {
        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.ParamRefMarkdownNodeParser"/> class.
        /// </summary>
        internal ParamRefMarkdownNodeParser()
        {
            // Nothing to do
        }

        #endregion

        #region methods

        /// <summary>
        /// Parses to markdown.
        /// The <paramref name="element"/> is the element to parse.
        /// </summary>
        /// <returns>The parsed markdown.</returns>
        /// <param name="element">The element.</param>
        public String ParseToMarkdown(XElement element)
        {
            if (element.Name != "paramref")
            {
                return null;
            }

            var name = element.Attribute("name").Value;

            return String.Format("*{0}*", name);
        }

        #endregion
    }
}
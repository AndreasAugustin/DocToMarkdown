//  *************************************************************
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using DocToMarkdown.Common;

    /// <summary>
    /// See also markdown node parser.
    /// </summary>
    /// <example>
    /// For using the <c>seealso</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/xhd7ehkk.aspx"/>
    /// </example>
    internal class SeealsoMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private readonly Dictionary<String, String> _templateDictionary = new Dictionary<String, String>();

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="SeealsoMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="markdownType">The markdown type.</param>
        internal SeealsoMarkdownNodeParser(IEnvironment environment, MarkdownType markdownType)
        {
            this.InitTemplate(environment, markdownType);
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
            if (element.Name != "seealso")
            {
                return null;
            }

            var anchorAttr = element.Attribute("cref");
            var key = anchorAttr == null ? "href" : "cref";

            anchorAttr = anchorAttr ?? element.Attribute("href");

            if (anchorAttr == null)
            {
                return null;
            }

            var template = this._templateDictionary[key];

            var val = anchorAttr.Value;

            return String.Format(
                template,
                val,
                val.ToLower());
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment, MarkdownType markdownType)
        {
            if (this._templateDictionary.Any())
            {
                return;
            }

            var hyperRefTemp = markdownType == MarkdownType.GithubFlavoredMarkdown ?
                String.Format(
                                   "{0}{1}",
                                   "{0}",
                                   environment.NewLine) :
                String.Format(
                                   "[{0}](#{1}){2}",
                                   "{0}",
                                   "{1}",
                                   environment.NewLine);

            this._templateDictionary.Add("href", hyperRefTemp);

            var classRefTemp = markdownType == MarkdownType.GithubFlavoredMarkdown ? 
                String.Format(
                                   "**{0}**{1}",
                                   "{0}",
                                   environment.NewLine)
                : String.Format(
                                   "[{0}](#{1}){2}",
                                   "{0}",
                                   "{1}",
                                   environment.NewLine);

            this._templateDictionary.Add("cref", classRefTemp);
        }

        #endregion
    }
}
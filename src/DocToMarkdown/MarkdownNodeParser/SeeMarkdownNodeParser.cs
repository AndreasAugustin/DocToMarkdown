//  *************************************************************
// <copyright file="SeeMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy. All rights reserved.
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
    /// Parser for the see tag.
    /// </summary>
    internal class SeeMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private readonly Dictionary<String, String> _templateDictionary = new Dictionary<String, String>();

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="SeeMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        internal SeeMarkdownNodeParser(IEnvironment environment)
        {
            this.InitTemplate(environment);
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
            if (element.Name != "see")
            {
                return String.Empty;
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

        private void InitTemplate(IEnvironment environment)
        {
            if (this._templateDictionary.Any())
            {
                return;
            }

            this._templateDictionary.Add(
                "href",
                String.Format(
                    "See href: [[{0}|{1}]]{2}",
                    "{0}",
                    "{1}",
                    environment.NewLine));
            this._templateDictionary.Add(
                "cref",
                String.Format(
                    "See cref: [{0}](#{1}){2}",
                    "{0}",
                    "{1}",
                    environment.NewLine));
        }

        #endregion
    }
}
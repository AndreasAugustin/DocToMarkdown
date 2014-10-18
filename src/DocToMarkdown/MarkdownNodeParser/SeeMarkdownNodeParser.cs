//  *************************************************************
// <copyright file="SeeMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy. 
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

    /// <summary>
    /// Parser for the see tag.
    /// </summary>
    internal class SeeMarkdownNodeParser : AbstractMarkdownNodeParser, IMarkdownNodeParser
    {
        #region fields

        private static readonly Dictionary<String, String> TemplateDictionary = new Dictionary<String, String>();

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="SeeMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="environment">The environment.</param>
        internal SeeMarkdownNodeParser(ParseXmlToMarkdown parser, IEnvironment environment)
            : base(parser, environment)
        {
            this.InitTemplate();
        }

        #endregion

        #region methods

        /// <summary>
        /// Parses to markdown.
        /// </summary>
        /// <returns>The parsed markdown.</returns>
        /// <param name="element">The element.</param>
        public override String ParseToMarkdown(XElement element)
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

            var template = TemplateDictionary[key];

            var val = anchorAttr.Value;

            return String.Format(
                template,
                val, val.ToLower());
        }

        #endregion

        #region helper methods

        private void InitTemplate()
        {
            if (TemplateDictionary.Any())
            {
                return;
            }

            TemplateDictionary.Add("href", "[[{0}|{1}]]");
            TemplateDictionary.Add("cref", "[{0}](#{1})");
        }

        #endregion
    }
}
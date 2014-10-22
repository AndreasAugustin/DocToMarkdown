//  *************************************************************
// <copyright file="SummaryMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy. All rights reserved.
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
    /// Parser for the summary tag.
    /// </summary>
    internal class SummaryMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private static String _template;
        private IParserPool _parserPool;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parserPool">The parser pool.</param>
        /// <param name="environment">The environment.</param>
        internal SummaryMarkdownNodeParser(IParserPool parserPool, IEnvironment environment)
        {
            this._parserPool = parserPool;
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
            if (element.Name != "summary")
            {
                return String.Empty;
            }
                
            var seeElements = element.Elements("see");

            foreach (var seeElement in seeElements)
            {
                var parsedSee = this._parserPool.Parse<SeeMarkdownNodeParser>(seeElement);

                seeElement.SetValue(parsedSee);
            }

            return String.Format(
                _template,
                element.Value.Trim());
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment)
        {
            if (!String.IsNullOrEmpty(_template))
            {
                return;
            }

            _template = String.Format("> {0}{1}", "{0}", environment.NewLine);
        }

        #endregion
    }
}
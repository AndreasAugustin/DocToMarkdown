//  *************************************************************
// <copyright file="ExceptionMarkdownNodeParser.cs" company="None">
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
    /// Parses <see cref="XNode"/> node to Markdown String.
    /// </summary>
    internal sealed class ExceptionMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private static String template;
        private IParserPool _parserPool;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parserPool">The parser pool.</param>
        /// <param name="environment">The environment.</param>
        public ExceptionMarkdownNodeParser(IParserPool parserPool, IEnvironment environment)
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
            if (element.Name != "exception")
            {
                return String.Empty;
            }

            // TODO add anchors
            var reference = element.Attribute("cref");

            var elements = element.Elements();
                       
            return String.Format(
                template,
                reference.Value,
                element.Value);
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment)
        {
            if (!String.IsNullOrEmpty(template))
            {
                return;
            }

            template = String.Format("> **Exception:** [{0}](#{0}):{2}> {1}{2}{2}", "{0}", "{1}", environment.NewLine);
        }

        #endregion
    }
}
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

    using DocToMarkdown.Common;

    /// <summary>
    /// Parses <see cref="XNode"/> node to Markdown String.
    /// </summary>
    /// <example>
    /// For using the <c>exception</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/9w4cf933.aspx"/>
    /// </example>
    internal sealed class ExceptionMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private String _template;
        private IParserPool _parserPool;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parserPool">The parser pool.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="markdownType">The markdown type.</param>
        public ExceptionMarkdownNodeParser(IParserPool parserPool, IEnvironment environment, MarkdownType markdownType)
        {
            this._parserPool = parserPool;
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
            if (element.Name != "exception")
            {
                return null;
            }
                
            var reference = element.Attribute("cref");

            var elements = element.Elements();
                       
            return String.Format(
                this._template,
                reference.Value,
                element.Value);
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment, MarkdownType markdownType)
        {
            this._template = markdownType == MarkdownType.GithubFlavoredMarkdown ?
                String.Format(">**Exception:** *{0}*:{2}> {1}{2}{2}", "{0}", "{1}", environment.NewLine)
                : String.Format(">**Exception:** [{0}](#{0}):{2}> {1}{2}{2}", "{0}", "{1}", environment.NewLine);
        }

        #endregion
    }
}
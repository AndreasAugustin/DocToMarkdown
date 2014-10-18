//  *************************************************************
// <copyright file="SummaryMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy. 
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown
{
    using System;
    using System.Text;
    using System.Xml.Linq;

    /// <summary>
    /// Parser for the summary tag.
    /// </summary>
    internal class SummaryMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private static String template;
        private ParseXmlToMarkdown _parser;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="dependencies">The dependency injected parts.</param>
        internal SummaryMarkdownNodeParser(ParseXmlToMarkdown parser, IDependencies dependencies)
        {
            this._parser = parser;
            this.InitTemplate(dependencies.Environment);
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
                
            var elements = element.Elements();
            var stringBuilder = new StringBuilder();

            foreach (var el in elements)
            {
                stringBuilder.Append(this._parser.Parse(el));
            }

            return String.Format(
                template,
                element.Value,
                stringBuilder.ToString());
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment)
        {
            if (!String.IsNullOrEmpty(template))
            {
                return;
            }

            template = String.Format("{0}{2}{1}{2}", "{0}", "{1}", environment.NewLine);
        }

        #endregion
    }
}
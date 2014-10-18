//  *************************************************************
// <copyright file="ExceptionMarkdownNodeParser.cs" company="None">
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
    /// Parses <see cref="XNode"/> node to Markdown String.
    /// </summary>
    internal sealed class ExceptionMarkdownNodeParser : AbstractMarkdownNodeParser, IMarkdownNodeParser
    {
        #region fields

        private static String template;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name = "environment">The environment.</param>
        public ExceptionMarkdownNodeParser(ParseXmlToMarkdown parser, IEnvironment environment)
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
            if (element.Name != "exception")
            {
                return String.Empty;
            }

            // TODO add anchors
            var reference = element.Attribute("cref");

            var elements = element.Elements();
            var stringBuilder = new StringBuilder();

            foreach (var el in elements)
            {
                stringBuilder.Append(this.Parser.Parse(el));
            }

            return String.Format(
                template,
                element.Value,
                reference.Value,
                stringBuilder.ToString());
        }

        #endregion

        #region helper methods

        private void InitTemplate()
        {
            if (!String.IsNullOrEmpty(template))
            {
                return;
            }

            template = String.Format("[[{0}|{0}]]: {1}{2}{2}", "{0}", "{1}", this.Environment.NewLine);
        }

        #endregion
    }
}
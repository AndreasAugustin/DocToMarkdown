//  *************************************************************
// <copyright file="DocMarkdownNodeParser.cs" company="None">
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
    using System.Text;
    using System.Xml.Linq;

    /// <summary>
    /// Document markdown node parser.
    /// </summary>
    internal sealed class DocMarkdownNodeParser : AbstractMarkdownNodeParser, IMarkdownNodeParser
    {
        #region fields

        private static String template;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="dependencies">The dependency injected parts.</param>
        public DocMarkdownNodeParser(ParseXmlToMarkdown parser, IDependencies dependencies)
            : base(parser, dependencies)
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
            if (element.Name != "doc")
            {
                return String.Empty;
            }

            var assemblyName = element.Element("assembly").Element("name").Value;
            var members = element.Element("members").Elements("member");

            var stringBuilder = new StringBuilder();

            foreach (var member in members)
            {
                stringBuilder.Append(this.Parser.Parse(member));
            }

            return String.Format(
                template,               
                assemblyName,
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

            template = String.Format(
                "## {0} ##{2}{2}{1}{2}{2}",
                "{0}",
                "{1}",
                this.Environment.NewLine);
        }

        #endregion
    }
}
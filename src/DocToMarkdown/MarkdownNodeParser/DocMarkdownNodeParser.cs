//  *************************************************************
// <copyright file="DocMarkdownNodeParser.cs" company="None">
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
    using System.Text;
    using System.Xml.Linq;

    /// <summary>
    /// Document markdown node parser.
    /// </summary>
    internal sealed class DocMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private static String _template;
        private IParserPool _parserPool;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parserPool">The parser pool.</param>
        /// <param name="dependencies">The dependency injected parts.</param>
        public DocMarkdownNodeParser(IParserPool parserPool, IDependencies dependencies)
        {
            this._parserPool = parserPool;
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
            if (element.Name != "doc")
            {
                return String.Empty;
            }

            var assemblyName = element.Element("assembly").Element("name").Value;
            var members = element.Element("members").Elements("member");

            var stringBuilder = new StringBuilder();

            foreach (var member in members)
            {
                stringBuilder.Append(this._parserPool.Parse(member));
            }

            return String.Format(
                _template,               
                assemblyName,
                stringBuilder.ToString());
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment)
        {
            if (!String.IsNullOrEmpty(_template))
            {
                return;
            }

            _template = String.Format(
                "## {0} ##{2}{2}{1}{2}{2}",
                "{0}",
                "{1}",
                environment.NewLine);
        }

        #endregion
    }
}
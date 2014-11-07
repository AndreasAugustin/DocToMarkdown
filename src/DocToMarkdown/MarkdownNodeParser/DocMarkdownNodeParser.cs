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

    using DocToMarkdown.Common;

    /// <summary>
    /// Document markdown node parser.
    /// </summary>
    internal sealed class DocMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private String _template;
        private IParserPool _parserPool;
        private ILogger _logger;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parserPool">The parser pool.</param>
        /// <param name="environment">The environment.</param>
        /// <param name = "logger">The logger.</param>
        public DocMarkdownNodeParser(IParserPool parserPool, IEnvironment environment, ILogger logger)
        {
            this._logger = logger;
            this._parserPool = parserPool;
            this.InitTemplate(environment);
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
            if (element.Name != "doc")
            {
                return String.Empty;
            }

            var assemblyElement = element.Element("assembly");
               
            if (assemblyElement == null)
            {
                const String errorMessage = "assembly element not found";
                this._logger.Error(errorMessage);
                throw new KeyNotFoundException(errorMessage);
            }

            var assemblyName = assemblyElement.Element("name").Value;
            var membersElement = element.Element("members");

            if (membersElement == null)
            {
                const String errorMessage = "members element not found";
                this._logger.Error(errorMessage);
                throw new KeyNotFoundException(errorMessage);
            }

            var members = membersElement.Elements("member");

            var stringBuilder = new StringBuilder();

            foreach (var member in members)
            {
                stringBuilder.Append(this._parserPool.Parse(member));
            }

            return String.Format(
                this._template,               
                assemblyName,
                stringBuilder.ToString());
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment)
        {
            this._template = String.Format(
                "{2}--- {2}## Assembly: {0} ##{2}{2}{1}{2}{2}",
                "{0}",
                "{1}",
                environment.NewLine);
        }

        #endregion
    }
}
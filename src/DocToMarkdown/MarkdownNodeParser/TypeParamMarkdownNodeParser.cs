//  *************************************************************
// <copyright file="TypeParamMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
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

    using DocToMarkdown.Common;

    /// <summary>
    /// Type parameter markdown node parser.
    /// </summary>
    public class TypeParamMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private String _template;
        private readonly IParserPool _parserPool;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.TypeParamMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name = "parserPool">The parser pool.</param>
        /// <param name="environment">The environment.</param>
        public TypeParamMarkdownNodeParser(IParserPool parserPool, IEnvironment environment)
        {
            this.InitTemplate(environment);
            this._parserPool = parserPool;
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
            if (element.Name != "typeparam")
            {
                return null;
            }

            var elements = element.Elements();
            var stringBuilder = new StringBuilder();

            var description = element.Value;
            stringBuilder.Append(description);

            foreach (var el in elements)
            {
                stringBuilder.Append(this._parserPool.Parse(el));
            }

            var name = element.Attribute("name").Value;

            return String.Format(
                this._template,
                name,
                stringBuilder.ToString());
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment)
        {
            this._template = String.Format(
                "\tType parameter {0}: {1} {2}",
                "{0}",
                "{1}",
                environment.NewLine);
        }

        #endregion
    }
}
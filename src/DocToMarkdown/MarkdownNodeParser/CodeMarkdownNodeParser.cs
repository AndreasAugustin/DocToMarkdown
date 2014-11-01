//  *************************************************************
// <copyright file="CodeMarkdownNodeParser.cs" company="None">
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
    /// Parser for the code tags.
    /// </summary>
    /// <example>
    /// For using the <c>code</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/f8hahtxf.aspx"/>
    /// </example>
    internal class CodeMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private readonly IParserPool _parserPool;
        private String _template;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.CodeMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name = "parserPool">The parser pool.</param>
        /// <param name="environment">The environment.</param>
        internal CodeMarkdownNodeParser(IParserPool parserPool, IEnvironment environment)
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
        public string ParseToMarkdown(XElement element)
        {
            if (element.Name != "code")
            {
                return String.Empty;
            }

            var elements = element.Elements();
            var stringBuilder = new StringBuilder();

            foreach (var el in elements)
            {
                stringBuilder.Append(this._parserPool.Parse(el));
            }

            return String.Format(
                this._template,
                element.Value,
                stringBuilder.ToString());
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment)
        {
            this._template = String.Format("\tCode: {0}{1}{1}", "{0}", environment.NewLine);
        }

        #endregion
    }
}
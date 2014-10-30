//  *************************************************************
// <copyright file="CMarkdownNodeParser.cs" company="None">
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

    /// <summary>
    /// C markdown node parser.
    /// </summary>
    /// <example>
    /// For how to use the <c>c</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/te6h7cxs.aspx"/>
    /// </example>
    internal class CMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private readonly IParserPool _parserPool;
        private String _template;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.CMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parserPool">The parser pool.</param>
        /// <param name="environment">The environment.</param>
        internal CMarkdownNodeParser(IParserPool parserPool, IEnvironment environment)
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
            if (element.Name != "c")
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
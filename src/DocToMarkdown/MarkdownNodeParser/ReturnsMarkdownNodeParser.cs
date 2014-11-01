//  *************************************************************
// <copyright file="ReturnsMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;

    using DocToMarkdown.Common;

    /// <summary>
    /// Returns markdown node parser.
    /// </summary>
    /// <example>
    /// For using the <c>permission</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/4dcfdeds.aspx"/>
    /// </example> 
    internal class ReturnsMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private String _template;
        private IParserPool _parserPool;

        #endregion

        #region ctors

        public ReturnsMarkdownNodeParser(IParserPool parserPool, IEnvironment environment)
        {
            this.InitTemplate(environment);
            this._parserPool = parserPool;
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
            if (element.Name != "returns")
            {
                return String.Empty;
            }

            var elements = element.Elements();
            var stringBuilder = new StringBuilder();

            foreach (var el in elements.Where(e => e.Name != "paramref"))
            {
                stringBuilder.Append(this._parserPool.Parse(el));
            }

            var paramRefElements = element.Elements("paramref");

            foreach (var paramRefElement in paramRefElements)
            {
                var parsedParamRef = this._parserPool.Parse<ParamRefMarkdownNodeParser>(paramRefElement);
                if (parsedParamRef != null)
                {
                    paramRefElement.SetValue(parsedParamRef);
                }
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
            this._template = String.Format("> **Returns**: {0}{1}{1}", "{0}", environment.NewLine);
        }

        #endregion
    }
}
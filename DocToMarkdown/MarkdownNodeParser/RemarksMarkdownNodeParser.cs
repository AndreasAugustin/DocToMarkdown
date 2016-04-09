//  *************************************************************
// <copyright file="RemarksMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy. All rights reserved. 
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
    /// Remarks markdown node parser.
    /// </summary>
    /// <example>
    /// For using the <c>permission</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/3zw4z1ys.aspx"/>
    /// </example>
    internal class RemarksMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private readonly IParserPool _parserPool;
        private String _template;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="RemarksMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parserPool">The parser pool.</param>
        /// <param name="environment">The environment.</param>
        public RemarksMarkdownNodeParser(IParserPool parserPool, IEnvironment environment)
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
            if (element.Name != "remarks")
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
                var parsedParamRef = this._parserPool.Parse<ParamrefMarkdownNodeParser>(paramRefElement);
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
            this._template = String.Format("{1}**Remarks:**{1}>{0}{1}", "{0}", environment.NewLine);
        }

        #endregion
    }
}
﻿//  *************************************************************
// <copyright file="SummaryMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy. All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown
{
    using System;
    using System.Xml.Linq;

    using DocToMarkdown.Common;

    /// <summary>
    /// Parser for the summary tag.
    /// </summary>
    /// <example>
    /// For using the <c>seealso</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/2d6dt3kf.aspx"/>
    /// </example>
    internal class SummaryMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private String _template;
        private IParserPool _parserPool;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parserPool">The parser pool.</param>
        /// <param name="environment">The environment.</param>
        internal SummaryMarkdownNodeParser(IParserPool parserPool, IEnvironment environment)
        {
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
            if (element.Name != "summary")
            {
                return String.Empty;
            }
                
            var seeElements = element.Elements("see");

            foreach (var seeElement in seeElements)
            {
                var parsedSee = this._parserPool.Parse<SeeMarkdownNodeParser>(seeElement);
                if (parsedSee != null)
                {
                    seeElement.SetValue(parsedSee);
                }
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
                element.Value.Trim());
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment)
        {
            this._template = String.Format("> {0}{1}", "{0}", environment.NewLine);
        }

        #endregion
    }
}
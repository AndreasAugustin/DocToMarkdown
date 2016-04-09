//  *************************************************************
// <copyright file="TypeparamMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
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
    /// Type parameter markdown node parser.
    /// </summary>
    /// <example>
    /// For using the <c>seealso</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/ms173191.aspx"/>
    /// </example>
    internal class TypeparamMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private readonly IParserPool _parserPool;
        private String _template;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeparamMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name = "parserPool">The parser pool.</param>
        /// <param name="environment">The environment.</param>
        internal TypeparamMarkdownNodeParser(IParserPool parserPool, IEnvironment environment)
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
            if(element.Name != "typeparam")
            {
                return null;
            }

            var seeElements = element.Elements("see");

            foreach(var seeElement in seeElements)
            {
                var parsedSee = this._parserPool.Parse<SeeMarkdownNodeParser>(seeElement);

                seeElement.SetValue(parsedSee);
            }

            var name = element.Attribute("name").Value;

            return String.Format(
                this._template,
                name,
                element.Value);
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment)
        {
            this._template = String.Format(
                "> **Type parameter** {0}: {1} {2}",
                "{0}",
                "{1}",
                environment.NewLine);
        }

        #endregion
    }
}
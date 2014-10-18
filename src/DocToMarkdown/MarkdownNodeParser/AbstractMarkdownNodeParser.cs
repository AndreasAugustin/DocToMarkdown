//  *************************************************************
// <copyright file="AbstractMarkdownNodeParser.cs" company="None">
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
    using System.Xml.Linq;

    /// <summary>
    /// Abstract markdown node parser.
    /// </summary>
    internal abstract class AbstractMarkdownNodeParser : IMarkdownNodeParser
    {
        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="environment">The dependency to environment.</param>
        internal AbstractMarkdownNodeParser(ParseXmlToMarkdown parser, IEnvironment environment)
        {
            this.Environment = environment;
            this.Parser = parser;
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets the parser dictionary.
        /// </summary>
        /// <value>The parser dictionary.</value>
        protected ParseXmlToMarkdown Parser
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <value>The environment.</value>
        protected IEnvironment Environment
        {
            get;
            private set;
        }

        #endregion

        #region methods

        /// <summary>
        /// Parses to markdown.
        /// </summary>
        /// <returns>The parsed markdown.</returns>
        /// <param name="element">The element.</param>
        public abstract String ParseToMarkdown(XElement element);

        #endregion
    }
}
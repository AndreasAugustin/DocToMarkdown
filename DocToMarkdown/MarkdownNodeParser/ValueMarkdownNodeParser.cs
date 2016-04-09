//  *************************************************************
// <copyright file="ValueMarkdownNodeParser.cs" company="None">
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
    /// Value markdown node parser.
    /// </summary>
    /// <example>
    /// For using the <c>value</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/azda5z79.aspx"/>
    /// </example>
    internal class ValueMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private String _template;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.ValueMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        internal ValueMarkdownNodeParser(IEnvironment environment)
        {
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
            if (element.Name != "value")
            {
                return null;
            }

            return String.Format(this._template, element.Value);
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment)
        {
            this._template = String.Format("{0}> **Value:** {1}{0}", environment.NewLine, "{0}");
        }

        #endregion
    }
}
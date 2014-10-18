//  *************************************************************
// <copyright file="ParseXmlToMarkdown.cs" company="None">
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
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;

    /// <summary>
    /// Parse xml to markdown.
    /// </summary>
    internal sealed class ParseXmlToMarkdown
    {
        #region fields

        private List<IMarkdownNodeParser> _parserList;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseXmlToMarkdown"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        internal ParseXmlToMarkdown(IEnvironment environment)
        {
            this.InitDictionary(environment);
        }

        #endregion

        #region methods

        /// <summary>
        /// Parse the specified node.
        /// </summary>
        /// <param name="node">The xml node to parse.</param>
        /// <returns>The parsed node.</returns>
        internal String Parse(XNode node)
        {
            var element = node as XElement;

            if (element == null)
            {
                return null;
            }

            var strBuilder = new StringBuilder();

            foreach (var parser in this._parserList)
            {
                strBuilder.Append(parser.ParseToMarkdown(element));
            }

            return strBuilder.ToString();
        }

        #endregion

        #region helper methods

        private void InitDictionary(IEnvironment environment)
        {
            this._parserList = new List<IMarkdownNodeParser>();
            this._parserList.Add(new DocMarkdownNodeParser(this, environment));
            this._parserList.Add(new MemberMarkdownNodeParser(this, environment));
            this._parserList.Add(new SummaryMarkdownNodeParser(this, environment));
            this._parserList.Add(new SeeMarkdownNodeParser(this, environment));
            this._parserList.Add(new ExceptionMarkdownNodeParser(this, environment));
        }

        #endregion
    }
}
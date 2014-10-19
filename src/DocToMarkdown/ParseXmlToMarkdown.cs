//  *************************************************************
// <copyright file="ParseXmlToMarkdown.cs" company="None">
//     Copyright (c) 2014 andy. All rights reserved.
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
    public sealed class ParseXmlToMarkdown : IParserPool
    {
        #region fields

        private List<IMarkdownNodeParser> _parserList;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseXmlToMarkdown"/> class.
        /// </summary>
        /// <param name="dependencies">The dependency injected classes.</param>
        internal ParseXmlToMarkdown(IDependencies dependencies)
        {
            this.InitDictionary(dependencies);
        }

        #endregion

        #region methods

        /// <summary>
        /// Parse the specified node.
        /// </summary>
        /// <param name="node">The xml node to parse.</param>
        /// <returns>The parsed node.</returns>
        public String Parse(XNode node)
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

        private void InitDictionary(IDependencies dependencies)
        {
            this._parserList = new List<IMarkdownNodeParser>();
            this._parserList.Add(new DocMarkdownNodeParser(this, dependencies));
            this._parserList.Add(new MemberMarkdownNodeParser(this, dependencies));
            this._parserList.Add(new ParamMarkdownNodeParser(this, dependencies));
            this._parserList.Add(new SummaryMarkdownNodeParser(this, dependencies));
            this._parserList.Add(new SeeMarkdownNodeParser());
            this._parserList.Add(new ExceptionMarkdownNodeParser(this, dependencies));
            this._parserList.Add(new CodeMarkdownNodeParser(this, dependencies.Environment));
            this._parserList.Add(new ExampleMarkdownNodeParser(this, dependencies.Environment));
            this._parserList.Add(new RemarksMarkdownNodeParser(this, dependencies));
            this._parserList.Add(new TypeParamMarkdownNodeParser(this, dependencies.Environment));
            this._parserList.Add(new ReturnsMarkdownNodeParser(this, dependencies.Environment));
        }

        #endregion
    }
}
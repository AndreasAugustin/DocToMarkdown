//  *************************************************************
// <copyright file="MarkdownNodeParserPool.cs" company="None">
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
    using System.Text;
    using System.Xml.Linq;

    using DocToMarkdown.Common;

    /// <summary>
    /// Parse xml to markdown.
    /// </summary>
    internal sealed class MarkdownNodeParserPool : IParserPool
    {
        #region fields

        private Dictionary<Type, IMarkdownNodeParser> _parserDictionary;
        private ILogger _logger;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseXmlToMarkdown"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name = "markdownType">The markdown type.</param>
        /// <param name = "loggerManager">The logger manager.</param>
        internal MarkdownNodeParserPool(
            IEnvironment environment,
            MarkdownType markdownType,
            ILoggerManager loggerManager)
        {
            this.InitDictionary(environment, markdownType);
            this._logger = loggerManager.GetLogger("Parser");
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

            foreach (var parser in this._parserDictionary.Values)
            {
                strBuilder.Append(parser.ParseToMarkdown(element));
            }

            return strBuilder.ToString();
        }

        /// <summary>
        /// Parse the specified element.
        /// </summary>
        /// <param name="element">The xml element to parse.</param>
        /// <returns>The parsed node.</returns>
        /// <typeparam name="TParser">The parser.</typeparam>
        /// <exception cref="KeyNotFoundException">Thrown when the parser is not found.</exception>
        public String Parse<TParser>(XElement element)
            where TParser : IMarkdownNodeParser
        {
            if (!this._parserDictionary.ContainsKey(typeof(TParser)))
            {
                throw new KeyNotFoundException("Parser not found");
            }

            return this._parserDictionary[typeof(TParser)].ParseToMarkdown(element);
        }

        #endregion

        #region helper methods

        private void InitDictionary(IEnvironment environment, MarkdownType markdownType)
        {
            this._parserDictionary = new Dictionary<Type, IMarkdownNodeParser>();

            this._parserDictionary.Add(typeof(DocMarkdownNodeParser), new DocMarkdownNodeParser(this, environment));

            this._parserDictionary.Add(
                typeof(MemberMarkdownNodeParser),
                new MemberMarkdownNodeParser(
                    this,
                    environment));

            this._parserDictionary.Add(
                typeof(SummaryMarkdownNodeParser),
                new SummaryMarkdownNodeParser(
                    this,
                    environment));

            this._parserDictionary.Add(
                typeof(ReturnsMarkdownNodeParser),
                new ReturnsMarkdownNodeParser(
                    this,
                    environment));

            this._parserDictionary.Add(
                typeof(SeeMarkdownNodeParser),
                new SeeMarkdownNodeParser(environment, markdownType));
            this._parserDictionary.Add(typeof(ParaMarkdownNodeParser), new ParaMarkdownNodeParser(this));

            this._parserDictionary.Add(
                typeof(ListMarkdownNodeParser),
                new ListMarkdownNodeParser(environment, this, 0));

            this._parserDictionary.Add(
                typeof(TypeparamMarkdownNodeParser),
                new TypeparamMarkdownNodeParser(
                    this,
                    environment));

            this._parserDictionary.Add(
                typeof(ParamMarkdownNodeParser),
                new ParamMarkdownNodeParser(this, environment));

            this._parserDictionary.Add(typeof(ParamRefMarkdownNodeParser), new ParamRefMarkdownNodeParser());

            this._parserDictionary.Add(
                typeof(ExceptionMarkdownNodeParser),
                new ExceptionMarkdownNodeParser(
                    this,
                    environment));
           
            this._parserDictionary.Add(
                typeof(CodeMarkdownNodeParser),
                new CodeMarkdownNodeParser(
                    this,
                    environment));

            this._parserDictionary.Add(
                typeof(ExampleMarkdownNodeParser),
                new ExampleMarkdownNodeParser(
                    this,
                    environment));

            this._parserDictionary.Add(
                typeof(RemarksMarkdownNodeParser),
                new RemarksMarkdownNodeParser(
                    this,
                    environment));

            this._parserDictionary.Add(
                typeof(ValueMarkdownNodeParser),
                new ValueMarkdownNodeParser(environment));   
        }

        #endregion
    }
}
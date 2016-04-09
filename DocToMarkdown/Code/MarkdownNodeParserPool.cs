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
        /// Initializes a new instance of the <see cref="MarkdownNodeParserPool"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name = "markdownType">The markdown type.</param>
        /// <param name = "loggerManager">The logger manager.</param>
        internal MarkdownNodeParserPool(
            IEnvironment environment,
            MarkdownType markdownType,
            ILoggerManager loggerManager)
        {
            this._logger = loggerManager.GetLogger("Parser");
            this.InitDictionary(environment, markdownType);
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
            this._logger.Debug("---- Started to parse in parser pool.");

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

            this._logger.Debug("---- Finished parsing in parser pool.");

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
            this._logger.Debug(String.Format("---- Started parsing for Parser {0}.", typeof(TParser).ToString()));

            if (!this._parserDictionary.ContainsKey(typeof(TParser)))
            {
                throw new KeyNotFoundException("Parser not found");
            }

            var parsed = this._parserDictionary[typeof(TParser)].ParseToMarkdown(element);

            this._logger.Debug(String.Format("---- Finished parsing for Parser {0}.", typeof(TParser).ToString()));

            return parsed;
        }

        #endregion

        #region helper methods

        private void InitDictionary(IEnvironment environment, MarkdownType markdownType)
        {
            this._logger.Debug("---- Started initialising of parser dictionary.");

            this._parserDictionary = new Dictionary<Type, IMarkdownNodeParser>();

            this._parserDictionary.Add(
                typeof(DocMarkdownNodeParser),
                new DocMarkdownNodeParser(
                    this,
                    environment,
                    this._logger));

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
                typeof(PermissionMarkdownNodeParser),
                new PermissionMarkdownNodeParser(
                    environment));

            this._parserDictionary.Add(
                typeof(SeeMarkdownNodeParser),
                new SeeMarkdownNodeParser(environment, markdownType));

            this._parserDictionary.Add(
                typeof(SeealsoMarkdownNodeParser),
                new SeealsoMarkdownNodeParser(environment, markdownType));

            this._parserDictionary.Add(typeof(ParaMarkdownNodeParser), new ParaMarkdownNodeParser(this));

            this._parserDictionary.Add(
                typeof(ListMarkdownNodeParser),
                new ListMarkdownNodeParser(environment, this, 0));

            this._parserDictionary.Add(
                typeof(TypeparamMarkdownNodeParser),
                new TypeparamMarkdownNodeParser(
                    this,
                    environment));

            this._parserDictionary.Add(typeof(TypeparamrefMarkdownNodeParser), new TypeparamrefMarkdownNodeParser());

            this._parserDictionary.Add(
                typeof(ParamMarkdownNodeParser),
                new ParamMarkdownNodeParser(this, environment));

            this._parserDictionary.Add(typeof(ParamrefMarkdownNodeParser), new ParamrefMarkdownNodeParser());

            this._parserDictionary.Add(
                typeof(ExceptionMarkdownNodeParser),
                new ExceptionMarkdownNodeParser(
                    this,
                    environment,
                    markdownType));
           
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

            this._logger.Debug("---- Finished initialising of parser dictionary.");
        }

        #endregion
    }
}
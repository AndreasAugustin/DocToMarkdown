//  *************************************************************
// <copyright file="ListMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown
{
    using System;
    using System.Text;
    using System.Xml.Linq;

    using DocToMarkdown.Common;

    /// <summary>
    /// List markdown node parser.
    /// </summary>
    /// <example>
    /// For using the <c>list</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/y3ww3c7e.aspx"/>
    /// </example>
    /// <example> For a list
    /// <list type="bullet">
    /// <item>
    /// <description>Item 1.</description> 
    /// </item> 
    /// <item> 
    /// <description>Item 2.</description> 
    /// </item> 
    /// </list>
    /// </example> 
    internal class ListMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private IParserPool _parserPool;
        private String _template;
        private Int32 _listLevel;
        private IEnvironment _environment;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.ListMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="parserPool">The parser pool.</param>
        /// <param name = "listLevel">The level of a list. 
        /// E.q. it is possible to have a list in the list. In this case the level is 1.
        /// If the list is at the master node, the level is 0.
        /// </param>
        /// <exception cref="ArgumentException">Thrown when the listLevel is less then zero.</exception>
        internal ListMarkdownNodeParser(IEnvironment environment, IParserPool parserPool, Int32 listLevel = 0)
        {
            if (listLevel < 0)
            {
                throw new ArgumentException("listlevel needs to be greater or equal 0", "listLevel");
            }

            this._parserPool = parserPool;
            this._listLevel = listLevel;
            this._environment = environment;
            this.InitTemplate(this._environment, this._listLevel);
        }

        #endregion

        #region methods

        /// <summary>
        /// Parses to markdown.
        /// </summary>
        /// <returns>The parsed markdown.</returns>
        /// <param name="element">The element.</param>
        public String ParseToMarkdown(XElement element)
        {
            if (element.Name != "list")
            {
                return null;
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

            var innerListElements = element.Descendants("list");

            foreach (var innerListElement in innerListElements)
            {
                var newListParser = new ListMarkdownNodeParser(
                                        this._environment,
                                        this._parserPool,
                                        this._listLevel + 1);

                var parsedInnerList = newListParser.ParseToMarkdown(innerListElement);

                if (parsedInnerList != null)
                {
                    innerListElement.SetValue(parsedInnerList);
                }
            }

            var resultStringBuilder = new StringBuilder();

            if (this._listLevel == 0)
            {
                resultStringBuilder.Append("**List:**");
            }

            var itemElements = element.Elements("item");

            foreach (var description in itemElements.Elements("description"))
            {
                resultStringBuilder.Append(String.Format(this._template, description.Value));
            }
          
            return resultStringBuilder.ToString();
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment, Int32 listLevel)
        {
            this._template = String.Format(
                "{0}{1}- {2}",
                environment.NewLine,
                new StringBuilder().Append(' ', listLevel),
                "{0}");
        }

        #endregion
    }
}
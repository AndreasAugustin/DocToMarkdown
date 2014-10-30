//  *************************************************************
// <copyright file="ExampleMarkdownNodeParser.cs" company="None">
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

    /// <summary>
    /// Example markdown node parser.
    /// </summary>
    /// <example>
    /// For using the <c>example</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/9w4cf933.aspx"/>
    /// </example>
    internal class ExampleMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private readonly IParserPool _parserPool;
        private String _template;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.ExampleMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parserPool">The parser pool.</param>
        /// <param name="environment">The environment.</param>
        public ExampleMarkdownNodeParser(IParserPool parserPool, IEnvironment environment)
        {
            this.InitTemplate(environment);
            this._parserPool = parserPool;
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
            if (element.Name != "example")
            {
                return null;
            }

            var elements = element.Elements();
            var stringBuilder = new StringBuilder();

            var description = element.Value;
            stringBuilder.Append(description);

            foreach (var el in elements)
            {
                stringBuilder.Append(this._parserPool.Parse(el));
            }

            var name = element.Attribute("name").Value;

            return String.Format(
                this._template,
                name,
                stringBuilder.ToString());
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment)
        {
            this._template = String.Format(
                "\tExample: _C# code_{1}{1}```c#{1}{0}{1}```{1}{1}",
                "{0}",
                environment.NewLine);
        }

        #endregion
    }
}
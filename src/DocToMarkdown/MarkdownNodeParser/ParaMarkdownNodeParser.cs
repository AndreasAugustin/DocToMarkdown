//  *************************************************************
// <copyright file="ParaMarkdownNodeParser.cs" company="None">
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
    /// Para markdown node parser.
    /// </summary>
    /// <example>
    /// For using the <c>para</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/x640hcd2.aspx"/>
    /// </example>
    /// <example>
    /// For using a para tag.
    /// <para>Here's how you could make a second paragraph in a description. 
    /// <see cref="System.Console.WriteLine(System.String)"/> for information about output statements.</para>
    /// </example>
    internal class ParaMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private IParserPool _parserPool;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.ParaMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parserPool">The parser pool.</param>
        internal ParaMarkdownNodeParser(IParserPool parserPool)
        {
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
            if (element.Name != "para")
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

            return String.Format("*{0}*", element.Value);
        }

        #endregion
    }
}
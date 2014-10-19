//  *************************************************************
// <copyright file="RemarksMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy. All rights reserved. 
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
    /// Remarks markdown node parser.
    /// </summary>
    internal class RemarksMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private String _template;
        private readonly IParserPool _parserPool;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="RemarksMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parserPool">The parser pool.</param>
        /// <param name="dependencies">The dependency injected classes.</param>
        public RemarksMarkdownNodeParser(IParserPool parserPool, IDependencies dependencies)
        {
            this.InitTemplate(dependencies.Environment);
            this._parserPool = parserPool;
        }

        #endregion

        #region methods

        public String ParseToMarkdown(System.Xml.Linq.XElement element)
        {
            if (element.Name != "remarks")
            {
                return String.Empty;
            }

            var elements = element.Elements();
            var stringBuilder = new StringBuilder();

            foreach (var el in elements)
            {
                stringBuilder.Append(this._parserPool.Parse(el));
            }

            return String.Format(
                _template,
                element.Value,
                stringBuilder.ToString());
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment)
        {
            this._template = String.Format("\t{1}{1}>{0}{1}{1}", "{0}", environment.NewLine);
        }

        #endregion
    }
}
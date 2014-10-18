//  *************************************************************
// <copyright file="ParamMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy. 
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown
{
    using System;

    public class ParamMarkdownNodeParser : AbstractMarkdownNodeParser, IMarkdownNodeParser
    {
        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ParamMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="dependencies">The dependency injected parts.</param>
        public ParamMarkdownNodeParser(ParseXmlToMarkdown parser, IDependencies dependencies)
            : base(parser, dependencies)
        {
        }

        #endregion

        #region methods

        #endregion

        #region helper methods

        #endregion
    }
}
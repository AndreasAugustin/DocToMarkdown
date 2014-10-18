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

    using DocToMarkdown.Common;

    /// <summary>
    /// Remarks markdown node parser.
    /// </summary>
    internal class RemarksMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private String _template;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="RemarksMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="dependencies">The dependency injected classes.</param>
        public RemarksMarkdownNodeParser(IDependencies dependencies)
        {
            this.InitTemplate(dependencies.Environment);
        }

        #endregion

        #region methods

        public String ParseToMarkdown(System.Xml.Linq.XElement element)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment)
        {
            this._template = String.Format("{1}{1}>{0}{1}{1}", "{0}", environment.NewLine);
        }

        #endregion
    }
}
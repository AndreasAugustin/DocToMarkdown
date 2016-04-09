//  *************************************************************
// <copyright file="PermissionMarkdownNodeParser.cs" company="None">
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
    /// Permission markdown node parser.
    /// </summary>
    /// <example>
    /// For using the <c>permission</c> tag are found at
    /// <see href="http://msdn.microsoft.com/en-us/library/h9df2kfb.aspx"/>
    /// </example>
    internal class PermissionMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private String _template;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocToMarkdown.PermissionMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        internal PermissionMarkdownNodeParser(IEnvironment environment)
        {
            this.InitTemplate(environment);
        }

        #endregion

        #region methods

        /// <summary>
        /// Parses to markdown.
        /// The <paramref name="element"/> is the element to parse.
        /// </summary>
        /// <returns>The parsed markdown.</returns>
        /// <param name="element">The element.</param>
        /// <exception cref="NotImplementedException">Thrown when trying to run this method.</exception>
        /// <permission cref="System.Security.PermissionSet">Everyone can access this method within this assembly.</permission>
        public String ParseToMarkdown(XElement element)
        {
            if (element.Name != "permission")
            {
                return null;
            }

            var cref = element.Attribute("cref").Value;
            var value = element.Value;

            return String.Format(
                this._template,
                cref,
                value);
        }

        #endregion

        #region helper methods

        private void InitTemplate(IEnvironment environment)
        {
            if (!String.IsNullOrEmpty(this._template))
            {
                return;
            }

            this._template = String.Format("{2}**Permision:**>*{0}*: {1}{2}", "{0}", "{1}", environment.NewLine);
        }

        #endregion
    }
}
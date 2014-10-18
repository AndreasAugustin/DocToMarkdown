//  *************************************************************
// <copyright file="MemberMarkdownNodeParser.cs" company="None">
//     Copyright (c) 2014 andy. 
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
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    /// <summary>
    /// Member markdown node parser.
    /// </summary>
    internal class MemberMarkdownNodeParser : IMarkdownNodeParser
    {
        #region fields

        private readonly Dictionary<String, String> _templateDictionary = new Dictionary<String, String>();
        private readonly Regex memperTypeRegex = new Regex(@"^.*?(?=:)");
        private readonly ParseXmlToMarkdown _parser;

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="dependencies">The dependency injected parts.</param>
        internal MemberMarkdownNodeParser(ParseXmlToMarkdown parser, IDependencies dependencies)
        {
            this._parser = parser;
            this.InitTemplateDictionary(dependencies.Environment);
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
            if (element.Name != "member")
            {
                return null;
            }
                
            var name = element.Attribute("name");
            if (name == null)
            {
                return null;
            }

            // Check if element is for method, class, ...
            var match = this.memperTypeRegex.Match(name.Value);

            if (match == null)
            {
                return null;
            }

            var memberType = match.ToString();

            if (!this._templateDictionary.ContainsKey(memberType))
            {
                return null;
            }

            var template = this._templateDictionary[memberType];

            var elements = element.Elements();
            var stringBuilder = new StringBuilder();

            foreach (var el in elements)
            {
                stringBuilder.Append(this._parser.Parse(el));
            }
                
            var val = String.Format("<a name=\"{1}\"></a>{0}", name.Value, name.Value.ToLower());

            return String.Format(
                template,
                val,
                stringBuilder.ToString());
        }

        #endregion

        #region helper methods

        private void InitTemplateDictionary(IEnvironment environment)
        {
            if (this._templateDictionary.Any())
            {
                return;
            }

            // Member is a type
            this._templateDictionary.Add(
                "T",
                String.Format("---{2}#### {0}{2}{2}{1}{2}{2}", "{0}", "{1}", environment.NewLine)); 

            // Member is a method
            this._templateDictionary.Add(
                "M",
                String.Format("#### {0}{2}{2}{2}{1}{2}", "{0}", "{1}", environment.NewLine));
        }

        #endregion
    }
}
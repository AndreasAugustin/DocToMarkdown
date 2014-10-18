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
    internal class MemberMarkdownNodeParser : AbstractMarkdownNodeParser, IMarkdownNodeParser
    {
        #region fields

        private static readonly Dictionary<String, String> TemplateDictionary = new Dictionary<String, String>();
        private readonly Regex memperTypeRegex = new Regex(@"^.*?(?=:)");

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberMarkdownNodeParser"/> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        /// <param name="environment">The environment.</param>
        internal MemberMarkdownNodeParser(ParseXmlToMarkdown parser, IEnvironment environment)
            : base(parser, environment)
        {
            this.InitTemplateDictionary();
        }

        #endregion

        #region methods

        /// <summary>
        /// Parses to markdown.
        /// </summary>
        /// <returns>The parsed markdown.</returns>
        /// <param name="element">The element.</param>
        public override String ParseToMarkdown(XElement element)
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

            if (!TemplateDictionary.ContainsKey(memberType))
            {
                return null;
            }

            var template = TemplateDictionary[memberType];

            var elements = element.Elements();
            var stringBuilder = new StringBuilder();

            foreach (var el in elements)
            {
                stringBuilder.Append(this.Parser.Parse(el));
            }
                
            var val = String.Format("<a name=\"{1}\"></a>{0}", name.Value, name.Value.ToLower());

            return String.Format(
                template,
                val,
                stringBuilder.ToString());
        }

        #endregion

        #region helper methods

        private void InitTemplateDictionary()
        {
            if (TemplateDictionary.Any())
            {
                return;
            }

            // Member is a type
            TemplateDictionary.Add(
                "T",
                String.Format("---{2}#### {0}{2}{2}{1}{2}{2}", "{0}", "{1}", this.Environment.NewLine)); 

            // Member is a method
            TemplateDictionary.Add(
                "M",
                String.Format("#### {0}{2}{2}{2}{1}{2}", "{0}", "{1}", this.Environment.NewLine));
        }

        #endregion
    }
}
//  *************************************************************
// <copyright file="IParserPool.cs" company="None">
//     Copyright (c) 2014 andy. 
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown
{
    using System;
    using System.Xml.Linq;

    /// <summary>
    /// Interface for parser pools.
    /// </summary>
    internal interface IParserPool
    {
        #region methods

        /// <summary>
        /// Parse the specified node.
        /// </summary>
        /// <param name="node">The xml node to parse.</param>
        /// <returns>The parsed node.</returns>
        String Parse(XNode node);

        /// <summary>
        /// Parse the specified element.
        /// </summary>
        /// <param name="element">The xml element to parse.</param>
        /// <returns>The parsed node.</returns>
        /// <typeparam name="TParser">The parser.</typeparam>
        String Parse<TParser>(XElement element)
            where TParser : IMarkdownNodeParser;

        #endregion
    }
}
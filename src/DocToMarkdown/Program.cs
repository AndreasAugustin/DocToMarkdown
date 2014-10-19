//  *************************************************************
// <copyright file="Program.cs" company="None">
//     Copyright (c) 2014 andy. All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Xml.Linq;

    using DocToMarkdown.Common;

    /// <summary>
    /// Main class.
    /// </summary>
    internal class Program
    {
        #region fields

        private static readonly IConfiguration Configuration = new ConfigurationAdapter();
        private static readonly IDependencies Dependencies = new Dependencies();
        private static XElementCorrection _correction = new XElementCorrection();
        private static IParserPool _parser;

        #endregion

        #region methods

        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(String[] args)
        {
            Console.WriteLine("Hello World!");

            _parser = new ParseXmlToMarkdown(Dependencies);

            var xmlSourcePath = Configuration["xmlSource.absolute.path"];
            var markdownTargetPath = Configuration["markupTarget.folder.path"];

            String xml = null;

            try
            {
                xml = File.ReadAllText(xmlSourcePath);
            }
            catch (IOException e)
            {
                Trace.TraceError(e.ToString());
                Console.WriteLine(e.ToString());
            }
                
            XDocument doc = null;
            try
            {
                doc = XDocument.Parse(xml);
            }
            catch (System.Xml.XmlException e)
            {
                Trace.TraceError(e.ToString());
                Console.WriteLine(e.ToString());
            }

            var node = doc.Root;

            var correctedDocDictionary = _correction.CorrectionAndNamespaceOrderXElement(node);
                    
            foreach (var nameSpace in correctedDocDictionary.Keys)
            {
                var absoluteTargetPath = Path.Combine(markdownTargetPath, Path.ChangeExtension(nameSpace, "markdown"));

                var markdownString = _parser.Parse(correctedDocDictionary[nameSpace]);

                using (var streamWriter = new StreamWriter(absoluteTargetPath))
                {
                    streamWriter.Write(markdownString);
                }
            }
                
            Console.ReadLine();
        }

        #endregion
    }
}
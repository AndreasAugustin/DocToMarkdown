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
    /// Starts the console application.
    /// Some input for this project came from
    /// <see href="https://gist.github.com/lontivero/593fc51f1208555112e0"/>
    /// </summary>
    internal class Program
    {
        #region fields

        private static readonly IEnvironment Environment = new EnvironmentAdapter();
        private static IParserPool _parser;
        private static String _xmlSourcePath;
        private static String _markdownTargetPath;
        private static MarkdownType _markdownType;
        private static ILoggerManager _loggerManager;

        #endregion

        #region methods

        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(String[] args)
        {
            Console.WriteLine("Starting to parse");

            Init();

            String xml = null;

            foreach (var xmlFileAbsolutePath in Directory.GetFiles(_xmlSourcePath))
            {
                if (Path.GetExtension(xmlFileAbsolutePath) != ".xml")
                {
                    continue;
                }

                try
                {
                    xml = File.ReadAllText(xmlFileAbsolutePath);
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
                    Console.WriteLine(String.Format("Parsed {0}", xmlFileAbsolutePath));
                }
                catch (System.Xml.XmlException e)
                {
                    Trace.TraceError(e.ToString());
                    Console.WriteLine(e.ToString());
                }

                var node = doc.Root;

                var correctedDocDictionary = new XElementCorrection(_loggerManager).CorrectionAndNamespaceOrderXElement(node);
                    
                foreach (var nameSpace in correctedDocDictionary.Keys)
                {
                    var absoluteTargetPath = Path.Combine(_markdownTargetPath, String.Format("{0}.md", nameSpace));

                    var markdownString = _parser.Parse(correctedDocDictionary[nameSpace]);

                    using (var streamWriter = new StreamWriter(absoluteTargetPath))
                    {
                        streamWriter.WriteLine(String.Format("### Namespace: {0} ###", nameSpace));
                        streamWriter.Write(markdownString);
                        Console.WriteLine(String.Format("Writen: {0}", absoluteTargetPath));
                    }
                }
            }
                
            ShutDown();
        }

        #endregion

        #region helper methods

        private static void ShutDown()
        {
            if (_loggerManager != null)
            {
                _loggerManager.ShutDown();
            }

            Console.WriteLine("End");

            Console.WriteLine("----------- Please press return to proceed -----------");
            Console.ReadLine();
        }

        private static void Init()
        {
            Console.WriteLine("Starting init application");
            _loggerManager = new NLogManagerAdapter();

            Console.WriteLine("Setting configuration");
            var configuration = new ConfigurationAdapter(_loggerManager, AppDomain.CurrentDomain.BaseDirectory);

            SetLoggerManager(configuration);

            SetMarkdownType(configuration);

            Console.WriteLine("Setting up source paths");
            _xmlSourcePath = configuration["xmlSource.folder.path"];
            Console.WriteLine("Xml source path: {0}", _xmlSourcePath);

            _markdownTargetPath = configuration["markupTarget.folder.path"];
            Console.WriteLine("Markdown target path: {0}", _markdownTargetPath);

            Console.WriteLine("Source paths setting finished");

            _parser = new MarkdownNodeParserPool(Environment, _markdownType, _loggerManager);
        }

        private static void SetLoggerManager(IConfiguration configuration)
        {
            Console.WriteLine("Setting logger manager");

            var globalThresholdString = configuration["logger.global.threshold"];
            Console.WriteLine("Global threshold from configuration: {0}", globalThresholdString);

            Int32 globalThreshold;
            if (!Int32.TryParse(globalThresholdString, out globalThreshold))
            {
                throw new InvalidCastException("The cast for the global threshold did not work");
            }

            Console.WriteLine("Global threshold: {0}", globalThreshold);

            if (globalThreshold > 7 || globalThreshold < 0)
            {
                const String Message = "Logger configuration is not set correctly";
                Log(Message, LogLevel.Fatal);
                throw new ApplicationException(Message);
            }

            if (globalThreshold != 7)
            {
                _loggerManager.GlobalThreshold = (LogLevel)globalThreshold;
            }

            Console.WriteLine("Logger manager setup finished");
        }

        private static void SetMarkdownType(IConfiguration configuration)
        {
            Console.WriteLine("Setting up markdown type");

            var markdownTypeString = configuration["markdownType"];

            Console.WriteLine("MarkdownTypeString: {0}", markdownTypeString);

            Int32 markdownType;

            if (!Int32.TryParse(markdownTypeString, out markdownType))
            {
                throw new InvalidCastException("The cast for the markdown type did not work");
            }

            Console.WriteLine("MarkdownType: {0}", markdownType);

            _markdownType = (MarkdownType)markdownType;
            Console.WriteLine("Markdowntype setup finished");
        }

        private static void Log(String message, LogLevel logLevel)
        {
            if (_loggerManager != null)
            {
                _loggerManager.GetLogger("Programm").Log(logLevel, message);
            }           
        }

        #endregion
    }
}
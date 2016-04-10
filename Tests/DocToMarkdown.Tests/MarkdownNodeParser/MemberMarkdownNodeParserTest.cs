//  *************************************************************
// <copyright file="MemberMarkdownNodeParserTest.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Tests
{

    using System;
    using System.Xml.Linq;

    using DocToMarkdown.Common;

    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Test for the <see cref="MemberMarkdownNodeParser"/> class.
    /// </summary>
    [TestFixture]
    public class MemberMarkdownNodeParserTest
    {
        #region fields

        private const String NodeString = "<member name=\"T:DocToMarkdown.Program\">" +
                                          "<summary>Starts the console application. Some input for this project came from " +
                                          "<see href=\"https://gist.github.com/lontivero/593fc51f1208555112e0\"/>" +
                                          "</summary>" +
                                          "</member>";

        #endregion

        #region properties

        private XElement XmlInput
        {
            get
            {
                return XElement.Parse(NodeString);
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Test for creating a new object.
        /// </summary>
        [Test]
        [Category("Unit test: parser")]
        public void Init_CreateObject_IsNotNull()
        {
            var parserPoolStub = Substitute.For<IParserPool>();
            var environmentStub = Substitute.For<IEnvironment>();

            var obj = new MemberMarkdownNodeParser(parserPoolStub, environmentStub);

            Assert.IsNotNull(obj);
        }

        #endregion
    }
}
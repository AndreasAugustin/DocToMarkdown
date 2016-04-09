//  *************************************************************
// <copyright file="XElementCorrectionTest.cs" company="None">
//     Copyright (c) 2014 andy.  All rights reserved.
// </copyright>
// <license>MIT Licence</license>
// <author>andy</author>
// <email>andy.augustin@t-online.de</email>
// *************************************************************

namespace DocToMarkdown.Tests
{
    using System;
    using System.Text;
    using System.Xml.Linq;

    using DocToMarkdown;
    using DocToMarkdown.Common;

    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Test for the <see cref="XElementCorrection"/> class. 
    /// </summary>
    [TestFixture]
    public class XElementCorrectionTest
    {
        #region properties

        private XElement MockElement
        {
            get
            {
                const String StringElement1 = @"<member name=""T:GenericMath.LinearAlgebra.SpecialPolynomials"">
                                        <summary>
                                                    Special polynomials.
                                                    </summary>
                                                    </member>";

                const String StringElement2 = @"<member name=""M:GenericMath.LinearAlgebra.SpecialPolynomials.ZeroPolynomial``2(System.UInt32)"">
                                                    <summary>
                                                    Creates the zeros polynomial for dimension.
                                                    </summary>
                                                    <returns>The polynomial.</returns>
                                                    <param name=""degree"">The dimension.</param>
                                                    <typeparam name=""T"">The type parameter.</typeparam>
                                                    <typeparam name=""TGroup"">The underlying structure.</typeparam>
                                                </member>";

                const String StringElement3 = @"<member name=""M:GenericMath.LinearAlgebra.SpecialPolynomials.OnePolynomial``2(System.UInt32)"">
                                            <summary>
                                            Creates the one polynomial for dimension.
                                            </summary>
                                            <returns>The polynomial.</returns>
                                            <param name=""degree"">The dimension.</param>
                                            <typeparam name=""T"">The type parameter.</typeparam>
                                            <typeparam name=""TRing"">The underlying structure.</typeparam>
                                        </member>";

                const String StringElement4 = @"<member name=""T:Generic.LinearAlgebra.Polynomial`2"">
                                        <summary>
                                        Polynomial from set.
                                        </summary>
                                        <typeparam name=""T"">The type parameter.</typeparam>
                                        <typeparam name=""TStruct"">The underlying structure.</typeparam>
                                    </member>";

                var stringBuilder = new StringBuilder();
                stringBuilder.Append(StringElement1);
                stringBuilder.Append(StringElement2);
                stringBuilder.Append(StringElement3);
                stringBuilder.Append(StringElement4);

                var elementString = String.Format(@"<members>{0}</members>", stringBuilder.ToString());

                return XElement.Parse(elementString);
            }
        }

        #endregion properties

        #region methods

        /// <summary>
        /// Corrections the and namespace order X element run result count equals expected.
        /// </summary>
        [Category("Correction test")]
        [Test]
        public void CorrectionAndNamespaceOrderXElement_Run_ResultCountEqualsExpected()
        {
            var mockElement = this.MockElement;
            var loggerManagerStub = Substitute.For<ILoggerManager>();

            var xmlElementCorrection = new XElementCorrection(loggerManagerStub);

            var result = xmlElementCorrection.CorrectionAndNamespaceOrderXElement(mockElement);

            Assert.AreEqual(2, result.Count);
        }

        #endregion
    }
}
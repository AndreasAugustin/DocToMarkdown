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
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;

    using DocToMarkdown;
    using NUnit.Framework;

    [TestFixture]
    public class XElementCorrectionTest
    {
        #region properties

        private XElement MockElement
        {
            get
            {
                const string stringElement1 = @"<member name=""T:GenericMath.LinearAlgebra.SpecialPolynomials"">
                                        <summary>
                                                    Special polynomials.
                                                    </summary>
                                                    </member>";

                const string stringElement2 = @"<member name=""M:GenericMath.LinearAlgebra.SpecialPolynomials.ZeroPolynomial``2(System.UInt32)"">
                                                    <summary>
                                                    Creates the zeros polynomial for dimension.
                                                    </summary>
                                                    <returns>The polynomial.</returns>
                                                    <param name=""degree"">The dimension.</param>
                                                    <typeparam name=""T"">The type parameter.</typeparam>
                                                    <typeparam name=""TGroup"">The underlying structure.</typeparam>
                                                </member>";

                const string stringElement3 = @"<member name=""M:GenericMath.LinearAlgebra.SpecialPolynomials.OnePolynomial``2(System.UInt32)"">
                                            <summary>
                                            Creates the one polynomial for dimension.
                                            </summary>
                                            <returns>The polynomial.</returns>
                                            <param name=""degree"">The dimension.</param>
                                            <typeparam name=""T"">The type parameter.</typeparam>
                                            <typeparam name=""TRing"">The underlying structure.</typeparam>
                                        </member>";

                const string stringElement4 = @"<member name=""T:Generic.LinearAlgebra.Polynomial`2"">
                                        <summary>
                                        Polynomial from set.
                                        </summary>
                                        <typeparam name=""T"">The type parameter.</typeparam>
                                        <typeparam name=""TStruct"">The underlying structure.</typeparam>
                                    </member>";

                var stringBuilder = new StringBuilder();
                stringBuilder.Append(stringElement1);
                stringBuilder.Append(stringElement2);
                stringBuilder.Append(stringElement3);
                stringBuilder.Append(stringElement4);

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

            var xElementCorrection = new XElementCorrection();

            var result = xElementCorrection.CorrectionAndNamespaceOrderXElement(mockElement);

            Assert.AreEqual(2, result.Count);
        }

        #endregion
    }
}
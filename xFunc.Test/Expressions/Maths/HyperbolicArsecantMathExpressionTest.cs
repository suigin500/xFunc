﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class HyperbolicArsecantMathExpressionTest
    {

        private MathParser parser;

        [TestInitialize]
        public void TestInit()
        {
            parser = new MathParser();
        }

        [TestMethod]
        public void CalculateTest()
        {
            var exp = parser.Parse("arsech(1)");

            Assert.AreEqual(MathExtentions.Asech(1), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = parser.Parse("deriv(arsech(2x), x)").Derivative();

            Assert.AreEqual("-(2 / ((2 * x) * sqrt(1 - ((2 * x) ^ 2))))", exp.ToString());
        }

    }

}
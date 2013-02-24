﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{

    [TestClass]
    public class HyperbolicArsineTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            var exp = new HyperbolicArsine(new Number(1));

            Assert.AreEqual(MathExtentions.Asinh(1), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = new HyperbolicArsine(new Multiplication(new Number(2), new Variable('x')));
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("(2 * 1) / sqrt(((2 * x) ^ 2) + 1)", deriv.ToString());
        }

    }

}
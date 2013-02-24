﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test.Expressions.Maths.Hyperbolic
{

    [TestClass]
    public class HyperbolicTangentTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            var exp = new HyperbolicTangent(new Number(1));

            Assert.AreEqual(Math.Tanh(1), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = new HyperbolicTangent(new Multiplication(new Number(2), new Variable('x')));
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("(2 * 1) / (cosh(2 * x) ^ 2)", deriv.ToString());
        }

    }

}
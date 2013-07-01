﻿// Copyright 2012-2013 Dmitry Kischenko
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
// express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
using System;

namespace xFunc.Maths.Expressions.Hyperbolic
{

    public class Sinh : UnaryMathExpression
    {

        public Sinh()
            : base(null)
        {

        }

        public Sinh(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            return ToString("sinh({0})");
        }

        public override double Calculate()
        {
            return Math.Sinh(firstMathExpression.Calculate());
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Sinh(firstMathExpression.Calculate(parameters));
        }

        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return Math.Sinh(firstMathExpression.Calculate(parameters, functions));
        }

        public override IMathExpression Clone()
        {
            return new Sinh(firstMathExpression.Clone());
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            var cosh = new Cosh(firstMathExpression.Clone());
            var mul = new Mul(firstMathExpression.Clone().Differentiate(variable), cosh);

            return mul;
        }

    }

}
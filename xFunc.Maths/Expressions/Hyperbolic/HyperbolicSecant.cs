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

    public class HyperbolicSecant : UnaryMathExpression
    {

        public HyperbolicSecant()
            : base(null)
        {

        }

        public HyperbolicSecant(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("sech({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return MathExtentions.Sech(firstMathExpression.Calculate(parameters));
        }

        public override IMathExpression Clone()
        {
            return new HyperbolicSecant(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(Variable variable)
        {
            var tanh = new HyperbolicTangent(firstMathExpression.Clone());
            var sech = Clone();
            var mul1 = new Multiplication(tanh, sech);
            var mul2 = new Multiplication(firstMathExpression.Clone().Differentiation(variable), mul1);
            var unMinus = new UnaryMinus(mul2);

            return unMinus;
        }

    }

}
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

namespace xFunc.Maths.Expressions
{

    public class Exp : UnaryMathExpression
    {

        public Exp() : base(null) { }

        public Exp(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("exp({0})");
        }

        public override double Calculate()
        {
            return Math.Exp(firstMathExpression.Calculate());
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Exp(firstMathExpression.Calculate(parameters));
        }

        protected override IMathExpression _Differentiation(Variable variable)
        {
            Mul mul = new Mul(firstMathExpression.Clone().Differentiate(variable), Clone());

            return mul;
        }

        public override IMathExpression Clone()
        {
            return new Exp(firstMathExpression.Clone());
        }

    }

}
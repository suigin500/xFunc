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
    
    public class Sqrt : UnaryMathExpression
    {

        public Sqrt() : base(null) { }

        public Sqrt(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("sqrt({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Sqrt(FirstMathExpression.Calculate(parameters));
        }

        protected override IMathExpression _Derivative(Variable variable)
        {
            Multiplication mul = new Multiplication(new Number(2), Clone());
            Division div = new Division(firstMathExpression.Clone().Differentiation(variable), mul);

            return div;
        }

        public override IMathExpression Clone()
        {
            return new Sqrt(firstMathExpression.Clone());
        }

    }

}
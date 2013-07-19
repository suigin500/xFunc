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
    
    public class Mul : BinaryMathExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Mul"/> class.
        /// </summary>
        public Mul() : base(null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mul"/> class.
        /// </summary>
        /// <param name="firstOperand">The first (left) operand.</param>
        /// <param name="secondOperand">The second (right) operand.</param>
        public Mul(IMathExpression firstOperand, IMathExpression secondOperand) : base(firstOperand, secondOperand) { }

        /// <summary>
        /// Converts this expression to the equivalent string.
        /// </summary>
        /// <returns>The string that represents this expression.</returns>
        public override string ToString()
        {
            if (parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} * {1})");
            }

            return ToString("{0} * {1}");
        }

        public override double Calculate()
        {
            return firstMathExpression.Calculate() * secondMathExpression.Calculate();
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return firstMathExpression.Calculate(parameters) * secondMathExpression.Calculate(parameters);
        }

        public override double Calculate(MathParameterCollection parameters, MathFunctionCollection functions)
        {
            return firstMathExpression.Calculate(parameters, functions) * secondMathExpression.Calculate(parameters, functions);
        }

        public override IMathExpression Differentiate(Variable variable)
        {
            var first = MathParser.HasVar(firstMathExpression, variable);
            var second = MathParser.HasVar(secondMathExpression, variable);

            if (first && second)
            {
                Mul mul1 = new Mul(firstMathExpression.Clone().Differentiate(variable), secondMathExpression.Clone());
                Mul mul2 = new Mul(firstMathExpression.Clone(), secondMathExpression.Clone().Differentiate(variable));
                Add add = new Add(mul1, mul2);

                return add;
            }
            if (first)
            {
                return new Mul(firstMathExpression.Clone().Differentiate(variable), secondMathExpression.Clone());
            }
            if (second)
            {
                return new Mul(firstMathExpression.Clone(), secondMathExpression.Clone().Differentiate(variable));
            }

            return new Number(0);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the new instance of <see cref="IMathExpression"/> that is a clone of this instance.</returns>
        public override IMathExpression Clone()
        {
            return new Mul(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

    }

}

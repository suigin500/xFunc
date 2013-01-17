﻿// Copyright 2012 Dmitry Kischenko
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
    
    public class CosineMathExpression : TrigonometryMathExpression
    {

        public CosineMathExpression() : base(null) { }

        public CosineMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("cos({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 180;

            return Math.Cos(radian);
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return Math.Cos(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 200;

            return Math.Cos(radian);
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            SineMathExpression sine = new SineMathExpression(firstMathExpression.Clone());
            MultiplicationMathExpression multiplication = new MultiplicationMathExpression(sine, firstMathExpression.Clone().Derivative(variable));
            UnaryMinusMathExpression unMinus = new UnaryMinusMathExpression(multiplication);

            return unMinus;
        }

        public override IMathExpression Clone()
        {
            return new CosineMathExpression(firstMathExpression.Clone());
        }

    }

}
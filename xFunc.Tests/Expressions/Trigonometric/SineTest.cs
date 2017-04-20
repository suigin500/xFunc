﻿// Copyright 2012-2017 Dmitry Kischenko
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
using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Expressionss.Trigonometric
{

    public class SineTest
    {

        [Fact]
        public void ExecuteRadianTest()
        {
            var exp = new Sin(new Number(1));

            Assert.Equal(Math.Sin(1), exp.Execute(AngleMeasurement.Radian));
        }

        [Fact]
        public void ExecuteDegreeTest()
        {
            var exp = new Sin(new Number(1));

            Assert.Equal(Math.Sin(1 * Math.PI / 180), exp.Execute(AngleMeasurement.Degree));
        }

        [Fact]
        public void ExecuteGradianTest()
        {
            var exp = new Sin(new Number(1));

            Assert.Equal(Math.Sin(1 * Math.PI / 200), exp.Execute(AngleMeasurement.Gradian));
        }

        [Fact]
        public void ExecuteComplexNumberTest()
        {
            var complex = new Complex(3, 2);
            var exp = new Sin(new ComplexNumber(complex));
            var result = (Complex)exp.Execute();

            Assert.Equal(Complex.Sin(complex), result);
            Assert.Equal(0.53092108624851986, result.Real, 15);
            Assert.Equal(-3.59056458998578, result.Imaginary, 15);
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Sin(new Number(1));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
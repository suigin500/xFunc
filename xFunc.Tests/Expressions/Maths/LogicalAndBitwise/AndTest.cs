﻿using System;
using xFunc.Maths;
using xFunc.Maths.Expressions.Collections;
using Xunit;

namespace xFunc.Test.Expressions.alAndBitwise
{
    
    public class AndTest
    {

        private Parser parser;
        
        public AndTest()
        {
            parser = new Parser();
        }

        [Fact]
        public void CalculateTest()
        {
            var exp = parser.Parse("a & b");
            var parameters = new ParameterCollection();
            parameters.Add("a");
            parameters.Add("b");

            parameters["a"] = true;
            parameters["b"] = true;
            Assert.True((bool)exp.Calculate(parameters));

            parameters["a"] = true;
            parameters["b"] = false;
            Assert.False((bool)exp.Calculate(parameters));

            parameters["a"] = false;
            parameters["b"] = true;
            Assert.False((bool)exp.Calculate(parameters));

            parameters["a"] = false;
            parameters["b"] = false;
            Assert.False((bool)exp.Calculate(parameters));
        }

    }
}
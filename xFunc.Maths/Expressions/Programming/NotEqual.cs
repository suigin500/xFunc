﻿// Copyright 2012-2018 Dmitry Kischenko
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
using System.Diagnostics.CodeAnalysis;
using xFunc.Maths.Analyzers;

namespace xFunc.Maths.Expressions.Programming
{

    /// <summary>
    /// Represents the inequality operator.
    /// </summary>
    public class NotEqual : BinaryExpression
    {

        [ExcludeFromCodeCoverage]
        internal NotEqual() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotEqual"/> class.
        /// </summary>
        /// <param name="left">The left (first) operand.</param>
        /// <param name="right">The right (second) operand.</param>
        public NotEqual(IExpression left, IExpression right) : base(left, right) { }
        
        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public override object Execute(ExpressionParameters parameters)
        {
            var leftValue = m_left.Execute(parameters);
            var rightValue = m_right.Execute(parameters);

            if (leftValue is double leftDouble && rightValue is double rightDouble)
                return leftDouble != rightDouble;

            if (leftValue is bool leftBool && rightValue is bool rightBool)
                return leftBool != rightBool;

            throw new NotSupportedException();
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        public override TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            return analyzer.Analyze(this);
        }

        /// <summary>
        /// Creates the clone of this instance.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="NotEqual" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Equal(m_left.Clone(), m_right.Clone());
        }
        
    }

}

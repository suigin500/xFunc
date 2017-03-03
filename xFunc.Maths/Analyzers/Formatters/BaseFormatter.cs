// Copyright 2012-2017 Dmitry Kischenko
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
using System.Text;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Maths.Analyzers.Formatters
{

    public abstract class BaseFormatter : IFormatter
    {

        protected string ToString(UnaryExpression exp, string format)
        {
            return string.Format(format, exp.Argument.Analyze(this));
        }

        protected string ToString(BinaryExpression exp, string format)
        {
            return string.Format(format, exp.Left.Analyze(this), exp.Right.Analyze(this));
        }

        protected string ToString(DifferentParametersExpression exp, string function)
        {
            var sb = new StringBuilder();

            sb.Append(function).Append('(');
            if (exp.Arguments != null)
            {
                foreach (var item in exp.Arguments)
                    sb.Append(item).Append(", ");
                sb.Remove(sb.Length - 2, 2);
            }
            sb.Append(')');

            return sb.ToString();
        }

        public abstract string Analyze(Derivative exp);
        public abstract string Analyze(Exp exp);
        public abstract string Analyze(Floor exp);
        public abstract string Analyze(Lb exp);
        public abstract string Analyze(Lg exp);
        public abstract string Analyze(Log exp);
        public abstract string Analyze(Mul exp);
        public abstract string Analyze(Pow exp);
        public abstract string Analyze(Round exp);
        public abstract string Analyze(Sqrt exp);
        public abstract string Analyze(UnaryMinus exp);
        public abstract string Analyze(UserFunction exp);
        public abstract string Analyze(DelegateExpression exp);
        public abstract string Analyze(Matrix exp);
        public abstract string Analyze(Inverse exp);
        public abstract string Analyze(ComplexNumber exp);
        public abstract string Analyze(Im exp);
        public abstract string Analyze(Re exp);
        public abstract string Analyze(Arccos exp);
        public abstract string Analyze(Arccsc exp);
        public abstract string Analyze(Arcsin exp);
        public abstract string Analyze(Cos exp);
        public abstract string Analyze(Csc exp);
        public abstract string Analyze(Sin exp);
        public abstract string Analyze(Arcosh exp);
        public abstract string Analyze(Arcsch exp);
        public abstract string Analyze(Arsinh exp);
        public abstract string Analyze(Cosh exp);
        public abstract string Analyze(Csch exp);
        public abstract string Analyze(Sinh exp);
        public abstract string Analyze(Avg exp);
        public abstract string Analyze(Max exp);
        public abstract string Analyze(Product exp);
        public abstract string Analyze(Stdevp exp);
        public abstract string Analyze(Var exp);
        public abstract string Analyze(Expressions.LogicalAndBitwise.And exp);
        public abstract string Analyze(Equality exp);
        public abstract string Analyze(NAnd exp);
        public abstract string Analyze(Not exp);
        public abstract string Analyze(XOr exp);
        public abstract string Analyze(Expressions.Programming.And exp);
        public abstract string Analyze(DivAssign exp);
        public abstract string Analyze(For exp);
        public abstract string Analyze(GreaterThan exp);
        public abstract string Analyze(Inc exp);
        public abstract string Analyze(LessThan exp);
        public abstract string Analyze(NotEqual exp);
        public abstract string Analyze(SubAssign exp);
        public abstract string Analyze(While exp);
        public abstract string Analyze(Expressions.Programming.Or exp);
        public abstract string Analyze(MulAssign exp);
        public abstract string Analyze(LessOrEqual exp);
        public abstract string Analyze(If exp);
        public abstract string Analyze(GreaterOrEqual exp);
        public abstract string Analyze(Equal exp);
        public abstract string Analyze(Dec exp);
        public abstract string Analyze(AddAssign exp);
        public abstract string Analyze(Expressions.LogicalAndBitwise.Or exp);
        public abstract string Analyze(NOr exp);
        public abstract string Analyze(Implication exp);
        public abstract string Analyze(Bool exp);
        public abstract string Analyze(Varp exp);
        public abstract string Analyze(Sum exp);
        public abstract string Analyze(Stdev exp);
        public abstract string Analyze(Min exp);
        public abstract string Analyze(Count exp);
        public abstract string Analyze(Tanh exp);
        public abstract string Analyze(Sech exp);
        public abstract string Analyze(Coth exp);
        public abstract string Analyze(Artanh exp);
        public abstract string Analyze(Arsech exp);
        public abstract string Analyze(Arcoth exp);
        public abstract string Analyze(Tan exp);
        public abstract string Analyze(Sec exp);
        public abstract string Analyze(Cot exp);
        public abstract string Analyze(Arctan exp);
        public abstract string Analyze(Arcsec exp);
        public abstract string Analyze(Arccot exp);
        public abstract string Analyze(Reciprocal exp);
        public abstract string Analyze(Phase exp);
        public abstract string Analyze(Conjugate exp);
        public abstract string Analyze(Transpose exp);
        public abstract string Analyze(Determinant exp);
        public abstract string Analyze(Vector exp);
        public abstract string Analyze(Variable exp);
        public abstract string Analyze(Undefine exp);
        public abstract string Analyze(Sub exp);
        public abstract string Analyze(Simplify exp);
        public abstract string Analyze(Root exp);
        public abstract string Analyze(Number exp);
        public abstract string Analyze(Mod exp);
        public abstract string Analyze(Ln exp);
        public abstract string Analyze(LCM exp);
        public abstract string Analyze(GCD exp);
        public abstract string Analyze(Fact exp);
        public abstract string Analyze(Div exp);
        public abstract string Analyze(Del exp);
        public abstract string Analyze(Ceil exp);
        public abstract string Analyze(Define exp);
        public abstract string Analyze(Add exp);
        public abstract string Analyze(Abs exp);

    }

}

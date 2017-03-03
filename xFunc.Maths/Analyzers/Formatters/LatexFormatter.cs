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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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

    /// <summary>
    /// Converts expressions into LaTeX string.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Analyzers.Formatters.BaseFormatter" />
    public class LatexFormatter : BaseFormatter
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LatexFormatter"/> class.
        /// </summary>
        public LatexFormatter() { }

        #region Standard

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Abs exp)
        {
            return ToString(exp, "abs({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Add exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is Add))
                return ToString(exp, "({0} + {1})");

            return ToString(exp, "{0} + {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Ceil exp)
        {
            return ToString(exp, "ceil({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Define exp)
        {
            return $"{exp.Key.Analyze(this)} := {exp.Value.Analyze(this)}";
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Del exp)
        {
            return ToString(exp, "del({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Derivative exp)
        {
            return ToString(exp, "deriv");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Div exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} / {1})");

            return ToString(exp, "{0} / {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Exp exp)
        {
            return ToString(exp, "exp({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Fact exp)
        {
            return ToString(exp, "{0}!");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Floor exp)
        {
            return ToString(exp, "floor({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(GCD exp)
        {
            return ToString(exp, "gcd");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Lb exp)
        {
            return ToString(exp, "lb({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(LCM exp)
        {
            return ToString(exp, "lcm");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Lg exp)
        {
            return ToString(exp, "lg({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Ln exp)
        {
            return ToString(exp, "ln({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Log exp)
        {
            return ToString(exp, "log({0}, {1})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Mod exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} % {1})");

            return ToString(exp, "{0} % {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Mul exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is Mul || exp.Parent is Add || exp.Parent is Sub))
                return ToString(exp, "({0} * {1})");

            return ToString(exp, "{0} * {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Number exp)
        {
            if (exp.Value < 0)
            {
                var sub = exp.Parent as Sub;
                if (sub != null && ReferenceEquals(sub.Right, exp))
                    return $"({exp.Value.ToString(CultureInfo.InvariantCulture)})";
            }

            return exp.Value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Pow exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is Add || exp.Parent is Sub || exp.Parent is Mul))
                return ToString(exp, "({0} ^ {1})");

            return ToString(exp, "{0} ^ {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Root exp)
        {
            return ToString(exp, "root({0}, {1})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Round exp)
        {
            return ToString(exp, "round");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Simplify exp)
        {
            return ToString(exp, "simplify({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Sqrt exp)
        {
            return ToString(exp, "sqrt({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Sub exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is Sub))
                return ToString(exp, "({0} - {1})");

            return ToString(exp, "{0} - {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(UnaryMinus exp)
        {
            if (exp.Argument is BinaryExpression)
                return ToString(exp, "-({0})");
            var sub = exp.Parent as Sub;
            if (sub != null && ReferenceEquals(sub.Right, exp))
                return ToString(exp, "(-{0})");

            return ToString(exp, "-{0}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Undefine exp)
        {
            return $"undef({exp.Key.Analyze(this)})";
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(UserFunction exp)
        {
            var sb = new StringBuilder();

            sb.Append(exp.Function).Append('(');
            if (exp.Arguments != null && exp.Arguments.Length > 0)
            {
                foreach (var item in exp.Arguments)
                    sb.Append(item).Append(", ");
                sb.Remove(sb.Length - 2, 2);
            }
            else if (exp.ParametersCount > 0)
            {
                for (int i = 1; i <= exp.ParametersCount; i++)
                    sb.AppendFormat("x{0}, ", i);
                sb.Remove(sb.Length - 2, 2);
            }
            sb.Append(')');

            return sb.ToString();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Variable exp)
        {
            return exp.Name;
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        [ExcludeFromCodeCoverage]
        public override string Analyze(DelegateExpression exp)
        {
            return "{Delegate Expression}";
        }

        #endregion Standard

        #region Matrix

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Vector exp)
        {
            var sb = new StringBuilder();

            sb.Append('{');
            foreach (var item in exp.Arguments)
                sb.Append(item).Append(", ");
            sb.Remove(sb.Length - 2, 2).Append('}');

            return sb.ToString();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Matrix exp)
        {
            var sb = new StringBuilder();

            sb.Append('{');
            foreach (var item in exp.Arguments)
                sb.Append(item).Append(", ");
            sb.Remove(sb.Length - 2, 2).Append('}');

            return sb.ToString();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Determinant exp)
        {
            return ToString(exp, "det({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Inverse exp)
        {
            return ToString(exp, "inverse({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Transpose exp)
        {
            return ToString(exp, "transpose({0})");
        }

        #endregion Matrix

        #region Complex Numbers

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(ComplexNumber exp)
        {
            var complex = exp.Value;
            if (complex.Real == 0)
            {
                if (complex.Imaginary == 1)
                    return "i";
                if (complex.Imaginary == -1)
                    return "-i";

                return $"{complex.Imaginary.ToString(CultureInfo.InvariantCulture)}i";
            }

            if (complex.Imaginary == 0)
                return $"{complex.Real.ToString(CultureInfo.InvariantCulture)}";

            if (complex.Imaginary > 0)
                return $"{complex.Real.ToString(CultureInfo.InvariantCulture)}+{complex.Imaginary.ToString(CultureInfo.InvariantCulture)}i";

            return $"{complex.Real.ToString(CultureInfo.InvariantCulture)}{complex.Imaginary.ToString(CultureInfo.InvariantCulture)}i";
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Conjugate exp)
        {
            return ToString(exp, "conjugate({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Im exp)
        {
            return ToString(exp, "im({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Phase exp)
        {
            return ToString(exp, "phase({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Re exp)
        {
            return ToString(exp, "re({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Reciprocal exp)
        {
            return ToString(exp, "reciprocal({0})");
        }

        #endregion Complex Numbers

        #region Trigonometric

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Arccos exp)
        {
            return ToString(exp, "arccos({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Arccot exp)
        {
            return ToString(exp, "arccot({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Arccsc exp)
        {
            return ToString(exp, "arccsc({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Arcsec exp)
        {
            return ToString(exp, "arcsec({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Arcsin exp)
        {
            return ToString(exp, "arcsin({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Arctan exp)
        {
            return ToString(exp, "arctan({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Cos exp)
        {
            return ToString(exp, "cos({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Cot exp)
        {
            return ToString(exp, "cot({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Csc exp)
        {
            return ToString(exp, "csc({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Sec exp)
        {
            return ToString(exp, "sec({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Sin exp)
        {
            return ToString(exp, "sin({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Tan exp)
        {
            return ToString(exp, "tan({0})");
        }


        #endregion

        #region Hyperbolic

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Arcosh exp)
        {
            return ToString(exp, "arcosh({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Arcoth exp)
        {
            return ToString(exp, "arcoth({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Arcsch exp)
        {
            return ToString(exp, "arcsch({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Arsech exp)
        {
            return ToString(exp, "arsech({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Arsinh exp)
        {
            return ToString(exp, "arsinh({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Artanh exp)
        {
            return ToString(exp, "artanh({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Cosh exp)
        {
            return ToString(exp, "cosh({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Coth exp)
        {
            return ToString(exp, "coth({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Csch exp)
        {
            return ToString(exp, "csch({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Sech exp)
        {
            return ToString(exp, "sech({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Sinh exp)
        {
            return ToString(exp, "sinh({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Tanh exp)
        {
            return ToString(exp, "tanh({0})");
        }

        #endregion Hyperbolic

        #region Statistical

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Avg exp)
        {
            return ToString(exp, "avg");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expresion.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Count exp)
        {
            return ToString(exp, "count");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Max exp)
        {
            return ToString(exp, "max");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Min exp)
        {
            return ToString(exp, "min");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Product exp)
        {
            return ToString(exp, "product");
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Stdev exp)
        {
            return ToString(exp, "stdev");
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Stdevp exp)
        {
            return ToString(exp, "stdevp");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Sum exp)
        {
            return ToString(exp, "sum");
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Var exp)
        {
            return ToString(exp, "var");
        }

        /// <summary>
        /// Analyzes the specified exppression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Varp exp)
        {
            return ToString(exp, "varp");
        }

        #endregion Statistical

        #region Logical and Bitwise

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Expressions.LogicalAndBitwise.And exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} and {1})");

            return ToString(exp, "{0} and {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Bool exp)
        {
            return exp.Value.ToString();
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Equality exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} <=> {1})");

            return ToString(exp, "{0} <=> {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Implication exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} => {1})");

            return ToString(exp, "{0} => {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(NAnd exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} nand {1})");

            return ToString(exp, "{0} nand {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(NOr exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} nor {1})");

            return ToString(exp, "{0} nor {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Not exp)
        {
            return ToString(exp, "not({0})");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Expressions.LogicalAndBitwise.Or exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} or {1})");

            return ToString(exp, "{0} or {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(XOr exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} xor {1})");

            return ToString(exp, "{0} xor {1}");
        }

        #endregion Logical and Bitwise

        #region Programming

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(AddAssign exp)
        {
            return ToString(exp, "{0} += {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Expressions.Programming.And exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} && {1})");

            return ToString(exp, "{0} && {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Dec exp)
        {
            return ToString(exp, "{0}--");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(DivAssign exp)
        {
            return ToString(exp, "{0} /= {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Equal exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is While))
                return ToString(exp, "({0} == {1})");

            return ToString(exp, "{0} == {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(For exp)
        {
            return ToString(exp, "for");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(GreaterOrEqual exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is While))
                return ToString(exp, "({0} >= {1})");

            return ToString(exp, "{0} >= {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(GreaterThan exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} > {1})");

            return ToString(exp, "{0} > {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(If exp)
        {
            return ToString(exp, "if");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Inc exp)
        {
            return ToString(exp, "{0}++");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(LessOrEqual exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is While))
                return ToString(exp, "({0} <= {1})");

            return ToString(exp, "{0} <= {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(LessThan exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} < {1})");

            return ToString(exp, "{0} < {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(MulAssign exp)
        {
            return ToString(exp, "{0} *= {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(NotEqual exp)
        {
            if (exp.Parent is BinaryExpression && !(exp.Parent is While))
                return ToString(exp, "({0} != {1})");

            return ToString(exp, "{0} != {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(Expressions.Programming.Or exp)
        {
            if (exp.Parent is BinaryExpression)
                return ToString(exp, "({0} || {1})");

            return ToString(exp, "{0} || {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(SubAssign exp)
        {
            return ToString(exp, "{0} -= {1}");
        }

        /// <summary>
        /// Analyzes the specified expression.
        /// </summary>
        /// <param name="exp">The expression.</param>
        /// <returns>The result of analysis.</returns>
        public override string Analyze(While exp)
        {
            return ToString(exp, "while({0}, {1})");
        }

        #endregion Programming

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ByndyuTask
{
    public class Calculator : DeixtraEngine
    {

        public Calculator()
        {
            
            Operations = new Dictionary<string, Operation>()
            {
                {"+", new Operation(1, (a, b) => a + b)},
                {"-", new Operation(1, (a, b) => a - b)},
                {"*", new Operation(2, (a, b) => a*b)},
                {
                    "/", new Operation(2, (a, b) =>
                    {
                        if (b == 0)
                            throw new Exception("Деление на ноль невозможно");
                        return a/b;
                    })
                }
            };
        }

        public double Calculate(string expression)
        {
           
            var normalizedExpression = NormolizeString(expression);
            if (!RightBrackets(normalizedExpression))
                throw new Exception("Неверная скобочная структура");
            return SolveExpression(normalizedExpression);
        }

        private bool RightBrackets(string expression)
        {

            var brackets = new Stack<String>();
            foreach (var s in expression.Split())
            {
                if (s == "(")
                    brackets.Push(s);
                if (s == ")")
                {
                    if (brackets.Count == 0 || brackets.Peek() != "(")
                        return false;
                    brackets.Pop();
                }
            }

            return brackets.Count == 0;
        }

        private string NormolizeString(string str)
        {

            var res = Operations
                .Keys
                .Aggregate(str, (current, key) => current.Replace(key, " " + key + " "));
            res = res.Replace("(", " ( ");
            res = res.Replace(")", " ) ");
            res = "( " + res + " )";
            
            //Удаляем лишние пробелы
            int len;
            do
            {
                len = res.Length;
                res = res.Replace("  ", " ");
            } while (len != res.Length);

            //Унарный минус
            res = res.Replace("( - ", "( -")
                .Substring(1,res.Length-3);//удаляем добавленные скобки
            
            return res;
        }



        [TestCase("0 + 1",  "0 + 1")]
        [TestCase("5+ 7",  "5 + 7")]
        [TestCase("15 +  1 + 45",  "15 + 1 + 45")]
        public void NormalizedInputString(string ex, string result)
        {
             Assert.That(NormolizeString(ex).Contains(result));
        }

        [Test]
        public void FindPostfixExpression()
        {
            var postfix = new Calculator();
            var res = "a b + c * f * f a b c * - / -";
            var rr = new Stack<string>(res.Split().Reverse());

            Assert.AreEqual(postfix.GetPostfix("( a + b ) * c * f - f / ( a - b * c )".Split()), rr);
        }
    }


    public class CalculatorShould
    {
        [TestCase("0", Result = 0)]
        [TestCase("5", Result = 5)]
        [TestCase("-5", Result = -5)]
        public double ReturnNumber(string ex)
        {
            var calc = new Calculator();
            return calc.Calculate(ex);
        }

        [TestCase("0 + 1", Result = 1)]
        [TestCase("5+ 7", Result = 12)]
        [TestCase("15 +  1 + 45", Result = 61)]
        public double FindSum(string ex)
        {
            var calc = new Calculator();
            return calc.Calculate(ex);
        }

        [TestCase("0 - 1", Result = -1)]
        [TestCase("5 - 2", Result = 3)]
        public double FindSub(string ex)
        {
            var calc = new Calculator();
            return calc.Calculate(ex);
        }

        [TestCase("0 * 1", Result = 0)]
        [TestCase("5* 7", Result = 35)]
        public double FindMul(string ex)
        {
            var calc = new Calculator();
            return calc.Calculate(ex);
        }

        [TestCase("0 / 1", Result = 0)]
        [TestCase("5 / 2", Result = 2.5)]
        public double FindDiv(string ex)
        {
            var calc = new Calculator();
            return calc.Calculate(ex);
        }

        [Test]
        public void NotDivZero()
        {
            var calc = new Calculator();
            Assert.That(() => { calc.Calculate("1/0"); }, Throws.TypeOf<Exception>());
        }

        [TestCase("1++3")]
        [TestCase("(1*)")]
        [TestCase("*7")]
        public void NotCalculateWrongExpression(string ex)
        {
            var calc = new Calculator();
            Assert.That(() => { calc.Calculate(ex); }, Throws.TypeOf<Exception>());
        }

        [TestCase("((1+1)")]
        [TestCase("((1+1)))")]
        [TestCase("((1+1))(")]
        public void CheckBrackets(string ex)
        {
            var calc = new Calculator();
            Assert.That(() => { calc.Calculate(ex); }, Throws.TypeOf<Exception>());
        }

        [TestCase("(0 + 1)", Result = 1)]
        [TestCase("5 * ( 7 - 1 )", Result = 30)]
        [TestCase("15 + ( 1 + 45 ) / 2", Result = 38)]
        public double FindExpressions(string ex)
        {
            var calc = new Calculator();
            return calc.Calculate(ex);
        }
    }
}
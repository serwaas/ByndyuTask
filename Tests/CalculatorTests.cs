using System;
using ByndyuTask;
using Ninject;
using NUnit.Framework;

namespace Tests
{
    public class CalculatorTests
    {

        private Calculator calc;
        
        public CalculatorTests()
        {
            var kernel = new StandardKernel(new ArithmeticalCalculatorModule());
            calc = kernel.Get<Calculator>();
        }

        [TestCase("0", Result = 0)]
        [TestCase("5", Result = 5)]
        [TestCase("-5", Result = -5)]
        public double ReturnNumber(string ex)
        {
            return calc.Calculate(ex);
        }

        [TestCase("0 + 1", Result = 1)]
        [TestCase("5 + 7", Result = 12)]
        [TestCase("15 +  1 + 45", Result = 61)]
        public double FindSum(string ex)
        {
            return calc.Calculate(ex);
        }

        [TestCase("0 - 1", Result = -1)]
        [TestCase("5 - 2", Result = 3)]
        public double FindSub(string ex)
        {
            return calc.Calculate(ex);
        }

        [TestCase("0 * 1", Result = 0)]
        [TestCase("5 * 7", Result = 35)]
        public double FindMul(string ex)
        {
            return calc.Calculate(ex);
        }

        [TestCase("0 / 1", Result = 0)]
        [TestCase("5 / 2", Result = 2.5)]
        public double FindDiv(string ex)
        {
            return calc.Calculate(ex);
        }

        [Test]
        public void NotDivZero()
        {
            Assert.That(() => { calc.Calculate("1 / 0"); }, Throws.TypeOf<Exception>());
        }

        [TestCase("1++3")]
        [TestCase("(1*)")]
        [TestCase("*7")]
        public void NotCalculateWrongExpression(string ex)
        {
            Assert.That(() => { calc.Calculate(ex); }, Throws.TypeOf<Exception>());
        }
        
        [TestCase("(0 + 1)", Result = 1)]
        [TestCase("5 * ( 7 - 1 )", Result = 30)]
        [TestCase("15 + ( 1 + 45 ) / 2", Result = 38)]
        public double FindExpressions(string ex)
        {
            return calc.Calculate(ex);
        }
    }
}

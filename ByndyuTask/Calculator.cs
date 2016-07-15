using System;



namespace ByndyuTask
{
    public class Calculator
    {
        private readonly IExpressionNormalizer Normalizer;
        private readonly ICalculatorEngine CalculatorEngine;
        private readonly IExpressionChecker Checker;

        public Calculator(ICalculatorEngine calculator, IExpressionNormalizer normalizer, IExpressionChecker checker)
        {

            Normalizer = normalizer;
            CalculatorEngine = calculator;
            Checker = checker;
        }

        public double Calculate(string expression)
        {

            var normEx = Normalizer.Normalize(expression);
            if (!Checker.CheckExpression(normEx))
                throw new Exception("Ошибка в записи выражения");

            return CalculatorEngine.SolveExpression(normEx);
        }
    }
}
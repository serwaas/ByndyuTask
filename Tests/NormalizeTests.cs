using ByndyuTask;
using NUnit.Framework;

namespace Tests
{
    class NormalizeTests
    {

        private ArithmeticalNormalizer Normalizer = new ArithmeticalNormalizer();

        [TestCase("-1", Result = " _ 1 ")]
        [TestCase("(1/0)", Result = " ( 1 / 0 ) ")]
        [TestCase("-(22* (-12-78   ) )", Result = " _ ( 22 * ( _ 12 - 78 ) ) ")]
        public string NormolizeExpression(string ex)
        {
            return Normalizer.Normalize(ex);
        }
    }
}

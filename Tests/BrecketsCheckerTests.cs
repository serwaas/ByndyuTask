using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ByndyuTask;

namespace Tests
{
    class BrecketsCheckerTests
    {

        private BracketsChecker checker = new BracketsChecker();
        

        [Test]
        public void CheckRightBrackets()
        {

            Assert.AreEqual(checker.CheckExpression("( )"), true);
        }

        [Test]
        public void CheckWAlownBracket()
        {

            Assert.AreEqual(checker.CheckExpression("("), false);
        }

        [TestCase("( )", Result = true)]
        [TestCase("( ) ( )", Result = true)]
        [TestCase("( ( ) )", Result = true)]
        public bool CheckExpression(string ex)
        {
            return checker.CheckExpression(ex);
        }

        [TestCase("( ) )", Result = false)]
        [TestCase("( ( )", Result = false)]
        [TestCase(") (", Result = false)]
        public bool CheckWrongExpression(string ex)
        {
            return checker.CheckExpression(ex);
        }
    }
}

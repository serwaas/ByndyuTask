using System.Collections.Generic;

namespace ByndyuTask
{
    public class BracketsChecker : IExpressionChecker
    {
        public bool CheckExpression(string expression)
        {

            var brackets = new Stack<string>();
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
    }
}
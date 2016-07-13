using System;
using System.Collections.Generic;
using System.Linq;

namespace ByndyuTask
{
    //Алгоритм Дейкстра. Вычисление выражения по постфиксной записи
    public abstract class DeixtraEngine
    {

        protected Dictionary<string, Operation> Operations;

        protected double SolveExpression(string exp)
        {
            var postfix = GetPostfix(exp.Split().Where(a => a != "").ToArray());
            if (postfix.Count == 0)
                throw new Exception("Неверное выражение");

            return GetResult(postfix);
        }
        protected Stack<string> GetPostfix(string[] expression)
        {

            var res = new Stack<string>();
            var tmp = new Stack<string>();

            //просматриваем выражение
            //числа кладем в результирующий стек, операции и скобки - в tmp
            foreach (var s in expression)

                switch (s)
                {
                    case "(":
                        tmp.Push(s);
                        break;
                    case ")": 
                        do
                        {
                            var value = tmp.Pop();
                            if (value == "(")
                                break;
                            res.Push(value);
                        } while (true);
                        break;

                    default:
                        if (Operations.ContainsKey(s))
                        {
                            //кладем в результирующий стек из tmp опереции с большим или равным приоритетом, 
                            //пока не встретим '(' или стек не опустеет
                            var lessOp = LessPriority(s);
                            while (tmp.Count != 0 && tmp.Peek() != "(" && !lessOp.Contains(tmp.Peek()))
                            {
                                res.Push(tmp.Pop());
                            }
                            
                            tmp.Push(s);
                            break;
                        }
                        res.Push(s);
                        break;
                }
            while (tmp.Count != 0)
                res.Push(tmp.Pop());
            
            //полученный стек записан в обратном порядке, поэтому разворачиваем
            return new Stack<string>(res);
        }

        protected List<string> LessPriority(string operation)
        {
            var priority = Operations[operation].Priority;
            return (from op in Operations where op.Value.Priority < priority select op.Key).ToList();
        }

        protected double GetResult(Stack<string> postfix)
        {

            var tmp = new Stack<string>();
            while (postfix.Count != 0)
            {
                var p = postfix.Pop();
                //встретив операцию применяем ее для двух верхних значений в стеке
                if (Operations.ContainsKey(p))
                {
                    double a, b;
                    if (tmp.Count < 2 || !double.TryParse(tmp.Pop(), out b) ||
                        !double.TryParse(tmp.Pop(), out a))
                        throw new Exception("Неверное выражение");
                    var r = Operations[p].Function(a, b);
                    tmp.Push(r.ToString());
                }
                else
                    tmp.Push(p);
            }
            return double.Parse(tmp.Peek());
        }


    }
}
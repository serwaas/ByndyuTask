using System;
using System.Collections.Generic;
using System.Linq;

namespace ByndyuTask
{
    public class CalculatorEngine : ICalculatorEngine 
    {

        private Dictionary<string, Operation> Operations { get; set; }

        public CalculatorEngine()
        {

            Operations = new Dictionary<string, Operation>()
            {
                {"+", new Operation(1, 2, (a) => a[0] + a[1])},
                {"-", new Operation(1, 2, (a) => a[0] - a[1])},
                {"_", new Operation(2, 1, (a) => -1*a[0])},
                {"*", new Operation(2, 2, (a) => a[0]*a[1])},
                {
                    "/", new Operation(2, 2, (a) =>
                    {
                        if (a[1] == 0)
                            throw new Exception("������� �� ���� ����������");
                        
                        return a[0]/a[1];
                    })
                }
            };
        }

        //�������� ��������. ���������� ��������� �� ����������� ������
        public double SolveExpression(string expression)
        {
            
            var postfix = GetPostfix(expression.Split().Where(a => a != "").ToArray());
            if (postfix.Count == 0)
                throw new Exception("�������� ���������");
            
            return GetResult(postfix);
        }

        private Stack<string> GetPostfix(string[] expression)
        {

            var res = new Stack<string>();
            var tmp = new Stack<string>();

            //������������� ���������
            //����� �������� � �������������� ����, �������� � ������ - � tmp
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
                            //�������� � �������������� ���� �� tmp �������� � ������� ��� ������ �����������, 
                            //���� �� �������� '(' ��� ���� �� ��������
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
            
            //���������� ���� ������� � �������� �������, ������� �������������
            return new Stack<string>(res);
        }

        private List<string> LessPriority(string operation)
        {

            var priority = Operations[operation].Priority;
            return (from op in Operations where op.Value.Priority < priority select op.Key).ToList();
        }

        private double GetResult(Stack<string> postfix)
        {

            var tmp = new Stack<string>();
            while (postfix.Count != 0)
            {
                var p = postfix.Pop();
                //�������� �������� ��������� �� ��� ���� ������� �������� � �����
                if (Operations.ContainsKey(p))
                {
                    var args = new double[Operations[p].NumberOfArguments];
                    try
                    {
                        for (int i = 0; i < args.Count(); i++)
                        {
                            if (!double.TryParse(tmp.Pop(), out args[i]))
                                throw new Exception();
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("�������� ���������");
                    }

                    args  = args.Reverse().ToArray();
                    var r = Operations[p].Function(args);
                    tmp.Push(r.ToString());
                }
                else
                    tmp.Push(p);
            }

            return double.Parse(tmp.Peek());
        }
    }
}
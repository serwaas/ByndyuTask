using System.Collections.Generic;
using System.Linq;

namespace ByndyuTask
{
    public class ArithmeticalNormalizer : IExpressionNormalizer
    {
        private List<string> Operations { get; set; }

        public ArithmeticalNormalizer()
        {
            Operations = new List<string>() {"+", "-", "*", "/",")","("};
        }

        public string Normalize(string expression)
        {
            var res = Operations.Aggregate(expression, (current, op) => current.Replace(op, " " + op + " "));

            res = "( " + res + " )";

            //Удаляем лишние пробелы
            int len;
            do
            {
                len = res.Length;
                res = res.Replace("  ", " ");
            } while (len != res.Length);

            //Унарный минус
            res = res.Replace("( - ", "( _ ")
                .Substring(1, res.Length - 2);//удаляем добавленные скобки

            return res;
        }
    }
}
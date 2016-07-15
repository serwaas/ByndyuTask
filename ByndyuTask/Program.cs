using System;
using Ninject;


namespace ByndyuTask
{
    public class Program
    {
        public static void Main()
        {

            Console.WriteLine("[Тестовое задание: Калькулятор]\n" +
                              "[Выполнил Сергей Васнин]\n\n" +
                              "[Для выхода введите q]\n\n");

            var kernel = new StandardKernel(new ArithmeticalCalculatorModule());
            var calc = kernel.Get<Calculator>();
            
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите выражение:");
                    var input = Console.ReadLine();
                    if (input == "q")
                        break;

                    var ans = calc.Calculate(input);
                    Console.WriteLine("\tОтвет: " + ans);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
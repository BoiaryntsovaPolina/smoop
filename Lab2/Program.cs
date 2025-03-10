using System.Diagnostics;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double x_start = 0.0, x_end = 2.0, dx = 0.05;

            Console.WriteLine(" a |    x    |    y(x) ");
            Console.WriteLine("-----------------------");

            for (double a = 0.1; a <= 0.4; a += 0.1)
            {
                for (double x = x_start; x <= x_end; x += dx)
                {
                    double denominator = Math.Sqrt(Math.Pow((1 - Math.Pow(x, 2)), 2) + 4 * Math.Pow(a, 2) * Math.Pow(x, 2));
                    double y;

                    if (denominator != 0)
                        y = 1.0 / denominator;
                    else
                    {
                        y = 0; 
                        Console.WriteLine($"Помилка: знаменник = 0 при a = {a}, x = {x}");
                    }

                    Console.WriteLine($"{a,3:F1} | {x,6:F2} | {y,8:F4}");
                }
            }
            Console.ReadKey();
        }
    }
}

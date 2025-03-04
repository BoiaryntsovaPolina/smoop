namespace Lab1Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ініціалізація значень за замовчуванням
            double x = 3.74 * 1E-2, y = -0.825, z = 0.16 * 1E2;

            // Виклик методу для отримання значення
            GetInput("Input x", ref x);
            GetInput("Input y", ref y);
            GetInput("Input z", ref z);

            // Перевірка на ділення на нуль
            double denominator = Math.Abs(x - (2 * y / (1 + Math.Pow(x, 2) * Math.Pow(y, 2))));
            if (denominator == 0)
            {
                Console.WriteLine("Помилка: ділення на нуль!");
                return;
            }

            // Обчислення виразу
            double v = (1 + Math.Pow(Math.Sin(x + y), 2)) / denominator * Math.Pow(x, Math.Abs(y)) + Math.Pow(Math.Cos(Math.Atan(1 / z)), 2);

            // Виведення результату
            Console.WriteLine($"y({x,6:F4}, {y,6:F3}, {z,6:F2}) = {v,7:F4}");
            Console.ReadKey();
        }

        // Метод для введення значень з перевіркою на порожній рядок
        static void GetInput(string message, ref double value)
        {
            Console.Write($"{message} (за умовою {value}): ");
            string input = Console.ReadLine() ?? "";
            value = input == "" ? value : double.Parse(input);
        }

    }
}

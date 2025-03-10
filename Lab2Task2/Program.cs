namespace Lab2Task2
{
    internal class Program
    {

        // Функція для обчислення факторіалу
        static double Factorial(int n)
        {
            double result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        // Функція для обчислення ряду
        static double CalculateSeries(double x, double e, int iterLimit, out bool error)
        {
            double sum = 0;   // Сума ряду
            double term;      // Поточний член ряду
            int n = 1;        // Лічильник ітерацій
            error = false;    // Чи є помилка

            // Перший член ряду
            term = Math.Pow(2, n - 1) * Math.Pow(x, 2 * n) / Factorial(2 * n);
            sum = term;

            // Додавання членів ряду до досягнення точності e
            for (n = 2; Math.Abs(term) > e; n++)
            {
               term *= Math.Pow(2, 2) * Math.Pow(x, 2) / ((2 * n - 1) * (2 * n)); // Виправлена рекурентна формула
                sum += term;

                if (n > iterLimit)
                {
                    error = true;
                    break;
                }
            }

            return sum;
        }



        static void Main(string[] args)
        {
            double e = 1e-6;  // Точність обчислень
            const int iterLimit = 500;  // Обмеження на кількість ітерацій

            // 1️⃣ Користувач вводить значення x
            Console.WriteLine("Введіть значення x (наприклад, 0.5236 для pi/6):");
            double x = Convert.ToDouble(Console.ReadLine());

            // 2️⃣ Обчислюємо ряд для введеного x
            bool error;
            double seriesValue = CalculateSeries(x, e, iterLimit, out error);
            double exactValue = Math.Pow(Math.Sin(x), 2);  // Точне значення через sin^2(x)

            // 3️⃣ Виводимо результати
            Console.WriteLine($"\nРезультати для x = {x}:");
            if (error)
                Console.WriteLine("Ряд розходиться (досягнуто ліміт ітерацій)");
            else
            {
                Console.WriteLine($"Сума ряду: {seriesValue}");
                Console.WriteLine($"Точне значення sin²(x): {exactValue}");
                Console.WriteLine($"Похибка: {Math.Abs(seriesValue - exactValue)}");
            }

            //  Контрольна перевірка для x = π/6
            double controlX = Math.PI / 6;
            double controlSeries = CalculateSeries(controlX, e, iterLimit, out error);
            double controlExact = Math.Pow(Math.Sin(controlX), 2);
            double controlError = Math.Abs(controlSeries - controlExact);

            Console.WriteLine("\nКонтрольна перевірка (x = π/6):");
            if (error)
                Console.WriteLine("Ряд розходиться (досягнуто ліміт ітерацій)");
            else
            {
                Console.WriteLine($"Сума ряду: {controlSeries}");
                Console.WriteLine($"Точне значення sin²(π/6): {controlExact}");
                Console.WriteLine($"Похибка: {controlError}");
            }
            Console.ReadKey();
        }
    }
}


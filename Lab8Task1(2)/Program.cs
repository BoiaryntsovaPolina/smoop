using Lab8Task1_2_;

internal class Program
{
    private static void Main(string[] args)
    {

        Console.WriteLine("\nЧастина 1: відображення часу та дати");
        Console.WriteLine("------------------------------------------------");

        TimeDisplayer timeDisplayer = new TimeDisplayer();

        timeDisplayer.DisplayTimeInfo(timeDisplayer.ShowCurrentTime);
        timeDisplayer.DisplayTimeInfo(timeDisplayer.ShowCurrentDate);
        timeDisplayer.DisplayTimeInfo(timeDisplayer.ShowCurrentDayOfWeek);

        Console.WriteLine("\nЧастина 2: перевірка чисел");
        Console.WriteLine("------------------------------------------------");

        NumberChecker numberChecker = new NumberChecker();

        // Перевіряємо різні числа на простоту та чи є вони числами Фібоначчі
        int[] numbersToCheck = { 2, 7, 8, 13, 15, 17, 21 };

        foreach (int number in numbersToCheck)
        {
            // Перевірка на просте число
            bool isPrime = numberChecker.CheckNumber(number, numberChecker.IsPrime);
            Console.WriteLine("Число " + number + " просте: " + isPrime);

            // Перевірка на число Фібоначчі
            bool isFibonacci = numberChecker.CheckNumber(number, numberChecker.IsFibonacci);
            Console.WriteLine("Число " + number + " є числом Фібоначчі: " + isFibonacci);
            Console.WriteLine();
        }

        Console.WriteLine("\nЧастина 3: обчислення площ");
        Console.WriteLine("------------------------------------------------");

        AreaCalculator areaCalculator = new AreaCalculator();

        double triangleArea1 = areaCalculator.CalculateArea(3, 4, areaCalculator.TriangleArea);
        Console.WriteLine("Площа трикутника з основою 3 і висотою 4: " + triangleArea1);

        double rectangleArea1 = areaCalculator.CalculateArea(5, 8, areaCalculator.RectangleArea);
        Console.WriteLine("Площа прямокутника з шириною 5 і висотою 8: " + rectangleArea1);

        Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
        Console.ReadKey();
    }
}
using Lab7Task2_2_;
using Lab7Task2_2_.Utilits;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // Для коректного відображення кирилиці

        // Створюємо об'єкт реєстру
        Registry aeroClubRegistry = new Registry();

        // Заповнюємо реєстр випадковими даними
        TestUtils.GenerateRandomDevices(aeroClubRegistry, 7, 10);

        // Демонструємо всі можливості реєстру
        TestUtils.DemonstrateAllFeatures(aeroClubRegistry);

        Console.WriteLine("\nПрограма завершила роботу. Натисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}
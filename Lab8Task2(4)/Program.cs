using Lab8Task2;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            SuitcaseManager manager = new SuitcaseManager();

            Console.WriteLine("=== ПРОГРАМА УПРАВЛІННЯ ВАЛІЗОЮ ===");
            manager.InteractiveSuitcasePacking();

            Console.WriteLine("\nВаліза зібрана!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Критична помилка: " + ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
    }
}

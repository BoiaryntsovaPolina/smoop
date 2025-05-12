using Lab7Task3;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ICipher aCipher = new ACipher();
        ICipher bCipher = new BCipher();

        // Текст для шифрування
        string originalText = "Привіт світ! Hello World! 123";

        Console.WriteLine("Початковий текст: " + originalText);

        // Шифруємо текст за допомогою ACipher
        string aEncoded = aCipher.encode(originalText);
        Console.WriteLine("\nACipher:");
        Console.WriteLine("Зашифрований текст: " + aEncoded);

        // Дешифруємо текст за допомогою ACipher
        string aDecoded = aCipher.decode(aEncoded);
        Console.WriteLine("Дешифрований текст: " + aDecoded);

        // Шифруємо текст за допомогою BCipher
        string bEncoded = bCipher.encode(originalText);
        Console.WriteLine("\nBCipher:");
        Console.WriteLine("Зашифрований текст: " + bEncoded);

        // Дешифруємо текст за допомогою BCipher
        string bDecoded = bCipher.decode(bEncoded);
        Console.WriteLine("Дешифрований текст: " + bDecoded);

        Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
        Console.ReadKey();
    }
}
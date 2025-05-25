using System.Text.Json;
using System.Text;
using Lab9Task2;

internal class Program
{
    // Дефолтні шляхи до файлів
    private const string DefaultTextFilePath = "input.txt";
    private const string DefaultBannedWordsFilePath = "banned_words.txt";
    private const string DefaultResultFileName = "censor_result.json";

    private static async Task Main(string[] args)
    {

        Console.WriteLine("=== Додаток 'Цензор' ===\n");

        FileHandler fileHandler = new FileHandler();
        TextCensor textCensor = new TextCensor();

        string textFilePath = GetFilePathFromUser("Введіть шлях до файлу з текстом (залиште пустим для 'input.txt'): ", DefaultTextFilePath);
        string bannedWordsFilePath = GetFilePathFromUser("Введіть шлях до файлу зі словами для цензури (залиште пустим для 'banned_words.txt'): ", DefaultBannedWordsFilePath);

        try
        {
            string originalText = fileHandler.ReadTextFromFile(textFilePath);
            string[] bannedWords = fileHandler.ReadBannedWords(bannedWordsFilePath);

            Console.WriteLine($"\nЗнайдено {bannedWords.Length} слів для цензури");
            Console.WriteLine("Початковий текст:");
            Console.WriteLine(originalText);

            string censoredText = textCensor.CensorText(originalText, bannedWords);
            int censoredWordsCount = textCensor.CountCensoredWords(originalText, bannedWords);

            Console.WriteLine("\nТекст після цензури:");
            Console.WriteLine(censoredText);
            Console.WriteLine($"\nЗамінено слів: {censoredWordsCount}");

            CensorResult result = new CensorResult(originalText, censoredText, censoredWordsCount, bannedWords);

            // Асинхронний виклик методу збереження
            await fileHandler.SaveResultToJsonAsync(result, DefaultResultFileName);

            Console.WriteLine($"\nРезультати збережено у файл '{DefaultResultFileName}'");
            Console.WriteLine("Натисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
            Console.WriteLine("Переконайтеся, що вказані файли існують і шлях до них правильний.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Помилка вводу/виводу: {ex.Message}");
            Console.WriteLine("Перевірте дозволи до файлів або спробуйте інше ім'я файлу.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непередбачена помилка: {ex.Message}");
        }
    }

    static string GetFilePathFromUser(string prompt, string defaultValue)
    {
        Console.Write(prompt);
        string input = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine($"Використовується файл за замовчуванням: '{defaultValue}'");
            return defaultValue;
        }
        return input;
    }
}

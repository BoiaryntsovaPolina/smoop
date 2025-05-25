using System.Text.Json;
using Lab9Task1;
internal class Program
{
    // Перевірка голосної
    static bool IsVowel(char c)
    {
        c = char.ToLower(c);
        return "аеєиіїоуюяaeiou".Contains(c);
    }

    // Аналіз тексту
    static Stats Analyze(string path, string text)
    {
        int sentences = 0, upper = 0, lower = 0, vowels = 0, consonants = 0, digits = 0;

        // Визначаємо роздільники речень
        string sentenceDelimiters = ".!?";

        foreach (char c in text)
        {
            if (sentenceDelimiters.Contains(c)) sentences++;
            if (char.IsUpper(c)) upper++;
            if (char.IsLower(c)) lower++;
            if (IsVowel(c)) vowels++;
            if (char.IsLetter(c) && !IsVowel(c)) consonants++;
            if (char.IsDigit(c)) digits++;
        }

        return new Stats(Path.GetFileName(path), sentences, upper, lower, vowels, consonants, digits);
    }

    // Збереження у JSON
    static async Task SaveJsonAsync(Stats stats, string path)
    {
        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                await JsonSerializer.SerializeAsync(fs, stats);
            }
            Console.WriteLine($"\nЗбережено (асинхронно): {path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    // Читання з JSON
    static async Task<Stats> LoadJsonAsync(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
                {
                    Stats stats = await JsonSerializer.DeserializeAsync<Stats>(fs);
                    Console.WriteLine($"\nЗавантажено (асинхронно): {path}");
                    return stats;
                }
            }
            else
            {
                Console.WriteLine("Файл не знайдено.");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
            return null;
        }
    }

    private static async Task Main(string[] args)
    {
        Console.WriteLine("=== Аналіз файлу ===");

        while (true)
        {
            Console.WriteLine("\n1. Аналізувати файл");
            Console.WriteLine("2. Завантажити JSON");
            Console.WriteLine("3. Вийти");
            Console.Write("Вибір: ");

            string choice = Console.ReadLine();

            switch (choice) 
            {
                case "1":
                    AnalyzeFileMenu();
                    break; 
                case "2":
                    LoadFile();
                    break;
                case "3":
                    return; 
                default: 
                    Console.WriteLine("Неправильний вибір!");
                    break;
            }
        }
    }

    static async Task AnalyzeFileMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Вибір файлу для аналізу ---");
            Console.WriteLine($"1. Використати файл за замовчуванням ('{DefaultFileName}')");
            Console.WriteLine("2. Ввести шлях до файлу вручну");
            Console.WriteLine("3. Повернутися до головного меню");
            Console.Write("Вибір: ");

            string choice = Console.ReadLine();
            string filePath = ""; // Змінна для зберігання шляху до файлу

            switch (choice)
            {
                case "1":
                    filePath = DefaultFileName; // Використовуємо наш заздалегідь визначений файл
                    ProcessFile(filePath);
                    return; 
                case "2":
                    Console.Write("\nВведіть повний шлях до файлу: ");
                    filePath = Console.ReadLine();
                    ProcessFile(filePath); 
                    return; 
                case "3":
                    return;
                default:
                    Console.WriteLine("Неправильний вибір!");
                    break;
            }
        }
    }

    const string DefaultFileName = "example.txt"; 

    // Обробка файлу
    static void ProcessFile(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine($"Файл '{Path.GetFileName(path)}' за шляхом '{path}' не існує!");
            return;
        }

        try
        {
            string text = File.ReadAllText(path);

            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine("Файл порожній!");
                return;
            }

            Console.WriteLine($"\n--- Аналіз файлу: {Path.GetFileName(path)} ---");
            Console.WriteLine($"Текст ({text.Length} символів):");
            Console.WriteLine(text);

            Stats stats = Analyze(path, text);
            stats.Show();

            string jsonFileName = Path.GetFileNameWithoutExtension(path) + "_stats.json";
            // Ми збережемо JSON у тому ж каталозі, де знаходиться оригінальний файл, 
            // або, якщо вказаний повний шлях, то поруч з ним.
            string jsonPath = Path.Combine(Path.GetDirectoryName(path) ?? Environment.CurrentDirectory, jsonFileName);
            SaveJsonAsync(stats, jsonPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка при обробці файлу '{Path.GetFileName(path)}': {ex.Message}");
        }
    }

    static async Task LoadFile()
    {
        Console.Write("\nШлях до JSON: ");
        string path = Console.ReadLine();

        Stats stats = await LoadJsonAsync(path);
        if (stats != null)
        {
            stats.Show();
        }
    }
}
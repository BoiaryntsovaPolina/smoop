using Lab10;
using System.Text.Json;
using System.Xml.Serialization;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("=== КЛАС РОБІТНИК ===\n");

        // Створюємо масив робітників
        Worker[] workers = new Worker[]
        {
            new Worker("Іванов Іван Іванович", "Програміст", 25000m, new DateTime(2015, 03, 15)),
            new Worker("Петренко Петро Петрович", "Менеджер", 30000m, new DateTime(2018, 07, 20)),
            new Worker("Сидоренко Сидір Сидорович", "Аналітик", 28000m, new DateTime(2016, 11, 10)),
            new Worker("Коваленко Анна Василівна", "Дизайнер", 22000m, new DateTime(2020, 01, 25)),
            new Worker("Шевченко Олег Миколайович", "Тестувальник", 24000m, new DateTime(2017, 05, 08))
        };

        Console.WriteLine("1. ПОЧАТКОВА ІНФОРМАЦІЯ ПРО РОБІТНИКІВ:");
        Console.WriteLine(new string('-', 80));
        for (int i = 0; i < workers.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {workers[i]}");
        }

        Console.WriteLine("\n2. РОБІТНИКИ ЗІ СТАЖЕМ БІЛЬШЕ 5 РОКІВ:");
        Console.WriteLine(new string('-', 80));

        // Знаходимо робітників зі стажем більше 5 років
        int count = 0;
        for (int i = 0; i < workers.Length; i++)
        {
            if (workers[i].GetWorkExperience() > 5)
            {
                count++;
                Console.WriteLine($"{count}. {workers[i]}");

                // Збільшуємо оклад на 4%
                decimal oldSalary = workers[i].Salary;
                workers[i].IncreaseSalary();
                Console.WriteLine($"   Оклад збільшено: {oldSalary:C} → {workers[i].Salary:C}");
                Console.WriteLine();
            }
        }

        if (count == 0)
        {
            Console.WriteLine("Немає робітників зі стажем більше 5 років.");
        }

        Console.WriteLine("\n3. ІНФОРМАЦІЯ ПІСЛЯ ЗБІЛЬШЕННЯ ОКЛАДІВ:");
        Console.WriteLine(new string('-', 80));
        for (int i = 0; i < workers.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {workers[i]}");
        }

        // XML СЕРІАЛІЗАЦІЯ
        Console.WriteLine("\n4. XML СЕРІАЛІЗАЦІЯ:");
        Console.WriteLine(new string('-', 50));

        // Серіалізація окремого робітника
        Worker singleWorker = workers[0];
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Worker));

        using (FileStream fs = new FileStream("worker.xml", FileMode.Create))
        {
            xmlSerializer.Serialize(fs, singleWorker);
            Console.WriteLine("Робітник серіалізований у worker.xml");
        }

        // Десеріалізація окремого робітника
        using (FileStream fs = new FileStream("worker.xml", FileMode.Open))
        {
            Worker? restoredWorker = xmlSerializer.Deserialize(fs) as Worker;
            Console.WriteLine($"Відновлений робітник: {restoredWorker}");
        }

        // Серіалізація масиву робітників
        XmlSerializer arraySerializer = new XmlSerializer(typeof(Worker[]));

        using (FileStream fs = new FileStream("workers.xml", FileMode.Create))
        {
            arraySerializer.Serialize(fs, workers);
            Console.WriteLine("Масив робітників серіалізований у workers.xml");
        }

        // Десеріалізація масиву робітників
        using (FileStream fs = new FileStream("workers.xml", FileMode.Open))
        {
            Worker[]? restoredWorkers = arraySerializer.Deserialize(fs) as Worker[];
            if (restoredWorkers != null)
            {
                Console.WriteLine($"Відновлено {restoredWorkers.Length} робітників з XML файлу");
            }
        }

        // JSON СЕРІАЛІЗАЦІЯ
        Console.WriteLine("\n5. JSON СЕРІАЛІЗАЦІЯ:");
        Console.WriteLine(new string('-', 50));

        // Серіалізація окремого робітника в JSON (для виводу в консоль)
        string jsonWorker = JsonSerializer.Serialize(singleWorker);
        Console.WriteLine("JSON представлення робітника:");
        Console.WriteLine(jsonWorker);

        // Десеріалізація з JSON рядка
        Worker? restoredFromJson = JsonSerializer.Deserialize<Worker>(jsonWorker);
        Console.WriteLine($"\nВідновлений з JSON: {restoredFromJson}");

        // Серіалізація у файл JSON (асинхронно)
        using (FileStream fs = new FileStream("worker.json", FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(fs, singleWorker);
            Console.WriteLine("Робітник збережений у worker.json");
        }

        // Десеріалізація з файлу JSON (асинхронно)
        using (FileStream fs = new FileStream("worker.json", FileMode.Open))
        {
            Worker? workerFromFile = await JsonSerializer.DeserializeAsync<Worker>(fs);
            Console.WriteLine($"Зчитаний з файлу: {workerFromFile}");
        }

        // Серіалізація масиву у JSON файл
        using (FileStream fs = new FileStream("workers.json", FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(fs, workers);
            Console.WriteLine("Масив робітників збережений у workers.json");
        }

        // Десеріалізація масиву з JSON файлу
        using (FileStream fs = new FileStream("workers.json", FileMode.Open))
        {
            Worker[]? workersFromFile = await JsonSerializer.DeserializeAsync<Worker[]>(fs);
            if (workersFromFile != null)
            {
                Console.WriteLine($"Зчитано {workersFromFile.Length} робітників з JSON файлу");
                Console.WriteLine("Перший робітник з файлу:");
                Console.WriteLine(workersFromFile[0]);
            }
        }

        Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}
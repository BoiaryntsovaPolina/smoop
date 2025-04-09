namespace Lab4Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputText = GetInputText();
            string processedText = ProcessText(inputText);
            Console.WriteLine("\nРезультат:\n" + processedText);
            SaveToFile("output.txt", processedText);
        }

        static string GetInputText()
        {
            Console.WriteLine("Оберіть спосіб введення тексту:");
            Console.WriteLine("1 - Ввести з клавіатури");
            Console.WriteLine("2 - Зчитати з файлу (dat.txt)");
            Console.Write("Ваш вибір: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Введіть текст:");
                    return Console.ReadLine();
                case "2":
                    return ReadFromFile("dat.txt");
                default:
                    return ErrorMessage("Невірний вибір.");
            }
        }

        static string ReadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (StreamReader sRead = new StreamReader(fileName))
                {
                    return sRead.ReadToEnd();
                }
            }
            return ErrorMessage("Файл не знайдено.");
        }

        static string ProcessText(string text)
        {
            string[] words = text.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 4)
                    words[i] = "";
                else if (words[i].Length == 5)
                    words[i] = "комп'ютер";
            }
            return string.Join(" ", words).Trim();
        }

        static void SaveToFile(string fileName, string content)
        {
            using (StreamWriter sWrite = new StreamWriter(fileName))
            {
                sWrite.Write(content);
            }
            Console.WriteLine($"Результат збережено в {fileName}");
        }

        static string ErrorMessage(string message)
        {
            Console.WriteLine(message);
            return "";
        }
    }
}

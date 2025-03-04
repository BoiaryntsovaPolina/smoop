namespace Lab1Task2
{
    internal class Program
    {
        static double CalculateFunction(double x)
        {
            return 2 * Math.PI / (Math.Pow(x, 2) - Math.PI);
        }

        static void Main(string[] args)
        {
            string inputFile = "LAB2.TXT";
            string outputFile = "LAB2.RES";

            try
            {
                if (!File.Exists(inputFile))
                {
                    Console.WriteLine("Файл LAB2.TXT не знайдено.");
                    return;
                }

                using (StreamReader streamReader = new StreamReader(inputFile))
                using (StreamWriter streamWriter = new StreamWriter(outputFile, false)) // false = перезапис файлу
                {
                    streamWriter.WriteLine("+++++++++++++++++++++++++++++++++++");
                    streamWriter.WriteLine("+  Аргумент    +  Функція         +");
                    streamWriter.WriteLine("+++++++++++++++++++++++++++++++++++");

                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (double.TryParse(line, out double x))
                        {
                            if (x == 0) continue; // Уникаємо ділення на 0
                            double y = CalculateFunction(x);
                            streamWriter.WriteLine($"+   X={x,6:F2}   +  Y={y,10:F5}    +");
                        }
                    }

                    streamWriter.WriteLine("+++++++++++++++++++++++++++++++++++");
                    streamWriter.WriteLine("Таблицю сформувала  <Бояринцова Поліна Сергіївна>");
                }

                Console.WriteLine("Розрахунок завершено. Результати у файлі LAB2.RES");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
            Console.ReadKey();
        }
    }
}
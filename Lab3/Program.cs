
namespace Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть розмірність масиву: ");
            int size = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("1. Введення з клавіатури.\n2. Зчитування з файлу.\n3. Заповнення випадковими значеннями.\nВиберіть спосіб заповнення масиву (1, 2, 3): ");
            int choose = Convert.ToInt32(Console.ReadLine());

            if (choose == 2)
            {
                size = 10;
            }

            int[] array = new int[size];

            switch (choose)
            {
                case 1:
                    One(size, array);
                    break;
                case 2:
                    if (!Two(size, array))
                    {
                        Console.WriteLine("Помилка читання з файлу. Програма завершена.");
                        return;
                    }
                    break;
                case 3:
                    Three(size, array);
                    break;
                default:
                    Console.WriteLine("Невірний вибір!");
                    return;
            }

            Console.WriteLine("Масив:");
            foreach (int num in array)
            {
                Console.Write("{0,5:D}", num);
            }
            Console.WriteLine();

            double result = CalculateAverage(array);
            Console.WriteLine(double.IsNaN(result) ? "Не вдалося знайти необхідні елементи." : $"Середнє арифметичне: {result}");
        }

        static void One(int size, int[] array)
        {
            Console.WriteLine("Уведіть {0} елементів:", size);
            for (int j = 0; j < size; j++)
            {
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    array[j] = value;
                }
                else
                {
                    Console.WriteLine("Некоректне значення, спробуйте ще раз.");
                    j--;
                }
            }
        }

        static void Three(int size, int[] array)
        {
            Random rnd = new Random();
            for (int j = 0; j < size; j++)
            {
                array[j] = rnd.Next(-50, 51);
            }
        }

        static bool Two(int size, int[] array)
        {
            if (!File.Exists("dat.txt"))
            {
                Console.WriteLine("Файл dat.txt не знайдено.");
                return false;
            }

            using (StreamReader sRead = new StreamReader("dat.txt"))
            {
                for (int j = 0; j < size; j++)
                {
                    string? line = sRead.ReadLine();
                    if (line == null)
                    {
                        Console.WriteLine("Недостатньо даних у файлі!");
                        return false;
                    }
                    if (int.TryParse(line, out int value))
                    {
                        array[j] = value;
                    }
                    else
                    {
                        Console.WriteLine($"Некоректне значення у файлі на рядку {j + 1}.");
                        return false;
                    }
                }
            }
            return true;
        }

        static double CalculateAverage(int[] array)
        {
            bool foundNegative = false, foundPositive = false;
            int firstNegative = 0, lastPositive = 0;

            foreach (int num in array)
            {
                if (num < 0 && !foundNegative)
                {
                    firstNegative = num;
                    foundNegative = true;
                }
                if (num > 0)
                {
                    lastPositive = num;
                    foundPositive = true;
                }
            }

            return (foundNegative && foundPositive) ? (firstNegative + lastPositive) / 2.0 : double.NaN;
        }
    }
}

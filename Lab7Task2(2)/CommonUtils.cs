using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_
{
    internal class CommonUtils
    {
        private static Random rnd = new Random();

        // Масиви даних для випадкової генерації
        private static string[] materials = { "Алюміній", "Карбон", "Нейлон", "Композит", "Титан", "Шовк", "Сталь" };

        // Повертає випадковий матеріал
        public static string GetRandomMaterial()
        {
            return materials[rnd.Next(materials.Length)];
        }

        // Повертає випадковий рік виробництва (від 2000 до поточного року)
        public static int GetRandomYear()
        {
            return rnd.Next(2000, DateTime.Now.Year + 1);
        }

        // Повертає випадкову вагу (від min до max)
        public static double GetRandomWeight(double min, double max)
        {
            return Math.Round(min + (max - min) * rnd.NextDouble(), 1);
        }

        // Метод для виведення роздільника
        public static void PrintDivider(char symbol = '-', int length = 40)
        {
            Console.WriteLine(new string(symbol, length));
        }

        // Метод для виведення заголовку з рамкою
        public static void PrintHeader(string header)
        {
            Console.WriteLine("\n" + new string('=', 5) + " " + header + " " + new string('=', 5));
        }

        // Метод для очікування натискання клавіші з повідомленням
        public static void WaitForKey(string message = "\nНатисніть будь-яку клавішу для продовження...")
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}

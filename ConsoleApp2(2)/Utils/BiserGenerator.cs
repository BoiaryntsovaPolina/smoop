using ConsoleApp2.ConcreteBiser;
using ConsoleApp2.Enums;
using ConsoleApp2.Sklad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Utils
{
    public static class BiserGenerator
    {
        private static readonly Random _random = new Random();

        public static Biser GenerateRandomBiser(
            double minSize = 1.0, double maxSize = 4.0,
            int minQuality = 4, int maxQuality = 9)
        {
            Array colors = Enum.GetValues(typeof(BiserColor));
            BiserColor randomColor = (BiserColor)colors.GetValue(_random.Next(colors.Length));
            double randomSize = minSize + _random.NextDouble() * (maxSize - minSize);
            int randomQuality = _random.Next(minQuality, maxQuality + 1);

            return new Biser(randomColor, randomSize, randomQuality);
        }

        public static void PopulateSkladWithRandomExistingBiser(
            ProfessionalBiserSklad sklad,
            int count,
            double minSize = 1.0, double maxSize = 4.0,
            int minQuality = 4, int maxQuality = 9)
        {
            if (sklad == null)
            {
                Console.WriteLine("Помилка: Склад не може бути null для заповнення.");
                return;
            }

            Console.WriteLine($"\n--- Додаємо {count} випадкових намистин на склад (існуючий бісер) ---");
            for (int i = 0; i < count; i++)
            {
                sklad.AddBiser(GenerateRandomBiser(minSize, maxSize, minQuality, maxQuality));
            }
        }


        // Симулює купівлю пакетика бісеру певного типу і додає його вміст на склад.
        public static void BuySimulatedBiserPackage(
            ProfessionalBiserSklad sklad,
            BiserColor biserColor,
            bool isQualityCzech,
            double desiredMinSize, // Додано параметр для бажаного мінімального розміру
            double desiredMaxSize) // Додано параметр для бажаного максимального розміру
        {
            int beadsInBag;
            double bagPrice;
            int quality;
            string brand;

            if (isQualityCzech)
            {
                beadsInBag = _random.Next(18, 23);
                bagPrice = _random.NextDouble() * (12.0 - 8.0) + 8.0;
                quality = _random.Next(8, 11);
                brand = "Чеський";
            }
            else
            {
                beadsInBag = _random.Next(45, 56);
                bagPrice = _random.NextDouble() * (7.0 - 3.0) + 3.0;
                quality = _random.Next(4, 8);
                brand = "Звичайний";
            }

            Console.WriteLine($"\nКупуємо пакет бісеру: Бренд:{brand}, Колір:{biserColor}, Якість:{quality}/10, {beadsInBag} шт. за {bagPrice:F2}грн. (Розмір: {desiredMinSize:F1}-{desiredMaxSize:F1}мм)");

            sklad.BuyBiserPackage(biserColor, quality, bagPrice, beadsInBag);
        }
    }
}

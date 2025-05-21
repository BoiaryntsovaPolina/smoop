using Lab7Task2_2_.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_.Utilits
{
    internal class TestUtils
    {
        // Метод для генерації випадкової кількості випадкових пристроїв
        public static void GenerateRandomDevices(Registry registry, int minCount = 5, int maxCount = 15)
        {
            Random rnd = new Random();
            int count = rnd.Next(minCount, maxCount + 1);

            for (int i = 0; i < count; i++)
            {
                registry.AddDevice(DeviceRandomFactory.CreateRandomDevice());
            }

            Console.WriteLine($"Згенеровано {count} випадкових пристроїв.");
        }

        // Метод для демонстрації всіх функцій реєстру
        public static void DemonstrateAllFeatures(Registry registry)
        {
            // Виведення списків
            DemonstrateListings(registry);

            // Демонстрація сортування
            DemonstrateSorting(registry);

            // Демонстрація клонування
            DemonstrateCloning(registry);
        }

        // Демонстрація різних видів списків
        private static void DemonstrateListings(Registry registry)
        {
            Console.WriteLine("ВИВЕДЕННЯ ВСІХ ПРИСТРОЇВ");
            registry.DisplayAllDevices();
            Console.WriteLine("Натисніть клавішу");
            Console.WriteLine("---------------------------------");
            Console.ReadKey();

            Console.WriteLine("ВИВЕДЕННЯ ЕЛЕКТРОННОГО ОБЛАДНАННЯ");
            registry.DisplayElectronicDevices();
            Console.WriteLine("Натисніть клавішу");
            Console.WriteLine("---------------------------------");
            Console.ReadKey();

            Console.WriteLine("ВИВЕДЕННЯ ОБЛАДНАННЯ БЕЗ ДВИГУНІВ");
            registry.DisplayDevicesWithoutEngines();
            Console.WriteLine("Натисніть клавішу");
            Console.WriteLine("---------------------------------");
            Console.ReadKey();
        }

        // Демонстрація сортування
        private static void DemonstrateSorting(Registry registry)
        {
            Console.WriteLine("СОРТУВАННЯ ЗА РОКОМ ВИРОБНИЦТВА ТА ВАГОЮ");
            registry.SortByYearWeight();
            registry.DisplayAllDevices();
            Console.WriteLine("Натисніть клавішу");
            Console.WriteLine("---------------------------------");
            Console.ReadKey();
        }

        // Демонстрація клонування
        private static void DemonstrateCloning(Registry registry)
        {
            Console.WriteLine("ДЕМОНСТРАЦІЯ КЛОНУВАННЯ");

            // Клонуємо перший пристрій
            Device? originalDevice = registry.CloneDevice(0);
            Console.WriteLine("Оригінальний пристрій:");
            Console.WriteLine(originalDevice.ToString());

            Device? clonedDevice = (Device?)originalDevice?.Clone();
            Console.WriteLine("\nКлонований пристрій:");
            Console.WriteLine(clonedDevice?.ToString());

            // Змінюємо властивості клонованого пристрою
            if (clonedDevice is Airplane)
            {
                Airplane clonedAirplane = (Airplane)clonedDevice;
                clonedAirplane.Model = clonedAirplane.Model + " (копія)";
                clonedAirplane.YearOfManufacture = DateTime.Now.Year;

                Console.WriteLine("\nКлонований пристрій після змін:");
                Console.WriteLine(clonedAirplane.ToString());
            }
        }
    }
}


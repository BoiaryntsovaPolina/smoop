using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_
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
            CommonUtils.PrintHeader("ВИВЕДЕННЯ ВСІХ ПРИСТРОЇВ");
            registry.DisplayAllDevices();
            CommonUtils.WaitForKey();

            CommonUtils.PrintHeader("ВИВЕДЕННЯ ЕЛЕКТРОННОГО ОБЛАДНАННЯ");
            registry.DisplayElectronicDevices();
            CommonUtils.WaitForKey();

            CommonUtils.PrintHeader("ВИВЕДЕННЯ ОБЛАДНАННЯ БЕЗ ДВИГУНІВ");
            registry.DisplayDevicesWithoutEngines();
            CommonUtils.WaitForKey();
        }

        // Демонстрація сортування
        private static void DemonstrateSorting(Registry registry)
        {
            CommonUtils.PrintHeader("СОРТУВАННЯ ЗА ВАГОЮ");
            registry.SortByWeight();
            registry.DisplayAllDevices();
            CommonUtils.WaitForKey();

            CommonUtils.PrintHeader("СОРТУВАННЯ ЗА РОКОМ ВИРОБНИЦТВА");
            registry.SortByYear();
            registry.DisplayAllDevices();
            CommonUtils.WaitForKey();
        }

        // Демонстрація клонування
        private static void DemonstrateCloning(Registry registry)
        {
            CommonUtils.PrintHeader("ДЕМОНСТРАЦІЯ КЛОНУВАННЯ");

            // Клонуємо перший пристрій
            Device? originalDevice = registry.CloneDevice(0);
            Console.WriteLine("Оригінальний пристрій:");
            originalDevice.DisplayInfo();

            Device? clonedDevice = (Device?)originalDevice?.Clone();
            Console.WriteLine("\nКлонований пристрій:");
            clonedDevice?.DisplayInfo();

            // Змінюємо властивості клонованого пристрою
            if (clonedDevice is Airplane)
            {
                Airplane clonedAirplane = (Airplane)clonedDevice;
                clonedAirplane.Model = clonedAirplane.Model + " (копія)";
                clonedAirplane.YearOfManufacture = DateTime.Now.Year;

                Console.WriteLine("\nКлонований пристрій після змін:");
                clonedAirplane.DisplayInfo();
            }
        }
    }
}


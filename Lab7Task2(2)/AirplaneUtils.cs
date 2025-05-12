using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_
{
    internal class AirplaneUtils
    {
        private static Random rnd = new Random();

        // Масиви даних для випадкової генерації
        private static string[] airplaneNames = { "Cessna", "Piper", "Boeing", "Airbus", "Bombardier" };
        private static string[] airplaneModels = { "172 Skyhawk", "PA-28 Cherokee", "737-800", "A320", "Global 7500" };
        private static string[] engineTypes = { "Поршневий", "Газотурбінний", "Реактивний", "Турбогвинтовий", "Турбовальний" };

        // Повертає випадкове ім'я літака
        public static string GetRandomName()
        {
            return airplaneNames[rnd.Next(airplaneNames.Length)];
        }

        // Повертає випадкову модель літака
        public static string GetRandomModel()
        {
            return airplaneModels[rnd.Next(airplaneModels.Length)];
        }

        // Повертає випадковий тип двигуна
        public static string GetRandomEngineType()
        {
            return engineTypes[rnd.Next(engineTypes.Length)];
        }

        // Повертає випадкову потужність двигуна
        public static int GetRandomHorsePower()
        {
            return rnd.Next(100, 5000);
        }

        // Створює випадковий об'єкт літака
        public static Airplane CreateRandom()
        {
            return new Airplane(
                GetRandomName(),
                GetRandomModel(),
                CommonUtils.GetRandomYear(),
                CommonUtils.GetRandomWeight(500, 5000),
                GetRandomEngineType(),
                GetRandomHorsePower(),
                rnd.Next(3000, 12000),  // Максимальна висота
                rnd.Next(2, 200)        // Пасажиромісткість
            );
        }
    }
}

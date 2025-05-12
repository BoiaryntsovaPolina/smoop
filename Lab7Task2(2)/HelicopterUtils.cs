using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_
{
    internal class HelicopterUtils
    {
        private static Random rnd = new Random();

        // Масиви даних для випадкової генерації
        private static string[] helicopterNames = { "Robinson", "Bell", "Sikorsky", "Eurocopter", "Hughes" };
        private static string[] helicopterModels = { "R44", "206 JetRanger", "UH-60 Black Hawk", "EC135", "MD 500" };
        private static string[] engineTypes = { "Поршневий", "Газотурбінний", "Турбовальний" };

        // Повертає випадкове ім'я вертольота
        public static string GetRandomName()
        {
            return helicopterNames[rnd.Next(helicopterNames.Length)];
        }

        // Повертає випадкову модель вертольота
        public static string GetRandomModel()
        {
            return helicopterModels[rnd.Next(helicopterModels.Length)];
        }

        // Повертає випадковий тип двигуна
        public static string GetRandomEngineType()
        {
            return engineTypes[rnd.Next(engineTypes.Length)];
        }

        // Повертає випадкову потужність двигуна
        public static int GetRandomHorsePower()
        {
            return rnd.Next(200, 3000);
        }

        // Створює випадковий об'єкт вертольота
        public static Helicopter CreateRandom()
        {
            return new Helicopter(
                GetRandomName(),
                GetRandomModel(),
                CommonUtils.GetRandomYear(),
                CommonUtils.GetRandomWeight(500, 3000),
                GetRandomEngineType(),
                GetRandomHorsePower(),
                rnd.Next(8, 25),        // Діаметр ротора
                rnd.Next(150, 350)      // Максимальна швидкість
            );
        }
    }
}

using Lab7Task2_2_.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_.Utilits
{
    internal class DeltaplaneUtils
    {
        private static Random rnd = new Random();

        // Масиви даних для випадкової генерації
        private static string[] deltaplaneNames = { "Wills Wing", "Moyes", "Airborne", "Aeros", "Icaro" };
        private static string[] deltaplaneModels = { "Sport 2", "Litespeed RX", "Sting 3", "Combat GT", "Laminar" };

        // Повертає випадкове ім'я дельтаплана
        public static string GetRandomName()
        {
            return deltaplaneNames[rnd.Next(deltaplaneNames.Length)];
        }

        // Повертає випадкову модель дельтаплана
        public static string GetRandomModel()
        {
            return deltaplaneModels[rnd.Next(deltaplaneModels.Length)];
        }

        // Створює випадковий об'єкт дельтаплана
        public static Deltaplane CreateRandom()
        {
            return new Deltaplane(
                GetRandomName(),
                GetRandomModel(),
                CommonUtils.GetRandomYear(),
                CommonUtils.GetRandomWeight(20, 50),
                "Крило",
                CommonUtils.GetRandomMaterial() + " та тканина",
                CommonUtils.GetRandomWeight(15, 35),
                rnd.Next(8, 12),           // Розмах крила
                rnd.Next(10, 20)           // Аеродинамічна якість
            );
        }
    }
}

using Lab7Task2_2_.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_.Utilits
{
    internal class HotAirBalloonUtils
    {
        private static Random rnd = new Random();

        // Масиви даних для випадкової генерації
        private static string[] balloonNames = { "Cameron", "Lindstrand", "Ultramagic", "Kavanagh", "Kubicek" };
        private static string[] balloonModels = { "C-90", "LBL-105", "N-250", "E-120", "BB-22" };

        // Повертає випадкове ім'я повітряної кулі
        public static string GetRandomName()
        {
            return balloonNames[rnd.Next(balloonNames.Length)];
        }

        // Повертає випадкову модель повітряної кулі
        public static string GetRandomModel()
        {
            return balloonModels[rnd.Next(balloonModels.Length)];
        }

        // Створює випадковий об'єкт повітряної кулі
        public static HotAirBalloon CreateRandom()
        {
            return new HotAirBalloon(
                GetRandomName(),
                GetRandomModel(),
                CommonUtils.GetRandomYear(),
                CommonUtils.GetRandomWeight(200, 500),
                "Оболонка",
                CommonUtils.GetRandomMaterial(),
                CommonUtils.GetRandomWeight(100, 350),
                rnd.Next(2, 8),           // Місткість корзини
                rnd.Next(1500, 4000)      // Об'єм оболонки
            );
        }
    }
}

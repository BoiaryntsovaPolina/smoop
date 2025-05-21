using Lab7Task2_2_.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_.Utilits
{
    internal class FlyingCarpetUtils
    {
        private static Random rnd = new Random();

        // Масиви даних для випадкової генерації
        private static string[] carpetNames = { "Агра", "Персія", "Самарканд", "Аравія", "Магрибська" };
        private static string[] carpetModels = { "Султан-2000", "Аладдін-300", "Летючий-500", "Стрімкий-100", "Чарівний-700" };
        private static string[] magicTypes = { "Левітація", "Телекінез", "Антигравітація", "Повітряна магія", "Просторова магія" };

        // Повертає випадкове ім'я килима
        public static string GetRandomName()
        {
            return carpetNames[rnd.Next(carpetNames.Length)];
        }

        // Повертає випадкову модель килима
        public static string GetRandomModel()
        {
            return carpetModels[rnd.Next(carpetModels.Length)];
        }

        // Повертає випадковий тип магії
        public static string GetRandomMagicType()
        {
            return magicTypes[rnd.Next(magicTypes.Length)];
        }

        // Створює випадковий об'єкт килима
        public static FlyingCarpet CreateRandom()
        {
            return new FlyingCarpet(
                GetRandomName(),
                GetRandomModel(),
                CommonUtils.GetRandomYear(),
                CommonUtils.GetRandomWeight(5, 15),
                "Килим",
                CommonUtils.GetRandomMaterial() + " з магічними нитками",
                CommonUtils.GetRandomWeight(3, 10),
                GetRandomMagicType(),
                rnd.Next(50, 200)          // Магічна сила
            );
        }
    }
}

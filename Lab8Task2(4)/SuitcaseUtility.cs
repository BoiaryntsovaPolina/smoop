using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8Task2
{
    internal class SuitcaseUtility
    {
        private static Random random = new Random();

        private static string[] colors = {
            "Червоний", "Синій", "Зелений", "Чорний", "Сірий",
            "Коричневий", "Помаранчевий", "Фіолетовий", "Бежевий", "Білий"
        };

        private static string[] manufacturers = {
            "Samsonite", "Delsey", "Rimowa", "Antler", "American Tourister",
            "Eastpak", "Tumi", "Louis Vuitton", "Victorinox", "Travelpro"
        };

        private static string[] thingNames = {
            "Футболка", "Джинси", "Шкарпетки", "Взуття", "Книга",
            "Зарядний пристрій", "Ноутбук", "Фотоапарат", "Зубна щітка", "Шампунь",
            "Телефон", "Парасолька", "Гаманець", "Навушники", "Паспорт",
            "Сонцезахисні окуляри", "Ліки", "Сувенір", "Косметичка", "Рушник"
        };

        // Узагальнений метод для отримання випадкового елемента з масиву будь-якого типу
        private static T GetRandomElement<T>(T[] array)
        {
            int index = random.Next(array.Length);
            return array[index];
        }

        private static double GetRandomDouble(double min, double max)
        {
            return min + (max - min) * random.NextDouble();
        }

        public static Suitcase CreateRandomSuitcase()
        {
            string color = GetRandomElement(colors);
            string manufacturer = GetRandomElement(manufacturers);
            double weight = Math.Round(GetRandomDouble(2.0, 5.0), 2);
            double maxVolume = Math.Round(GetRandomDouble(30.0, 80.0), 2);

            return new Suitcase(color, manufacturer, weight, maxVolume);
        }

        public static Thing CreateRandomThing(bool isEssential = false)
        {
            string name = GetRandomElement(thingNames);
            double volume = Math.Round(GetRandomDouble(0.5, 10.0), 2);
            double weight = Math.Round(GetRandomDouble(0.1, 3.0), 2);
            int importance = random.Next(1, 11); // Важливість від 1 до 10

            return new Thing(name, volume, weight, isEssential, importance);
        }

        // Створює набір речей для пакування у валізу
        public static Thing[] CreateThingsSet(int count, int essentialCount = 0)
        {
            if (count <= 0)
            {
                throw new ArgumentException("Кількість предметів повинна бути більше нуля!");
            }

            if (essentialCount < 0 || essentialCount > count)
            {
                throw new ArgumentException("Неправильна кількість обов'язкових предметів!");
            }

            Thing[] things = new Thing[count];

            // Спочатку створюємо обов'язкові предмети
            for (int i = 0; i < essentialCount; i++)
            {
                things[i] = CreateRandomThing(true);
            }

            // Потім додаємо звичайні предмети
            for (int i = essentialCount; i < count; i++)
            {
                things[i] = CreateRandomThing(false);
            }

            return things;
        }

        public static void FillSuitcaseRandomly(Suitcase suitcase, int count)
        {
            if (suitcase == null)
            {
                throw new ArgumentNullException(nameof(suitcase), "Валіза не може бути NULL!");
            }

            if (count <= 0)
            {
                throw new ArgumentException("Кількість предметів повинна бути більше нуля!");
            }

            int added = 0;
            int attempts = 0;
            int maxAttempts = count * 3;

            while (added < count && attempts < maxAttempts)
            {
                Thing thing = CreateRandomThing();

                try
                {
                    suitcase.AddThing(thing);
                    added++;
                }
                catch (ArgumentException)
                {
                    // Якщо валіза переповнена, продовжуємо спроби
                }

                attempts++;
            }

            if (added < count)
            {
                Console.WriteLine("Увага: додано лише {0} з {1} предметів через обмеження валізи",
                                 added, count);
            }
        }
    }
}

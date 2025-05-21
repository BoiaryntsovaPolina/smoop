using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8Task2
{
    internal class Suitcase
    {
        private string color;
        private string manufacturer;
        private double weight;       // Вага порожньої валізи (в кг)
        private double maxVolume;    // Максимальний об'єм валізи (в літрах)
        private Thing[] things;
        private int thingCount;
        private double currentVolume;  // Поточний зайнятий об'єм валізи (в літрах)
        private double totalWeight;    // Загальна вага валізи з речами (в кг)

        // Делегат для події ThingAdded, що напряму передає доданий предмет
        public delegate void ThingAddedEventListener(Suitcase sender, Thing addedThing);

        // Подія, що виникає при додаванні предмету до валізи
        public event ThingAddedEventListener ThingAdded;

        
        public Suitcase(string color, string manufacturer, double weight, double maxVolume, int maxThings = 100)
        {
            
            if (weight <= 0)
            {
                throw new ArgumentException("Вага валізи повинна бути більше нуля!");
            }

            if (maxVolume <= 0)
            {
                throw new ArgumentException("Об'єм валізи повинен бути більше нуля!");
            }

            if (maxThings <= 0)
            {
                throw new ArgumentException("Максимальна кількість речей повинна бути більше нуля!");
            }

            this.color = color;
            this.manufacturer = manufacturer;
            this.weight = weight;
            this.maxVolume = maxVolume;
            this.things = new Thing[maxThings];  // Створюємо масив для зберігання речей
            this.thingCount = 0;
            this.currentVolume = 0;
            this.totalWeight = weight;          // Початкова загальна вага - це вага порожньої валізи
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }

        public double Weight
        {
            get { return weight; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Вага валізи повинна бути більше нуля!");
                }
                // Перераховуємо загальну вагу при зміні ваги порожньої валізи
                totalWeight = totalWeight - weight + value;
                weight = value;
            }
        }

        public double MaxVolume
        {
            get { return maxVolume; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Об'єм валізи повинен бути більше нуля!");
                }

                if (value < currentVolume)
                {
                    throw new ArgumentException("Новий об'єм валізи менший, ніж вже зайнятий об'єм!");
                }

                maxVolume = value;
            }
        }

        public double CurrentVolume { get { return currentVolume; } }
        public double TotalWeight { get { return totalWeight; } }
        public int ThingCount { get { return thingCount; } }
        public bool IsFull { get { return thingCount >= things.Length || currentVolume >= maxVolume; } }

        // Метод для перевірки, чи можна додати предмет
        public bool CanAddThing(Thing thing)
        {
            if (thing == null)
                throw new ArgumentNullException(nameof(thing), "Предмет не може бути null!");

            // Перевіряємо, чи є ще місце в масиві речей
            if (thingCount >= things.Length)
            {
                return false;
            }

            // Перевіряємо, чи не перевищить об'єм валізи
            if (currentVolume + thing.Volume > maxVolume)
            {
                return false;
            }

            return true;
        }

        // Метод для розрахунку вільного об'єму
        public double GetFreeVolume()
        {
            return maxVolume - currentVolume;
        }

        // Метод для додавання предмету в валізу
        public void AddThing(Thing thing)
        {
            if (thing == null)
                throw new ArgumentNullException(nameof(thing), "Предмет не може бути null!");

            
            if (thingCount >= things.Length)
            {
                throw new ArgumentException("Валіза не може вмістити більше предметів!");
            }

            
            if (currentVolume + thing.Volume > maxVolume)
            {
                throw new ArgumentException(
                    string.Format("Перевищено об'єм валізи! Вільно: {0:F2} л, потрібно: {1:F2} л",
                                 maxVolume - currentVolume, thing.Volume));
            }

            
            things[thingCount] = thing;
            thingCount++;

            
            currentVolume += thing.Volume;
            totalWeight += thing.Weight;

            // Викликаємо подію, якщо є підписники
            ThingAdded?.Invoke(this, thing);
        }

        // Метод для видалення предмету з валізи за індексом
        public Thing RemoveThing(int index)
        {
            // Перевіряємо коректність індексу
            if (index < 0 || index >= thingCount)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Неправильний індекс предмету!");
            }

            // Зберігаємо предмет для повернення
            Thing removedThing = things[index];

            // Оновлюємо поточний об'єм і загальну вагу
            currentVolume -= removedThing.Volume;
            totalWeight -= removedThing.Weight;

            // Зсуваємо всі наступні предмети на одну позицію вліво
            for (int i = index; i < thingCount - 1; i++)
            {
                things[i] = things[i + 1];
            }

            // Зменшуємо кількість предметів і очищаємо останню позицію
            thingCount--;
            things[thingCount] = null;

            return removedThing;
        }

        // Метод для отримання предмету за індексом (без видалення)
        public Thing GetThing(int index)
        {
            // Перевіряємо коректність індексу
            if (index < 0 || index >= thingCount)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Неправильний індекс предмету!");
            }

            return things[index];
        }

        // Метод для виведення вмісту валізи
        public void PrintContents()
        {
            Console.WriteLine("Вміст валізи '{0}' від {1}:", color, manufacturer);
            Console.WriteLine("-------------------------------");

            if (thingCount == 0)
            {
                Console.WriteLine("Валіза порожня");
            }
            else
            {
                for (int i = 0; i < thingCount; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, things[i]);
                }

                Console.WriteLine("-------------------------------");
                Console.WriteLine("Всього предметів: {0}", thingCount);
                Console.WriteLine("Зайнятий об'єм: {0:F2} л з {1:F2} л",
                                 currentVolume, maxVolume);
                Console.WriteLine("Загальна вага: {0:F2} кг", totalWeight);
            }
        }

        // Метод для сортування предметів за важливістю використовуючи IComparable
        public void SortByImportance()
        {
            if (thingCount <= 1)
                return;

            // Створюємо тимчасовий масив для сортування
            Thing[] tempArray = new Thing[thingCount];
            Array.Copy(things, tempArray, thingCount);

            // стандартний метод сортування
            Array.Sort(tempArray, 0, thingCount);

            // Копіюємо відсортований масив назад
            Array.Copy(tempArray, things, thingCount);
        }

        // Метод для знаходження найменш важливого предмета (не обов'язкового)
        public int FindLeastImportantNonEssentialItem()
        {
            int leastImportantIndex = -1;
            int lowestImportance = 11; // Більше максимально можливого значення важливості (10)

            for (int i = 0; i < thingCount; i++)
            {
                if (!things[i].IsEssential && things[i].Importance < lowestImportance)
                {
                    lowestImportance = things[i].Importance;
                    leastImportantIndex = i;
                }
            }

            return leastImportantIndex;
        }

        // Спробувати додати важливий предмет, видаляючи менш важливі предмети якщо потрібно
        public bool TryAddImportantThing(Thing importantThing, out Thing[] removedThings)
        {
            if (importantThing == null)
                throw new ArgumentNullException(nameof(importantThing), "Предмет не може бути null!");

            // Якщо предмет можна додати без проблем
            if (CanAddThing(importantThing))
            {
                AddThing(importantThing);
                removedThings = new Thing[0]; // порожній масив
                return true;
            }

            // Якщо для предмета не вистачає об'єму
            double neededVolume = importantThing.Volume - GetFreeVolume();
            if (neededVolume <= 0)
            {
                // Якщо є вільний об'єм, але заповнений масив предметів
                removedThings = new Thing[0]; // порожній масив
                return false;
            }

            // Створюємо тимчасовий масив для зберігання видалених предметів
            Thing[] tempRemoved = new Thing[thingCount];
            int removedCount = 0;
            double freedVolume = 0;

            // Спочатку шукаємо і видаляємо необов'язкові предмети з найменшою важливістю
            while (freedVolume < neededVolume && thingCount > 0)
            {
                int leastImportantIndex = FindLeastImportantNonEssentialItem();
                if (leastImportantIndex == -1) // Якщо не залишилось необов'язкових предметів
                    break;

                Thing leastImportant = GetThing(leastImportantIndex);

                // Перевіряємо, чи важливість предмету не більше нашого важливого
                if (leastImportant.Importance >= importantThing.Importance)
                    break;

                // Видаляємо предмет
                Thing removed = RemoveThing(leastImportantIndex);
                tempRemoved[removedCount++] = removed;
                freedVolume += removed.Volume;
            }

            // Якщо звільнили достатньо місця
            if (CanAddThing(importantThing))
            {
                AddThing(importantThing);

                // Створюємо масив потрібного розміру для видалених предметів
                removedThings = new Thing[removedCount];
                Array.Copy(tempRemoved, removedThings, removedCount);
                return true;
            }
            else
            {
                // Якщо не вдалося звільнити достатньо місця, повертаємо предмети назад у валізу
                for (int i = removedCount - 1; i >= 0; i--)
                {
                    AddThing(tempRemoved[i]);
                }
                removedThings = new Thing[0]; // порожній масив
                return false;
            }
        }


        public override string ToString()
        {
            return string.Format("Валіза '{0}' від {1}, вага: {2:F2} кг, об'єм: {3:F2} л",
                                 color, manufacturer, weight, maxVolume);
        }
    }
}
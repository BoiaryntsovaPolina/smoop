using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8Task2
{
    internal class SuitcaseManager
    {
        public void InteractiveSuitcasePacking()
        {
            Console.WriteLine("=== Інтерактивне пакування валізи ===");

            // Створюємо валізу
            Console.WriteLine("Створення нової валізи:");
            Console.Write("Колір валізи: ");
            string color = Console.ReadLine();

            Console.Write("Виробник: ");
            string manufacturer = Console.ReadLine();

            double weight = ReadPositiveDouble("Вага порожньої валізи (кг)");
            double maxVolume = ReadPositiveDouble("Максимальний об'єм валізи (л)");

            // Створюємо валізу з вказаними параметрами
            Suitcase suitcase = new Suitcase(color, manufacturer, weight, maxVolume);

            // Реєструємо обробник подій
            RegisterEventListener(suitcase);

            Console.WriteLine($"\nСтворено валізу: {suitcase}");

            // Додаємо предмети
            bool continueAdding = true;

            while (continueAdding && !suitcase.IsFull)
            {
                Console.WriteLine("\n=== Меню дій ===");
                Console.WriteLine("1. Додати предмет вручну");
                Console.WriteLine("2. Додати випадковий предмет");
                Console.WriteLine("3. Додати набір випадкових предметів");
                Console.WriteLine("4. Вивести вміст валізи");
                Console.WriteLine("5. Сортувати предмети за важливістю");
                Console.WriteLine("6. Видалити предмет");
                Console.WriteLine("0. Завершити пакування");

                Console.Write("\nВаш вибір: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCustomThing(suitcase);
                        break;

                    case "2":
                        AddRandomThing(suitcase);
                        break;

                    case "3":
                        AddRandomThingSet(suitcase);
                        break;

                    case "4":
                        PrintSuitcaseInfo(suitcase);
                        break;

                    case "5":
                        suitcase.SortByImportance();
                        Console.WriteLine("Предмети відсортовані за важливістю");
                        PrintSuitcaseInfo(suitcase);
                        break;

                    case "6":
                        RemoveThing(suitcase);
                        break;

                    case "0":
                        continueAdding = false;
                        break;

                    default:
                        Console.WriteLine("Неправильний вибір. Спробуйте ще раз.");
                        break;
                }

                // Перевіряємо, чи валіза повна
                if (suitcase.IsFull)
                {
                    Console.WriteLine("\nВаліза заповнена повністю!");
                }
            }

            // Виводимо фінальний вміст валізи
            Console.WriteLine("\n=== Фінальний вміст валізи ===");
            PrintSuitcaseInfo(suitcase);
        }

        // Об'єднаний метод виведення інформації про валізу
        private void PrintSuitcaseInfo(Suitcase suitcase)
        {
            suitcase.PrintContents();
            Console.WriteLine($"Вільний об'єм: {suitcase.GetFreeVolume():F2} л");
            Console.WriteLine($"Загальна вага: {suitcase.TotalWeight:F2} кг");
        }

        // Новий метод для видалення предмета
        private void RemoveThing(Suitcase suitcase)
        {
            if (suitcase.ThingCount == 0)
            {
                Console.WriteLine("Валіза порожня, нема що видаляти");
                return;
            }

            PrintSuitcaseInfo(suitcase);

            int index = ReadIntInRange("Введіть номер предмета для видалення (від 1)", 1,
                                       suitcase.ThingCount) - 1;

            try
            {
                Thing removed = suitcase.RemoveThing(index);
                Console.WriteLine($"Предмет '{removed.Name}' успішно видалено");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка під час видалення: {ex.Message}");
            }
        }

        // Допоміжні методи

        // Реєстрація обробника подій для валізи
        private void RegisterEventListener(Suitcase suitcase)
        {
            SuitcaseListener listener = new SuitcaseListener();
            suitcase.ThingAdded += listener.OnThingAdded;
        }

        // Метод для читання додатного дійсного числа з консолі
        private double ReadPositiveDouble(string prompt)
        {
            double value = 0;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write($"{prompt}: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out value) && value > 0)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Некоректне значення. Введіть додатне число.");
                }
            }

            return value;
        }

        // Метод для читання цілого числа з консолі у заданому діапазоні
        private int ReadIntInRange(string prompt, int min, int max)
        {
            int value = 0;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write($"{prompt}: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out value) && value >= min && value <= max)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine($"Некоректне значення. Введіть число від {min} до {max}.");
                }
            }

            return value;
        }

        // Метод для спроби додати важливий предмет з оптимізацією вмісту валізи
        private bool TryAddThingWithOptimization(Suitcase suitcase, Thing importantThing)
        {
            Console.WriteLine($"Намагаємося оптимізувати вміст валізи для додавання предмета '{importantThing.Name}'...");

            Thing[] removedThings;
            bool success = suitcase.TryAddImportantThing(importantThing, out removedThings);

            if (success)
            {
                Console.WriteLine($"Предмет '{importantThing.Name}' успішно додано до валізи!");

                // Якщо для звільнення місця довелося щось видалити
                if (removedThings.Length > 0)
                {
                    Console.WriteLine("Для звільнення місця були видалені такі предмети:");
                    for (int i = 0; i < removedThings.Length; i++)
                    {
                        Console.WriteLine($"  - {removedThings[i].Name} (важливість: {removedThings[i].Importance}/10)");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Не вдалося додати предмет '{importantThing.Name}' навіть після оптимізації.");
                Console.WriteLine("Ймовірно, у валізі лише обов'язкові предмети або предмети з вищою важливістю.");
            }

            return success;
        }


        // Допоміжний метод для додавання користувацького предмета
        private void AddCustomThing(Suitcase suitcase)
        {
            Console.WriteLine("\nДодавання нового предмета:");

            Console.Write("Назва предмета: ");
            string name = Console.ReadLine();

            double volume = ReadPositiveDouble("Об'єм предмета (л)");
            double thingWeight = ReadPositiveDouble("Вага предмета (кг)");

            Console.Write("Обов'язковий предмет? (так/ні): ");
            bool isEssential = Console.ReadLine().ToLower() == "так";

            int importance = ReadIntInRange("Важливість предмета (1-10)", 1, 10);

            // Створюємо новий предмет
            Thing thing = new Thing(name, volume, thingWeight, isEssential, importance);

            try
            {
                // Перевіряємо, чи можна додати предмет і додаємо з оптимізацією, якщо потрібно
                bool canAdd = suitcase.CanAddThing(thing);

                if (canAdd)
                {
                    suitcase.AddThing(thing);
                    Console.WriteLine($"Предмет '{name}' успішно додано до валізи.");
                }
                else
                {
                    Console.WriteLine($"Предмет '{name}' не поміщається у валізу.");

                    // Якщо предмет важливий або обов'язковий, пропонуємо оптимізацію
                    if (isEssential || importance >= 7)
                    {
                        Console.Write("Спробувати оптимізувати вміст валізи? (так/ні): ");
                        if (Console.ReadLine().ToLower() == "так")
                        {
                            TryAddThingWithOptimization(suitcase, thing);
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка при додаванні предмета: {ex.Message}");
            }
        }

        // Допоміжний метод для додавання випадкового предмета
        private void AddRandomThing(Suitcase suitcase)
        {
            Console.Write("Створити обов'язковий предмет? (так/ні): ");
            bool isEssential = Console.ReadLine().ToLower() == "так";

            Thing randomThing = SuitcaseUtility.CreateRandomThing(isEssential);

            Console.WriteLine($"Створено випадковий предмет: {randomThing}");

            try
            {
                // Перевіряємо, чи можна додати предмет і додаємо з оптимізацією, якщо потрібно
                bool canAdd = suitcase.CanAddThing(randomThing);

                if (canAdd)
                {
                    suitcase.AddThing(randomThing);
                    Console.WriteLine($"Предмет '{randomThing.Name}' успішно додано до валізи.");
                }
                else
                {
                    Console.WriteLine($"Предмет '{randomThing.Name}' не поміщається у валізу.");

                    // Якщо предмет важливий або обов'язковий, пропонуємо оптимізацію
                    if (isEssential || randomThing.Importance >= 7)
                    {
                        Console.Write("Спробувати оптимізувати вміст валізи? (так/ні): ");
                        if (Console.ReadLine().ToLower() == "так")
                        {
                            TryAddThingWithOptimization(suitcase, randomThing);
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка при додаванні предмета: {ex.Message}");
            }
        }

        // Допоміжний метод для додавання набору випадкових предметів
        private void AddRandomThingSet(Suitcase suitcase)
        {
            int count = ReadIntInRange("Скільки випадкових предметів додати", 1, 20);

            Console.WriteLine($"Додаємо {count} випадкових предметів:");

            int addedCount = 0;
            for (int i = 0; i < count; i++)
            {
                // 20% ймовірність, що предмет буде обов'язковим
                bool isEssential = new Random().Next(100) < 20;

                Thing randomThing = SuitcaseUtility.CreateRandomThing(isEssential);

                try
                {
                    if (suitcase.CanAddThing(randomThing))
                    {
                        suitcase.AddThing(randomThing);
                        Console.WriteLine($"+ Додано: {randomThing.Name}" +
                                          (randomThing.IsEssential ? " (обов'язковий)" : ""));
                        addedCount++;
                    }
                    else
                    {
                        // Якщо предмет важливий або обов'язковий, спробуємо оптимізувати
                        if (randomThing.IsEssential || randomThing.Importance >= 7)
                        {
                            bool success = TryAddThingWithOptimization(suitcase, randomThing);
                            if (success) addedCount++;
                        }
                        else
                        {
                            Console.WriteLine($"- Не додано: {randomThing.Name} (недостатньо місця)");
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"! Помилка: {ex.Message}");
                }
            }
            Console.WriteLine($"\nУспішно додано {addedCount} з {count} предметів.");
        }
    }
}
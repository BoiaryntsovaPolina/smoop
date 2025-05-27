using ConsoleApp2.BaseClasses;
using ConsoleApp2.ConcreteProducts;
using ConsoleApp2.Enums;
using ConsoleApp2.Interfaces;
using ConsoleApp2.Utils;
using ConsoleApp2.Workshop;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        BiserWorkshop workshop = new BiserWorkshop("Майстерня Аврора");

        Console.WriteLine("⭐ ПОЧАТОК ДЕМОНСТРАЦІЇ ⭐");
        Console.WriteLine("Назва майстерні: " + workshop.Name);

        // Початкове наповнення складу "існуючим" бісером (від останніх поробок)
        Console.WriteLine("\n--- Початкове наповнення складу існуючим бісером ---");
        // Додаємо 30 випадкових намистин середньої якості
        BiserGenerator.PopulateSkladWithRandomExistingBiser(workshop.Sklad, 30);
        // Додаємо 10 випадкових намистин вищої якості
        BiserGenerator.PopulateSkladWithRandomExistingBiser(workshop.Sklad, 10, minQuality: 8, maxQuality: 10);

        workshop.Sklad.VyvestyDetalnyyStanSkladu();

        // Створюємо вироби
        Kvitka flower1 = new Kvitka("Сонячна квітка");
        Braslet bracelet1 = new Braslet("Браслет 'Океан'", 20); // 20 см
        Kvitka flower2 = new Kvitka("Ніжна фіалка");

        // Розпочинаємо роботи
        workshop.StartWork(flower1);
        workshop.StartWork(bracelet1);
        workshop.StartWork(flower2);

        // Спроба завершити роботи
        Console.WriteLine("\n--- Спроба завершити 'Сонячну квітку' ---");
        if (!workshop.CompleteWork("Сонячна квітка"))
        {
            Console.WriteLine("Не вистачило бісеру для 'Сонячної квітки'. Докуповуємо!");
            // Отримуємо вимоги до квітки, щоб знати, який розмір докуповувати
            BiserRequirement[] flowerReqs = flower1.GetRequirements();

            // Докуповуємо необхідний бісер, імітуючи купівлю пакетів з конкретними розмірами
            // Для зеленого потрібен розмір 2.0
            BiserGenerator.BuySimulatedBiserPackage(workshop.Sklad, BiserColor.Zeleniy, false, flowerReqs[0].MinSize, flowerReqs[0].MinSize + 0.5);
            // Для рожевого потрібен розмір 3.0
            BiserGenerator.BuySimulatedBiserPackage(workshop.Sklad, BiserColor.Rozoviy, true, flowerReqs[1].MinSize, flowerReqs[1].MinSize + 0.5);
            // Для жовтого потрібен розмір 2.5
            BiserGenerator.BuySimulatedBiserPackage(workshop.Sklad, BiserColor.Zhovtiy, true, flowerReqs[2].MinSize, flowerReqs[2].MinSize + 0.5);

            // Спроба завершити ще раз після докупівлі
            Console.WriteLine("\n--- Повторна спроба завершити 'Сонячну квітку' після докупівлі ---");
            workshop.CompleteWork("Сонячна квітка");
        }

        Console.WriteLine("\n--- Спроба завершити 'Браслет 'Океан'' ---");
        if (!workshop.CompleteWork("Браслет 'Океан'"))
        {
            Console.WriteLine("Не вистачило бісеру для 'Браслету 'Океан''. Докуповуємо!");
            // Отримуємо вимоги до браслета, щоб знати, який розмір докуповувати
            BiserRequirement[] braceletReqs = bracelet1.GetRequirements();
            // Для синього браслета потрібен розмір 2.0
            BiserGenerator.BuySimulatedBiserPackage(workshop.Sklad, BiserColor.Siniy, false, braceletReqs[0].MinSize, braceletReqs[0].MinSize + 0.5);

            // Спроба завершити ще раз після докупівлі
            Console.WriteLine("\n--- Повторна спроба завершити 'Браслет 'Океан'' після докупівлі ---");
            workshop.CompleteWork("Бraслет 'Океан'"); // Виправлено потенційну опечатку в назві
        }

        Console.WriteLine("\n--- Спроба завершити 'Ніжна фіалка' ---");
        workshop.CompleteWork("Ніжна фіалка"); // Можливо, не вистачить бісеру

        // Перевіряємо потребу в докупівлі
        Console.WriteLine("\n  Перевіряємо потребу в докупівлі...");
        workshop.Sklad.ChyPotribnaDocupivlya(60); // Мінімальна кількість 60 намистин на складі

        // Фінальний стан майстерні
        Console.WriteLine("\n" + new string('=', 50));
        workshop.VyvestyStanMaysterny();

        // Демонстрація ітератора (foreach)
        Console.WriteLine("\n  Перебір всього бісеру на складі:");
        int count = 0;
        foreach (IBiser biser in workshop.Sklad)
        {
            if (count < 5) // Показуємо тільки перші 5 намистин для прикладу
            {
                Console.WriteLine($"    - {biser}");
                count++;
            }
            else
            {
                // Якщо бісеру більше, ніж 5, не виводимо все, щоб не захаращувати консоль
                Console.WriteLine($"    ... та ще {workshop.Sklad.GetTotalBiserCount() - 5} намистин");
                break;
            }
        }

        Console.WriteLine("\n  Демонстрація завершена!  ✨ ");
    }
}
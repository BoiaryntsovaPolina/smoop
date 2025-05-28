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

        // --- Функція для циклічної докупівлі та спроби завершення ---
        // Це дозволяє уникнути дублювання коду
        TryCompleteAndBuyUntilEnough(workshop, flower1);
        TryCompleteAndBuyUntilEnough(workshop, bracelet1);
        TryCompleteAndBuyAndStopIfCannotBuy(workshop, flower2); // Для фіалки не будемо купувати безкінечно

        // Перевіряємо потребу в докупівлі
        Console.WriteLine("\nПеревіряємо потребу в докупівлі...");
        workshop.Sklad.ChyPotribnaDocupivlya(60);

        // Фінальний стан майстерні
        Console.WriteLine("\n" + new string('=', 50));
        workshop.VyvestyStanMaysterny();

        // Демонстрація ітератора (foreach)
        Console.WriteLine("\nПеребір всього бісеру на складі:");
        int count = 0;
        foreach (IBiser biser in workshop.Sklad)
        {
            if (count < 5)
            {
                Console.WriteLine($"    - {biser}");
                count++;
            }
            else
            {
                Console.WriteLine($"    ... та ще {workshop.Sklad.GetTotalBiserCount() - 5} намистин (повний список дивіться у логах складу)");
                break;
            }
        }

        Console.WriteLine("\nДемонстрація завершена!");
    }

    // Спроба завершити виріб, і якщо не вистачає бісеру, докуповувати його до тих пір, поки не вистачить.
    private static void TryCompleteAndBuyUntilEnough(BiserWorkshop workshop, Virob virob)
    {
        Console.WriteLine($"\n--- Спроба завершити '{virob.Name}' ---");

        // Якщо CompleteWork повертає false, це означає, що бісеру не вистачило
        BiserRequirement[] missingReqs = workshop.CompleteWorkWithMissing(virob);

        // Цикл буде продовжуватися, поки є відсутні вимоги
        while (missingReqs.Length > 0)
        {
            Console.WriteLine($"Не вистачило бісеру для '{virob.Name}'. Докуповуємо!");

            // Докуповуємо кожен тип бісеру, якого не вистачає
            for (int i = 0; i < missingReqs.Length; i++)
            {
                BiserRequirement req = missingReqs[i];
                Console.WriteLine($"    Потрібно докупити: {req.Quantity - req.AvailableQuantity} шт. {req.Color} бісеру, розмір >={req.MinSize:F1}мм, якість >={req.MinQuality}.");

                // Визначаємо, чи потрібен якісний "чеський" бісер
                bool isCzech = req.MinQuality >= 8; // Припустимо, якість 8 і вище - це "чеський"

                // Купуємо пакет з бажаним кольором, якістю та діапазоном розмірів.
                // Додаємо невеликий діапазон 0.5мм до minSize для реалістичності.
                BiserGenerator.BuySimulatedBiserPackage(workshop.Sklad, req.Color, isCzech, req.MinSize, req.MinSize + 0.5);
            }

            // Виводимо стан складу після докупівлі
            Console.WriteLine("\n--- Стан складу після докупівлі: ---");
            workshop.Sklad.VyvestyDetalnyyStanSkladu();

            // Повторна спроба завершити виріб
            Console.WriteLine($"\n--- Повторна спроба завершити '{virob.Name}' після докупівлі ---");
            missingReqs = workshop.CompleteWorkWithMissing(virob);
        }
    }

    // Спроба завершити виріб, і якщо не вистачає бісеру, докуповувати його тільки 2 рази.
    // Якщо після цього все одно не вистачить - відмінити.
    private static void TryCompleteAndBuyAndStopIfCannotBuy(BiserWorkshop workshop, Virob virob)
    {
        Console.WriteLine($"\n--- Спроба завершити '{virob.Name}' ---");
        int maxBuyAttempts = 2; // Обмеження на кількість спроб докупівлі
        int currentAttempt = 0;

        BiserRequirement[] missingReqs = workshop.CompleteWorkWithMissing(virob);

        while (missingReqs.Length > 0 && currentAttempt < maxBuyAttempts)
        {
            Console.WriteLine($"Не вистачило бісеру для '{virob.Name}'. Докуповуємо (спроба {currentAttempt + 1} з {maxBuyAttempts})!");
            for (int i = 0; i < missingReqs.Length; i++)
            {
                BiserRequirement req = missingReqs[i];
                Console.WriteLine($"    Потрібно докупити: {req.Quantity - req.AvailableQuantity} шт. {req.Color} бісеру, розмір >={req.MinSize:F1}мм, якість >={req.MinQuality}.");
                bool isCzech = req.MinQuality >= 8;
                BiserGenerator.BuySimulatedBiserPackage(workshop.Sklad, req.Color, isCzech, req.MinSize, req.MinSize + 0.5);
            }

            Console.WriteLine("\n--- Стан складу після докупівлі: ---");
            workshop.Sklad.VyvestyDetalnyyStanSkladu();

            Console.WriteLine($"\n--- Повторна спроба завершити '{virob.Name}' після докупівлі ---");
            missingReqs = workshop.CompleteWorkWithMissing(virob);
            currentAttempt++;
        }

        if (missingReqs.Length > 0)
        {
            Console.WriteLine($"\nВиріб '{virob.Name}' не вдалося завершити після {maxBuyAttempts} спроб докупівлі. Відміна.");
        }
    }
}
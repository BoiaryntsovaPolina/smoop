using Lab7.Classes;
using Lab7.Utilits;
using System;

namespace Lab7
{
    enum ProductCategory
    {
        Electronics,
        Groceries,
        Clothing,
        OfficeEquipment,
        Furniture
    }

    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "shops_out.txt";

            ShopContainer container = new ShopContainer();

            Shop[] shops = null;
            try
            {
                shops = container.Load(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при завантаженні даних: {ex.Message}");
                return;
            }

            if (shops == null || shops.Length == 0)
            {
                Console.WriteLine("Масив магазинів порожній або не завантажений.");
                return;
            }

            Console.WriteLine("===== ЗАВАНТАЖЕНІ МАГАЗИНИ =====");
            for (int i = 0; i < shops.Length; i++)
            {
                if (shops[i] != null)
                {
                    Console.WriteLine($"\n--- Магазин {i + 1} ---");
                    Console.WriteLine(shops[i].ToString()); // Виводимо повну інформацію про магазин
                }
                else
                {
                    Console.WriteLine($"\n--- Магазин {i + 1}: null ---");
                }
            }


            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try
            {
                // 1. Тестування класів Person та Product
                TestPersonAndProduct();

                // 2. Тестування ShopContainer
                TestShopContainer();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }

        static void TestPersonAndProduct()
        {
            Console.WriteLine("=== Тестування класів Person та Product ===");

            // Використання утилітних класів для створення об'єктів
            Person person1 = PersonRandom.CreateRandomPerson();
            Person person2 = PersonRandom.CreateRandomPerson();
            Person person3 = (Person)person1.Clone();

            Console.WriteLine("Person1: " + person1);
            Console.WriteLine("Person2: " + person2);
            Console.WriteLine("Person3 (клон Person1): " + person3);
            Console.WriteLine($"Порівняння Person1 і Person2: {person1.CompareTo(person2)}");

            // Створення продуктів через утилітний клас
            Product product1 = ProductRandom.CreateRandomProduct();
            Product product2 = ProductRandom.CreateRandomProduct();
            Product product3 = (Product)product1.Clone();

            Console.WriteLine("\nProduct1: " + product1);
            Console.WriteLine("Product2: " + product2);
            Console.WriteLine("Product3 (клон Product1): " + product3);
            Console.WriteLine($"Порівняння Product1 і Product2: {product1.CompareTo(product2)}");
        }

        static void TestShopContainer()
        {
            Console.WriteLine("\n=== Тестування класу ShopContainer ===");

            // Створення контейнера та заповнення за допомогою утилітного класу
            ShopContainer container = new ShopContainer();
            Shop[] randomShops = ShopRandom.CreateRandomShops(5, 3);

            // Додавання магазинів до контейнера
            foreach (var shop in randomShops)
            {
                container.Add(shop);
            }

            Console.WriteLine($"Створено контейнер з {container.Count} магазинами");

            // Виведення вмісту контейнера через foreach
            Console.WriteLine("\nВміст контейнера до сортування:");
            DisplayContainerWithForeach(container);

            // Сортування магазинів за вартістю
            container.SortByTotalValue();
            Console.WriteLine("\nВміст контейнера після сортування за вартістю:");
            DisplayContainerWithForeach(container);

            // Збереження відсортованого контейнера у файл
            string fileName = "shops.txt";
            container.Save(fileName);

            // Створення нового контейнера з перших 3 елементів
            ShopContainer subContainer = container.GetFirstN(3);
            Console.WriteLine("\nНовий контейнер з перших 3 елементів:");
            DisplayContainerWithForeach(subContainer);

            // Збереження нового контейнера у файл
            subContainer.Save("subshops.txt");
        }

        // Використання foreach для виведення елементів контейнера
        static void DisplayContainerWithForeach(ShopContainer container)
        {
            int index = 0;
            foreach (Shop shop in container)
            {
                Console.WriteLine($"[{index++}] {shop.ToShortString()}");
            }
        }
    }
}

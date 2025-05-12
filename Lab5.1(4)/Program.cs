using System;
using Lab5._1;

public enum ProductCategory
{
    OfficeEquipment,
    Software,
    OfficeSupplies
}

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Створення одного об'єкта типу Shop
        Shop shop = new Shop("ТехноМагазин", ProductCategory.OfficeEquipment, DateTime.Now);
        Console.WriteLine("Скорочена інформація про магазин:");
        Console.WriteLine(shop.ToShortString());

        // Виведення значень індексатора для різних категорій
        Console.WriteLine("\nПеревірка індексатора:");
        Console.WriteLine($"OfficeEquipment: {shop[ProductCategory.OfficeEquipment]}");
        Console.WriteLine($"Software: {shop[ProductCategory.Software]}");
        Console.WriteLine($"OfficeSupplies: {shop[ProductCategory.OfficeSupplies]}");

        // Присвоєння значень властивостям і виведення повної інформації
        shop.Name = "МегаОфіс";
        shop.Category = ProductCategory.Software;
        shop.Date = new DateTime(2023, 5, 15);

        Console.WriteLine("\nПовна інформація про магазин після зміни властивостей:");
        Console.WriteLine(shop.ToString());

        // Додавання товарів за допомогою методу AddProducts
        Console.WriteLine("\nДодавання товарів...");
        shop.AddProducts(
            new Product(new Person("Петро Іваненко", "+380501234567"), "Ноутбук Acer", 25000),
            new Product(new Person("Ольга Петренко", "+380672345678"), "Windows 11", 5000),
            new Product(new Person("Василь Сидоренко", "+380633456789"), "Microsoft Office", 3500)
        );

        Console.WriteLine("\nОновлений перелік товарів магазину:");
        Console.WriteLine(shop.ToString());

        // Створення масиву магазинів і виведення інформації про них
        Console.WriteLine("\nСтворення масиву магазинів...");
        Shop[] shops = ShopRandom.CreateRandomShops(5, 3);

        // Знаходження магазинів з найбільшою та найменшою сумарною вартістю
        Shop minShop = shops[0];
        Shop maxShop = shops[0];

        foreach (Shop s in shops)
        {
            if (s.TotalValue < minShop.TotalValue)
                minShop = s;
            if (s.TotalValue > maxShop.TotalValue)
                maxShop = s;
        }

        Console.WriteLine("\nМагазин з найменшою сумарною вартістю товарів:");
        Console.WriteLine(minShop.ToShortString());

        Console.WriteLine("\nМагазин з найбільшою сумарною вартістю товарів:");
        Console.WriteLine(maxShop.ToShortString());

        Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
        Console.ReadKey();
    }
}

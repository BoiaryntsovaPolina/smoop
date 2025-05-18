using Lab7.Classes;
using System;

namespace Lab7.Utilits
{
    internal class ProductRandom
    {
        private static Random rnd = new Random();
        private static string[] productNames = { "Комп'ютер", "Принтер", "Монітор", "Мишка", "Програма Office",
                                              "Антивірус", "Папір A4", "Олівці", "Ручки", "Степлер" };

        public static string GetRandomProductName()
        {
            return productNames[rnd.Next(productNames.Length)];
        }

        public static Product CreateRandomProduct()
        {
            Person supplier = PersonRandom.CreateRandomPerson();
            string name = GetRandomProductName();
            decimal cost = rnd.Next(100, 10000);
            return new Product(supplier, name, cost);
        }

        public static Product[] CreateRandomProducts(int count)
        {
            Product[] products = new Product[count];
            for (int i = 0; i < count; i++)
            {
                products[i] = CreateRandomProduct();
            }
            return products;
        }
    }
}


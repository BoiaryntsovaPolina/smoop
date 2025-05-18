using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using Lab7.Classes;

namespace Lab7.Utilits
{
    internal class ShopRandom
    {
        private static Random rnd = new Random();
        private static string[] shopNames = { "Comfy", "Rozetka", "MyFone", "Fozzy", "Brain", "Allo", "Foxtrot" };

        public static string GetRandomName()
        {
            return shopNames[rnd.Next(shopNames.Length)];
        }

        public static ProductCategory GetRandomCategory()
        {
            Array categories = Enum.GetValues(typeof(ProductCategory));
            return (ProductCategory)categories.GetValue(rnd.Next(categories.Length));
        }

        public static Shop CreateRandomShop(int productCount = 0)
        {
            string name = GetRandomName();
            ProductCategory category = GetRandomCategory();
            DateTime date = DateTime.Now.AddDays(-rnd.Next(1000));
            Product[] products = null;

            if (productCount > 0)
            {
                products = ProductRandom.CreateRandomProducts(productCount);
            }

            return new Shop(name, category, date, products);
        }

        public static Shop[] CreateRandomShops(int count, int productsPerShop = 0)
        {
            Shop[] shops = new Shop[count];
            for (int i = 0; i < count; i++)
            {
                shops[i] = CreateRandomShop(productsPerShop);
            }
            return shops;
        }

    }
}


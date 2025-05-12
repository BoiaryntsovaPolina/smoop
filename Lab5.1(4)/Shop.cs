using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab5._1
{
    class Shop
    {
        private string? name;
        private ProductCategory category;
        private DateTime date;
        private double totalValue;
        private Product[] products;

        public string? Name
        {
            get { return name; }
            set
            {
                try
                {
                    if (value != null && !Regex.IsMatch(value, @"^[\p{L}\d\s'-]{3,50}$"))
                        throw new ArgumentException("Некоректна назва магазину!");
                    name = value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка при встановленні назви магазину: {ex.Message}");
                    name = "Невідомий магазин";
                }
            }
        }

        public ProductCategory Category
        {
            get { return category; }
            set { category = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public double TotalValue
        {
            get
            {
                CalculateTotalValue();
                return totalValue;
            }
        }

        public Product[] Products
        {
            get { return products; }
            set { products = value ?? new Product[0]; }
        }

        public Shop(string? name, ProductCategory category, DateTime date, Product[]? products = null)
        {
            try
            {
                Name = name;
                Category = category;
                Date = date;
                Products = products ?? new Product[0];
                CalculateTotalValue();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при створенні магазину: {ex.Message}");
            }
        }

        public Shop() : this("Новий магазин", ProductCategory.OfficeEquipment, DateTime.Now) { }

        private void CalculateTotalValue()
        {
            totalValue = 0;
            foreach (var product in products)
            {
                if (product != null)
                {
                    totalValue += (double)product.Cost;
                }
            }
        }

        public bool this[ProductCategory category]
        {
            get { return this.Category == category; }
        }

        public void AddProducts(params Product[] newProducts)
        {
            try
            {
                int oldLength = products.Length;
                Array.Resize(ref products, oldLength + newProducts.Length);
                Array.Copy(newProducts, 0, products, oldLength, newProducts.Length);
                CalculateTotalValue();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при додаванні товарів: {ex.Message}");
            }
        }

        public string ToShortString()
        {
            return $"Магазин: {Name ?? "Невідомий"}, Категорія: {Category}, " +
                   $"Дата відкриття: {Date.ToShortDateString()}, Сумарна вартість: {TotalValue} грн";
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(ToShortString());
            sb.AppendLine("\nТовари:");
            if (products.Length == 0)
            {
                sb.AppendLine("  Немає товарів");
            }
            else
            {
                foreach (var p in products)
                {
                    if (p != null)
                        sb.AppendLine($"  {p}");
                }
            }
            return sb.ToString();
        }
    }
}

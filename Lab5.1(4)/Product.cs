using System;
using System.Text.RegularExpressions;

namespace Lab5._1
{
    class Product
    {
        private string? name;
        private decimal cost;

        public Person? Supplier { get; set; }

        public string? Name
        {
            get { return name; }
            set
            {
                try
                {
                    if (value != null && !Regex.IsMatch(value, @"^[\p{L}\d\s'-]{3,50}$"))
                        throw new ArgumentException("Некоректна назва товару!");
                    name = value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка при встановленні назви: {ex.Message}");
                    name = "Невідомий товар";
                }
            }
        }

        public decimal Cost
        {
            get { return cost; }
            set
            {
                try
                {
                    if (value <= 0)
                        throw new ArgumentException("Вартість повинна бути більше 0!");
                    cost = value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка при встановленні вартості: {ex.Message}");
                    cost = 1;
                }
            }
        }

        public Product(Person? supplier, string? name, decimal cost)
        {
            try
            {
                Supplier = supplier;
                Name = name;
                Cost = cost;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при створенні продукту: {ex.Message}");
            }
        }

        public Product() : this(new Person(), "Невідомий товар", 0) { }

        public override string ToString()
        {
            return $"{Supplier}, Назва: {Name ?? "Невідомий"}, Вартість: {Cost} грн";
        }
    }
}

using System;
using System.Text.RegularExpressions;

namespace Lab7.Classes
{
    internal class Product : IComparable<Product>,/* IComparable,*/ ICloneable  
    {
        private string? name;
        private decimal cost;
        private Person? supplier;


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

        public override string ToString()                                                           // ToString є
        {
            return $"{Supplier}, Назва: {Name ?? "Невідомий"}, Вартість: {Cost} грн";
        }

        // Реалізація методу CompareTo для інтерфейсу IComparable
        public int CompareTo(Product? other)
        {
            if (other == null) return 1;

            // Спочатку порівнюємо за вартістю
            int costComparison = Cost.CompareTo(other.Cost);
            if (costComparison != 0)
            {
                return costComparison;
            }

            // Потім порівнюємо за назвою, якщо вартість однакова
            if (Name != null && other.Name != null)
            {
                int nameComparison = Name.CompareTo(other.Name);
                if (nameComparison != 0)
                {
                    return nameComparison;
                }
            }
            else if (Name != null)
            {
                return 1;
            }
            else if (other.Name != null)
            {
                return -1;
            }

            // Нарешті порівнюємо за постачальником, якщо назви однакові
            if (Supplier != null && other.Supplier != null)
            {
                return Supplier.CompareTo(other.Supplier);
            }
            else if (Supplier != null)
            {
                return 1;
            }
            else if (other.Supplier != null)
            {
                return -1;
            }

            return 0;
        }

        // Реалізація методу Clone для інтерфейсу ICloneable
        public object Clone()
        {
            Person? clonedSupplier = null;
            if (Supplier != null)
            {
                clonedSupplier = (Person)Supplier.Clone();
            }

            // Створюємо нову копію об'єкта Product з тими ж значеннями
            return new Product(clonedSupplier, Name, Cost);
        }

        // Зберігаємо зворотну сумісність з IComparable
        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            if (obj is Product other)
                return CompareTo(other);

            throw new ArgumentException("Об'єкт не є типом Product");
        }

        public override bool Equals(object? obj)                                               // Додала Equals, GetHashCode
        {
            if (obj is Product)
            {
                return ToString().Equals(((Product)obj).ToString());
            }
            return false;
        }

        public override int GetHashCode() { return ToString().GetHashCode(); }

    }
}

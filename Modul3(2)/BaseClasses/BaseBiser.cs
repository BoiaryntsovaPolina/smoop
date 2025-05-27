using ConsoleApp2.Enums;
using ConsoleApp2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.BaseClasses
{
    public abstract class BaseBiser : IBiser
    {
        public BiserColor Color { get; protected set; }
        public double Size { get; protected set; }
        public int Quality { get; protected set; } // 1-10
        public string Brand { get; protected set; }

        protected BaseBiser(BiserColor color, double size, int quality, string brand)
        {
            Color = color;
            Size = size;
            Quality = Math.Max(1, Math.Min(10, quality)); // Якість завжди від 1 до 10
            Brand = brand;
        }

        // Порівнює дві намистини. Сортування за кольором, потім розміром, потім якістю.
        public int CompareTo(IBiser other)
        {
            if (other == null) return 1;

            int colorComparison = Color.CompareTo(other.Color);
            if (colorComparison != 0) return colorComparison;

            int sizeComparison = Size.CompareTo(other.Size);
            if (sizeComparison != 0) return sizeComparison;

            return Quality.CompareTo(other.Quality);
        }

        public override string ToString()
        {
            return $"Колір: {Color}, Розмір: {Size:F1}мм, Якість: {Quality}/10, Бренд: {Brand}";
        }
    }
}

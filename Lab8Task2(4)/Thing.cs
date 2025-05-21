using Lab8Task2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab8Task2
{
    // Делегат для події додавання предмету до валізи
    public delegate void ThingAddedListener(object sender, ThingEventArgs e);

    // Клас для передачі даних про предмет в події
    public class ThingEventArgs : EventArgs
    {
        public Thing Thing { get; private set; }

        public ThingEventArgs(Thing thing)
        {
            Thing = thing;
        }
    }

    // Реалізація IComparable для класу Thing
    public class Thing : IComparable<Thing>
    {
        private string name;
        private double volume;
        private double weight;
        private bool isEssential; // Обов'язковий предмет чи ні
        private int importance;   // Рівень важливості від 1 до 10

        public Thing(string name, double volume, double weight, bool isEssential = false, int importance = 5)
        {
            if (volume <= 0)
            {
                throw new ArgumentException("Об'єм предмету повинен бути більше нуля!");
            }

            if (weight <= 0)
            {
                throw new ArgumentException("Вага предмету повинна бути більше нуля!");
            }

            if (importance < 1 || importance > 10)
            {
                throw new ArgumentException("Важливість повинна бути в межах від 1 до 10!");
            }

            this.name = name;
            this.volume = volume;
            this.weight = weight;
            this.isEssential = isEssential;
            this.importance = importance;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Volume
        {
            get { return volume; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Об'єм предмету повинен бути більше нуля!");
                }
                volume = value;
            }
        }

        public double Weight
        {
            get { return weight; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Вага предмету повинна бути більше нуля!");
                }
                weight = value;
            }
        }

        public bool IsEssential
        {
            get { return isEssential; }
            set { isEssential = value; }
        }

        public int Importance
        {
            get { return importance; }
            set
            {
                if (value < 1 || value > 10)
                {
                    throw new ArgumentException("Важливість повинна бути в межах від 1 до 10!");
                }
                importance = value;
            }
        }

        // Реалізація IComparable для сортування за важливістю
        public int CompareTo(Thing other)
        {
            if (other == null) return 1;

            // Сортуємо за спаданням важливості (від найважливішого до найменш важливого)
            return other.importance.CompareTo(this.importance);
        }

        public override string ToString()
        {
            string essentialStatus = isEssential ? " (обов'язковий)" : "";
            return string.Format("{0}{1} (об'єм: {2:F2} л, вага: {3:F2} кг, важливість: {4}/10)",
                name, essentialStatus, volume, weight, importance);
        }
    }
}

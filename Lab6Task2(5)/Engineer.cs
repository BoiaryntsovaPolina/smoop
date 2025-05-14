using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Lab6Task2
{
    public class Engineer : Worker
    {
        public string? Specialization { get; set; }

        public Engineer() : base()
        {
            Specialization=null;
        }

        public Engineer(string? name, bool isMale, string? specialization) : base(name, isMale)
        {
            Specialization=specialization;
        }

        public override void Print()
        {
            base.Print();
        }

        // Перевизначення віртуального методу
        public override string GetWorkerType()
        {
            return "Engineer";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Engineer)
            {
                return ToString().Equals(((Engineer)obj).ToString());
            }
            return false;
        }

        public override int GetHashCode() { return ToString().GetHashCode(); }

        public override string ToString()
        {
            return $"{base.ToString()}, Specialization: {Specialization}";
        }
    }
}


















//// Конструктор
//public Engineer(string? name, bool isMale, string? specialization)
//{
//    Name = name;
//    IsMale = isMale;
//    Specialization = specialization;
//}

//// Реалізація абстрактного методу
//public override void ShowInfo()
//{
//    Console.WriteLine($"Engineer: {Name ?? "N/A"}, Male: {IsMale}, Specialization: {Specialization ?? "N/A"}");
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Lab6Task2
{
    public class Administration : Worker
    {
        public string? Department { get; set; }

        public Administration() : base()
        {
            Department = null;
        }
        public Administration(string? name, bool isMale, string? department) : base(name, isMale)
        {
            Department=department;
        }

        public override void Print()
        {
            base.Print();
            //Console.WriteLine($"Department: {Department}");
        }


        // Перевизначення віртуального методу
        public override string GetWorkerType()
        {
            return "Administration";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Staff)
            {
                return ToString().Equals(((Staff)obj).ToString());
            }
            return false;
        }

        public override int GetHashCode() { return ToString().GetHashCode(); }

        public override string ToString()
        {
            return $"{base.ToString()}, Department: {Department}";
        }
    }
}

















//// Конструктор
//public Administration(string? name, bool isMale, string? department)  // не використано наслідування
//{
//    Name = name;
//    IsMale = isMale;
//    Department = department;
//}

//// Реалізація абстрактного методу
//public override void ShowInfo()
//{
//    Console.WriteLine($"Administration: {Name ?? "N/A"}, Male: {IsMale}, Department: {Department ?? "N/A"}");
//}

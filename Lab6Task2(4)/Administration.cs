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

        // Конструктор
        public Administration(string? name, bool isMale, string? department)
        {
            Name = name;
            IsMale = isMale;
            Department = department;
        }

        // Реалізація абстрактного методу
        public override void ShowInfo()
        {
            Console.WriteLine($"Administration: {Name ?? "N/A"}, Male: {IsMale}, Department: {Department ?? "N/A"}");
        }

        // Перевизначення віртуального методу
        public override string GetWorkerType()
        {
            return "Administration";
        }

        // Перевизначення віртуального методу для перевірки валідності
        public override bool IsValid()
        {
            return base.IsValid() && !string.IsNullOrEmpty(Department);
        }
    }
}

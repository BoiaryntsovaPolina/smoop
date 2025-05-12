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

        // Конструктор
        public Engineer(string? name, bool isMale, string? specialization)
        {
            Name = name;
            IsMale = isMale;
            Specialization = specialization;
        }

        // Реалізація абстрактного методу
        public override void ShowInfo()
        {
            Console.WriteLine($"Engineer: {Name ?? "N/A"}, Male: {IsMale}, Specialization: {Specialization ?? "N/A"}");
        }

        // Перевизначення віртуального методу
        public override string GetWorkerType()
        {
            return "Engineer";
        }

        // Перевизначення віртуального методу для перевірки валідності
        public override bool IsValid()
        {
            return base.IsValid() && !string.IsNullOrEmpty(Specialization);
        }
    }
}

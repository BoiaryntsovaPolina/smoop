using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Lab6Task2
{
    public class Staff : Worker
    {
        public int? ExperienceYears { get; set; }

        // Конструктор
        public Staff(string? name, bool isMale, int experienceYears)
        {
            Name = name;
            IsMale = isMale;
            ExperienceYears = experienceYears;
        }

        // Реалізація абстрактного методу
        public override void ShowInfo()
        {
            Console.WriteLine($"Staff: {Name ?? "N/A"}, Male: {IsMale}, Experience: {ExperienceYears} years");
        }

        // Перевизначення віртуального методу
        public override string GetWorkerType()
        {
            return "Staff";
        }

        // Перевизначення віртуального методу для перевірки валідності
        public override bool IsValid()
        {
            return base.IsValid() && ExperienceYears.HasValue && ExperienceYears.Value > 0;
        }
    }
}

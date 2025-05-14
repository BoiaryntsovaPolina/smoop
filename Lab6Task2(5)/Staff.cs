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
        public int ExperienceYears { get; set; }

        public Staff() : base()
        {
            ExperienceYears=0;
        }

        public Staff(string? name, bool isMale, int experienceYears) : base(name, isMale)
        {
            ExperienceYears=experienceYears;
        }

        public override void Print()
        {
            base.Print();
        }

        // Перевизначення віртуального методу
        public override string GetWorkerType()
        {
            return "Staff";
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
            return $"{base.ToString()}, ExperienceYears: {ExperienceYears}"; 
        }

    }
}




















//// Конструктор
//public Staff(string? name, bool isMale, int experienceYears)
//{
//    Name = name;
//    IsMale = isMale;
//    ExperienceYears = experienceYears;
//}

// Реалізація абстрактного методу

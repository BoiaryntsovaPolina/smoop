using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_
{
    class Deltaplane : Device, IPart
    {
        public string? PartName { get; set; } = "Крило";
        public string? Material { get; set; } = "Алюміній та тканина";
        public double PartWeight { get; set; } = 30.0;
        public double WingSpan { get; set; } = 10.0; // Розмах крила в метрах
        public double GlideRatio { get; set; } = 15.0; // Аеродинамічна якість (відношення підйомної сили до опору)
        public override bool HasElectronics { get; } = false;  // Дельтаплани зазвичай не мають електроніки

        public Deltaplane(string? name, string? model, int yearOfManufacture, double weight,
            string? partName, string material, double partWeight, double wingSpan, double glideRatio) : base(name, model, yearOfManufacture, weight)
        {
            PartName = partName;
            Material = material;
            PartWeight = partWeight;
            WingSpan = wingSpan;
            GlideRatio = glideRatio;
        }

        // Метод для перевірки стану частини (крила)
        public bool CheckCondition()
        {
            // Припустимо, що стан крила залежить від року виробництва
            // Якщо крилу більше 3 років, воно потребує перевірки
            int currentYear = DateTime.Now.Year;
            return (currentYear - YearOfManufacture) <= 3;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Назва частини: {PartName}");
            Console.WriteLine($"Матеріал: {Material}");
            Console.WriteLine($"Вага частини: {PartWeight} кг");
            Console.WriteLine($"Розмах крила: {WingSpan} м");
            Console.WriteLine($"Аеродинамічна якість: {GlideRatio}");
            Console.WriteLine($"Стан: {(CheckCondition() ? "Добрий" : "Потребує перевірки")}");
        }
    }
}

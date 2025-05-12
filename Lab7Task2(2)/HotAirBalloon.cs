using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_
{
    class HotAirBalloon : Device, IPart
    {
        public string? PartName { get; set; } = "Оболонка";
        public string? Material { get; set; } = "Нейлон";
        public double PartWeight { get; set; } = 100.0;
        public int BasketCapacity { get; set; } = 4; // Місткість корзини (кількість людей)
        public double EnvelopeVolume { get; set; } = 2000.0; // Об'єм оболонки в кубічних метрах
        public override bool HasElectronics { get; } = false;  // Повітряні кулі зазвичай не мають електроніки

        public HotAirBalloon(string? name, string? model, int yearOfManufacture, double weight,
            string? partName, string? material, double partWeight, int basketCapacity, double envelopeVolume) : base(name, model, yearOfManufacture, weight)
        {
            PartName = partName;
            Material = material;
            PartWeight = partWeight;
            BasketCapacity = basketCapacity;
            EnvelopeVolume = envelopeVolume;
        }

        // Метод для перевірки стану частини (оболонки)
        public bool CheckCondition()
        {
            // Припустимо, що стан оболонки залежить від року виробництва
            // Якщо оболонці більше 5 років, вона потребує перевірки
            int currentYear = DateTime.Now.Year;
            return (currentYear - YearOfManufacture) <= 5;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Назва частини: {PartName}");
            Console.WriteLine($"Матеріал: {Material}");
            Console.WriteLine($"Вага частини: {PartWeight} кг");
            Console.WriteLine($"Місткість корзини: {BasketCapacity} осіб");
            Console.WriteLine($"Об'єм оболонки: {EnvelopeVolume} м³");
            Console.WriteLine($"Стан: {(CheckCondition() ? "Добрий" : "Потребує перевірки")}");
        }
    }
}

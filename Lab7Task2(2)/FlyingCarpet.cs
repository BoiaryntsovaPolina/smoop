using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_
{
    class FlyingCarpet : Device, IPart
    {
        public string? PartName { get; set; } = "Килим";
        public string? Material { get; set; } = "Шовк з магічними нитками";
        public double PartWeight { get; set; } = 5.0;
        public string? MagicType { get; set; } = "Левітація"; // Тип магії
        public int MagicPower { get; set; } = 100;   // Сила магії (умовні одиниці)
        public override bool HasElectronics { get; } = false;  // Магічні килими не мають електроніки

        public FlyingCarpet(string? name, string? model, int yearOfManufacture, double weight,
            string? partName, string? material, double partWeight, string? magicType, int magicPower) : base(name, model, yearOfManufacture, weight)
        {
            PartName = partName;
            Material = material;
            PartWeight = partWeight;
            MagicType = magicType;
            MagicPower = magicPower;
        }

        // Метод для перевірки стану частини (килима)
        public bool CheckCondition()
        {
            // Припустимо, що стан килима залежить від магічної сили
            // Килим у хорошому стані, якщо магічна сила > 50
            return MagicPower > 50;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Назва частини: {PartName}");
            Console.WriteLine($"Матеріал: {Material}");
            Console.WriteLine($"Вага частини: {PartWeight} кг");
            Console.WriteLine($"Тип магії: {MagicType}");
            Console.WriteLine($"Сила магії: {MagicPower}");
            Console.WriteLine($"Стан: {(CheckCondition() ? "Добрий" : "Потребує підзарядки")}");
        }

        // Додатковий метод для килимів
        public void Fly()
        {
            if (CheckCondition())
            {
                Console.WriteLine($"Килим {Name} злітає в повітря!");
                MagicPower -= 10; // Використовує магічну силу при польоті
            }
            else
            {
                Console.WriteLine($"Килим {Name} не може злетіти - недостатньо магічної сили!");
            }
        }
    }
}

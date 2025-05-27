using ConsoleApp2.BaseClasses;
using ConsoleApp2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.ConcreteProducts
{
    public class Kvitka : Virob
    {
        private BiserRequirement[] requirements;

        public Kvitka(string name) : base(name, VirobComplexity.Seredniy)
        {
            // Приклад вимог для квітки.
            requirements = new BiserRequirement[]
            {
                new BiserRequirement(BiserColor.Zeleniy, 2.0, 7, 20), 
                new BiserRequirement(BiserColor.Rozoviy, 3.0, 8, 50),
                new BiserRequirement(BiserColor.Zhovtiy, 2.5, 9, 10)
            };
        }

        public override BiserRequirement[] GetRequirements()
        {
            // Повертаємо копію масиву вимог
            BiserRequirement[] copy = new BiserRequirement[requirements.Length];
            for (int i = 0; i < requirements.Length; i++)
            {
                copy[i] = new BiserRequirement(requirements[i].Color, requirements[i].MinSize,
                                                requirements[i].MinQuality, requirements[i].Quantity); // Прибрано maxPrice
            }
            return copy;
        }

        public override double GetEstimatedTime()
        {
            // Приблизний час виготовлення залежно від складності
            return 2 + ((int)Complexity - 1) * 2; // 2 год для Простого, 4 для Середнього, 6 для Складного
        }
    }
}

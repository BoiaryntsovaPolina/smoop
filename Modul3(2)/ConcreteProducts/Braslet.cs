using ConsoleApp2.BaseClasses;
using ConsoleApp2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.ConcreteProducts
{
    public class Braslet : Virob
    {
        private double lengthCm; // Довжина браслета в сантиметрах

        public Braslet(string name, double lengthCm) : base(name, VirobComplexity.Prostyy)
        {
            this.lengthCm = lengthCm;
        }

        public override BiserRequirement[] GetRequirements()
        {
            // Кількість бісеру залежить від довжини (наприклад, 5 намистин на см)
            int requiredBiserCount = (int)(lengthCm * 5);
            // Приклад вимог для браслета (синій бісер)
            return new BiserRequirement[]
            {
                new BiserRequirement(BiserColor.Siniy, 2.0, 6, requiredBiserCount)
            };
        }

        public override double GetEstimatedTime()
        {
            return lengthCm * 0.1; // Наприклад, 0.1 години на сантиметр довжини
        }
    }
}

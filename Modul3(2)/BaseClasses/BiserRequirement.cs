using ConsoleApp2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.BaseClasses
{
    // Клас, що описує вимоги до бісеру для виробу.
    public class BiserRequirement
    {
        public BiserColor Color { get; private set; }
        public double MinSize { get; private set; }
        public int MinQuality { get; private set; }
        public int Quantity { get; private set; }

        public BiserRequirement(BiserColor color, double minSize, int minQuality, int quantity)
        {
            Color = color;
            MinSize = minSize;
            MinQuality = minQuality;
            Quantity = quantity;
        }
    }
}

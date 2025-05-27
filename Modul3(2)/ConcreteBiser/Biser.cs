using ConsoleApp2.BaseClasses;
using ConsoleApp2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.ConcreteBiser
{
    // стандартна намистинка
    public class Biser : BaseBiser
    {
        public Biser(BiserColor color, double size, int quality, string brand = "Стандарт")
            : base(color, size, quality, brand) { }
    }
}

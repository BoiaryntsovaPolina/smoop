using ConsoleApp2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Interfaces
{
    // Інтерфейс для бісеру. Визначає основні властивості намистини.
    public interface IBiser : IComparable<IBiser>
    {
        BiserColor Color { get; }
        double Size { get; }
        int Quality { get; }
        string Brand { get; }
    }
}

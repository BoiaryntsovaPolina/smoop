using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_
{
    internal interface IDevice
    {
        string? Name { get; set; }
        string? Model { get; set; }
        int YearOfManufacture {  get; set; }
        double Weight { get; set; }
        bool HasElectronics { get; }

    }
}

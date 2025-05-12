using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_
{
    internal interface IPart
    {
        string? PartName { get; set; }
        string? Material {  get; set; }
        double PartWeight { get; set; }

        bool CheckCondition(); // Метод для перевірки стану частини
    }
}

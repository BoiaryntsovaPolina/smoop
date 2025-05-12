using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_
{
    internal interface IEngine
    {
        string? EngineType { get; set; }
        int HorsePower { get; set; }
        bool IsActive { get; set; }

        void StartEngine();
        void StopEngine();
    }
}

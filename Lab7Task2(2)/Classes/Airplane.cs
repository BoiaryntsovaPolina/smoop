using Lab7Task2_2_.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_.Classes
{
    internal class Airplane : Device, IEngine 
    {
       
        public string? EngineType { get; set; } = "Турбореактивний";
        public int HorsePower { get; set; } = 1000;
        public bool IsActive { get; set; } = false;
        public int MaxAltitude { get; set; } = 10000;    // Максимальна висота польоту в метрах
        public int PassengerCapacity { get; set; } = 10; // Кількість пасажирів
        public override bool HasElectronics { get; } = true;  // Літаки завжди мають електроніку

        public Airplane(string? name, string? model, int yearOfManufacture, double weight,
            string? engineType, int horsePower, int maxAltitude, int passengerCapacity) : base(name, model, yearOfManufacture, weight)
        {
            EngineType = engineType;
            HorsePower = horsePower;
            MaxAltitude = maxAltitude;
            PassengerCapacity = passengerCapacity;
            IsActive = false;
        }

        public void StartEngine()
        {
            if (!IsActive)
            {
                IsActive = true;
                Console.WriteLine($"Двигун літака {Name} запущено!");
            }
            else
            {
                Console.WriteLine($"Двигун літака {Name} вже працює!");
            }
        }

        public void StopEngine()
        {
            if (IsActive)
            {
                IsActive = false;
                Console.WriteLine($"Двигун літака {Name} зупинено!");
            }
            else
            {
                Console.WriteLine($"Двигун літака {Name} вже зупинено!");
            }
        }

        public override bool Equals(object? obj)                                               
        {
            if (obj is Airplane)
            {
                return ToString().Equals(((Airplane)obj).ToString());
            }
            return false;
        }

        public override int GetHashCode() { return ToString().GetHashCode(); }

        public override string ToString()
        {
            return $"{base.ToString()}, \nТип двигуна: {EngineType}, \nПотужність: {HorsePower}, \nДвигун активний: {(IsActive ? "Так" : "Ні")}, " +
                $"\nМаксимальна висота: {MaxAltitude} м, \nПасажиромісткість: {PassengerCapacity} осіб";

        }

    }
}

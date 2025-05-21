using Lab7Task2_2_.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task2_2_.Classes
{
    class Helicopter : Device, IEngine
    {
        public string? EngineType { get; set; } = "Газотурбінний";
        public int HorsePower { get; set; } = 800;
        public bool IsActive { get; set; } = false;
        public int RotorDiameter { get; set; } = 15;   // Діаметр ротора в метрах
        public int MaxSpeed { get; set; } = 300;     // Максимальна швидкість в км/год
        public override bool HasElectronics { get; } = true;  // Вертольоти завжди мають електроніку

        public Helicopter(string? name, string? model, int yearOfManufacture, double weight,
            string? engineType, int horsePower, int rotorDiameter, int maxSpeed) : base(name, model, yearOfManufacture, weight)
        {
            EngineType = engineType;
            HorsePower = horsePower;
            RotorDiameter = rotorDiameter;
            MaxSpeed = maxSpeed;
            EngineType = engineType;
            IsActive = false;
        }

        public void StartEngine()
        {
            if (!IsActive)
            {
                IsActive = true;
                Console.WriteLine($"Двигун вертольота {Name} запущено! Ротор обертається.");
            }
            else
            {
                Console.WriteLine($"Двигун вертольота {Name} вже працює!");
            }
        }

        public void StopEngine()
        {
            if (IsActive)
            {
                IsActive = false;
                Console.WriteLine($"Двигун вертольота {Name} зупинено! Ротор сповільнюється.");
            }
            else
            {
                Console.WriteLine($"Двигун вертольота {Name} вже зупинено!");
            }
        }

        public override bool Equals(object? obj)                                               
        {
            if (obj is Helicopter)
            {
                return ToString().Equals(((Helicopter)obj).ToString());
            }
            return false;
        }

        public override int GetHashCode() { return ToString().GetHashCode(); }


        public override string ToString()
        {
            return $"{base.ToString()}, \nТип двигуна: {EngineType}, \nПотужність: {HorsePower} к.с., \nДвигун активний: {(IsActive ? "Так" : "Ні")}" +
                $"\nДіаметр ротора: {RotorDiameter} м, \nМаксимальна швидкість: {MaxSpeed} км/год";
        }


    }
}

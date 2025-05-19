using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab7Task2_2_
{
    abstract class Device : IDevice, ICloneable, IComparable<Device> 
    {
        public string? Name { get; set; } = "Невідомий пристрій";
        public string? Model { get; set; } = "Невідома модель";
        public int YearOfManufacture { get; set; } = 2000;
        public double Weight { get; set; } = 0.0;
        public virtual bool HasElectronics { get; } = false;

        public Device(string? name, string? model, int yearOfManufacture, double weight)
        {
            Name=name;
            Model=model;
            YearOfManufacture=yearOfManufacture;
            Weight=weight;
        }

        public override string ToString()
        {
            return $"Назва: {Name}, \nМодель: {Model}, \nРік виробництва: {YearOfManufacture}, \nВага: {Weight} кг, \nМає електроніку: {(HasElectronics ? "Так" : "Ні")}";
        }




        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual int CompareTo(Device? obj)
        {

            if (obj == null) return 1;
            Device otherDevice = obj as Device;
            if (otherDevice != null)
            {
                return this.Weight.CompareTo(otherDevice.Weight);
            }
            else
            {
                throw new ArgumentException("Об'єкт не є пристроєм");
            }
        }
    }
}

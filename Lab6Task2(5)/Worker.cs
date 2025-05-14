using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Lab6Task2
{
    public abstract class Worker
    {
 
        public string? Name { get; set; }
        public bool IsMale { get; set; }


        protected Worker(string? name, bool isMale)
        {
            Name=name;
            IsMale=isMale;
        }

        protected Worker()
        {
            Name="NoName";
            IsMale=false;
        }

        // Абстрактний метод для виведення інформації       
        public virtual void Print()
        {
            Console.WriteLine(ToString());
        }


        // Віртуальний метод для отримання типу працівника
        public virtual string GetWorkerType()
        {
            return "Worker";
        }

        public override string ToString()
        {
            return $"Name: {Name}, Male: {IsMale}";
        }
    }
}















// Додала конструктор з параметрами та дефолтний конструктор 



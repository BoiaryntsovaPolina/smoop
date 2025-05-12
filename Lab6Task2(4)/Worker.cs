using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6Task2
{
    public abstract class Worker
    {
        public string? Name { get; set; }
        public bool IsMale { get; set; }

        // Абстрактний метод для виведення інформації
        public abstract void ShowInfo();

        // Віртуальний метод для отримання типу працівника
        public virtual string GetWorkerType()
        {
            return "Worker";
        }

        // Віртуальний метод для перевірки валідності даних
        public virtual bool IsValid()
        {
            return !string.IsNullOrEmpty(Name);
        }
    }
}

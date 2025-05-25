using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    [Serializable] // Атрибут для XML серіалізації
    public class Worker
    {
        public string FullName { get; set; } 
        public string Position { get; set; } 
        public decimal Salary { get; set; } 
        public DateTime HireDate { get; set; } // Дата призначення на роботу

        // (необхідний для XML серіалізації)
        public Worker() { }

     
        public Worker(string fullName, string position, decimal salary, DateTime hireDate)
        {
            FullName = fullName;
            Position = position;
            Salary = salary;
            HireDate = hireDate;
        }

        // розрахунок стажу роботи
        public int GetWorkExperience()
        {
            return DateTime.Now.Year - HireDate.Year;
        }

        // Метод для збільшення окладу на 4%
        public void IncreaseSalary()
        {
            Salary = Salary * 1.04m; // Збільшуємо на 4%
        }

        public override string ToString()
        {
            return $"ПІБ: {FullName}, Посада: {Position}, Оклад: {Salary:C}, " +
                   $"Дата найму: {HireDate:dd.MM.yyyy}, Стаж: {GetWorkExperience()} років";
        }
    }
}

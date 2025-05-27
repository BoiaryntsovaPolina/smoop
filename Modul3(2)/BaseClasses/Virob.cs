using ConsoleApp2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.BaseClasses
{
    public abstract class Virob
    {
        public string Name { get; protected set; }
        public VirobComplexity Complexity { get; protected set; }
        public bool IsCompleted { get; protected set; }
        public double CalculatedTotalCost { get; protected set; }

        protected Virob(string name, VirobComplexity complexity)
        {
            Name = name;
            Complexity = complexity;
            IsCompleted = false;
            CalculatedTotalCost = 0;
        }

        // Повертає масив вимог до бісеру для цього виробу.
        public abstract BiserRequirement[] GetRequirements();

        // Повертає приблизний час виготовлення виробу в годинах.
        public abstract double GetEstimatedTime();

        /// Метод, що викликається після успішного створення виробу.
        public virtual void OnCreated()
        {
            IsCompleted = true;
            Console.WriteLine($"Виріб '{Name}' успішно створено!");
        }

        // Розраховує загальну вартість використаного бісеру (якщо виріб вже зроблено).
        public double CalculateTotalCost()
        {
            return CalculatedTotalCost;
        }

        public override string ToString()
        {
            string status = IsCompleted ? $"Статус: Завершено, Час: {GetEstimatedTime():F1} год." : "Статус: В процесі";
            return $"Виріб: '{Name}' (Складність: {Complexity}), {status}";
        }
    }
}

﻿using ConsoleApp2.BaseClasses;
using ConsoleApp2.Interfaces;
using ConsoleApp2.Sklad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Workshop
{
    // Клас, що представляє майстерню з бісеру.
    // Керує складом, поточними та завершеними роботами, використовуючи масиви.
    public class BiserWorkshop
    {
        public string Name { get; private set; }
        public ProfessionalBiserSklad Sklad { get; private set; }

        private Virob[] worksInProgress; // Масив для робіт в процесі
        private int worksInProgressCount; // Кількість робіт в процесі
        private Virob[] completedWorks;   // Масив для завершених робіт
        private int completedWorksCount; // Кількість завершених робіт

        private const int InitialCapacity = 10; // Початкова ємність масивів робіт

        public BiserWorkshop(string name)
        {
            Name = name;
            Sklad = new ProfessionalBiserSklad();
            worksInProgress = new Virob[InitialCapacity];
            worksInProgressCount = 0;
            completedWorks = new Virob[InitialCapacity];
            completedWorksCount = 0;
        }

        // Додає виріб до списку робіт в процесі. При необхідності розширює масив.
        public void StartWork(Virob virob)
        {
            if (virob == null) return;

            if (worksInProgressCount >= worksInProgress.Length)
            {
                // Розширюємо масив вдвічі
                Virob[] newWorksInProgress = new Virob[worksInProgress.Length * 2];
                Array.Copy(worksInProgress, newWorksInProgress, worksInProgress.Length);
                worksInProgress = newWorksInProgress;
            }
            worksInProgress[worksInProgressCount++] = virob;
            Console.WriteLine($"\nРозпочато роботу над виробом: '{virob.Name}'.");
        }

        // Завершує роботу над виробом. Повертає масив вимог до бісеру, якого не вистачило.
        public BiserRequirement[] CompleteWorkWithMissing(Virob virob)
        {
            int foundIndex = -1;

            // Знаходимо виріб у списку робіт в процесі за об'єктом Virob
            for (int i = 0; i < worksInProgressCount; i++)
            {
                if (worksInProgress[i] == virob) 
                {
                    foundIndex = i;
                    break;
                }
            }

            if (foundIndex == -1) // Якщо виріб не знайдено в процесі (можливо, вже завершено або не розпочато)
            {
                Console.WriteLine($"\nВиріб '{virob.Name}' не знайдено в списку робіт в процесі. Можливо, вже завершено.");
                return new BiserRequirement[0]; 
            }

            IBiser[] usedBiser; 

            // Спроба відібрати бісер зі складу та створити виріб
            BiserRequirement[] missingReqs = Sklad.SprobuvatiStvorityVirob(virob, out usedBiser);

            if (missingReqs.Length == 0) 
            {
                // Додаємо виріб до списку завершених робіт
                if (completedWorksCount >= completedWorks.Length)
                {
                    Virob[] newCompletedWorks = new Virob[completedWorks.Length * 2];
                    Array.Copy(completedWorks, newCompletedWorks, completedWorks.Length);
                    completedWorks = newCompletedWorks;
                }
                completedWorks[completedWorksCount++] = virob;

                // Видаляємо виріб зі списку робіт в процесі
                for (int i = foundIndex; i < worksInProgressCount - 1; i++)
                {
                    worksInProgress[i] = worksInProgress[i + 1];
                }
                worksInProgress[worksInProgressCount - 1] = null;
                worksInProgressCount--;

                return new BiserRequirement[0]; 
            }
            else
            {
                return missingReqs; 
            }
        }

        // Виводить загальний стан майстерні.
        public void VyvestyStanMaysterny()
        {
            Console.WriteLine($"\n========== СТАН МАЙСТЕРНІ '{Name.ToUpper()}' ==========");

            Console.WriteLine("\nРоботи в процесі:");
            if (worksInProgressCount == 0)
            {
                Console.WriteLine("    Немає незавершених робіт.");
            }
            else
            {
                for (int i = 0; i < worksInProgressCount; i++)
                {
                    Console.WriteLine($"    - {worksInProgress[i].Name} (Складність: {worksInProgress[i].Complexity})");
                }
            }

            Console.WriteLine("\nЗавершені роботи:");
            if (completedWorksCount == 0)
            {
                Console.WriteLine("    Немає завершених робіт.");
            }
            else
            {
                for (int i = 0; i < completedWorksCount; i++)
                {
                    // Для хобі виводимо лише факт завершення та час
                    Console.WriteLine($"    - {completedWorks[i].Name}: Статус: Завершено, Час: {completedWorks[i].GetEstimatedTime():F1} год.");
                }
            }

            // Виводимо стан складу через метод Sklad
            Sklad.VyvestyDetalnyyStanSkladu(); // Це виведе загальні витрати на докупівлю
            Console.WriteLine("=================================================");
        }
    }
}

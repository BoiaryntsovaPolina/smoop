using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6Task2
{
    public static class WorkerRandom
    {
        private static Random _random = new Random();

        private static string[] names = { "John", "Alex", "Olga", "Maria", "Dmitri", "Julia", "Ivan", "Kate" };
        private static string[] specializations = { "Software Development", "Civil Engineering", "Electrical Engineering" };
        private static string[] departments = { "HR", "Finance", "Marketing" };

        // Генерація випадкових імен
        public static string? GenerateRandomName()
        {
            return names[_random.Next(names.Length)];
        }

        // Генерація випадкової статі
        public static bool GenerateRandomGender()
        {
            return _random.Next(2) == 0; // True = Male, False = Female
        }

        // Генерація випадкового досвіду для Staff
        public static int? GenerateRandomExperience()
        {
            int experience = _random.Next(-5, 21); // Від -5 до 20 років
            if (experience <= 0)
            {
                return null; // Замість виключення повертаємо null
            }
            return experience;
        }

        // Генерація випадкової спеціалізації для Engineer
        public static string? GenerateRandomSpecialization()
        {
            string specialization = specializations[_random.Next(specializations.Length)];
            if (string.IsNullOrEmpty(specialization))
            {
                return null; // Замість виключення повертаємо null
            }
            return specialization;
        }

        // Генерація випадкового відділу для Administration
        public static string? GenerateRandomDepartment()
        {
            string department = departments[_random.Next(departments.Length)];
            if (string.IsNullOrEmpty(department))
            {
                return null; // Замість виключення повертаємо null
            }
            return department;
        }

        // Створення випадкових екземплярів класів
        public static Worker? CreateRandomWorker()
        {
            int type = _random.Next(3);
            Worker? worker = null;

            switch (type)
            {
                case 0: // Staff
                    {
                        int? experience = GenerateRandomExperience();
                        if (experience.HasValue)
                        {
                            worker = new Staff(GenerateRandomName(), GenerateRandomGender(), experience.Value);
                        }
                        else
                        {
                            Console.WriteLine("Invalid experience value");
                        }
                        break;
                    }
                case 1: // Engineer
                    {
                        string? specialization = GenerateRandomSpecialization();
                        if (!string.IsNullOrEmpty(specialization))
                        {
                            worker = new Engineer(GenerateRandomName(), GenerateRandomGender(), specialization);
                        }
                        else
                        {
                            Console.WriteLine("Invalid specialization value");
                        }
                        break;
                    }
                case 2: // Administration
                    {
                        string? department = GenerateRandomDepartment();
                        if (!string.IsNullOrEmpty(department))
                        {
                            worker = new Administration(GenerateRandomName(), GenerateRandomGender(), department);
                        }
                        else
                        {
                            Console.WriteLine("Invalid department value");
                        }
                        break;
                    }
                default:
                    Console.WriteLine("Unknown worker type");
                    break;
            }

            return worker;
        }
    }
}

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
        public static int GenerateRandomExperience()
        {
            int experience = _random.Next(0, 21); // Від 0 до 20 років
          
            return experience;
        }

        // Генерація випадкової спеціалізації для Engineer
        public static string? GenerateRandomSpecialization()
        {
            string specialization = specializations[_random.Next(specializations.Length)];
            return specialization;
        }

        // Генерація випадкового відділу для Administration
        public static string? GenerateRandomDepartment()
        {
            string department = departments[_random.Next(departments.Length)];
            return department;
        }

        // Створення випадкових екземплярів класів
        public static Worker? CreateRandomWorker()
        {
            int type = _random.Next(3);

            switch (type)
            {
                case 0: // Staff
                    {
                        int experience = GenerateRandomExperience();
                        return new Staff(GenerateRandomName(), GenerateRandomGender(), experience);
                    }
                case 1: // Engineer
                    {
                        string? specialization = GenerateRandomSpecialization();
                        return new Engineer(GenerateRandomName(), GenerateRandomGender(), specialization);
                    }
                case 2: // Administration
                    {
                        string? department = GenerateRandomDepartment();
                        return new Administration(GenerateRandomName(), GenerateRandomGender(), department);
                    }
                default:
                    return null;
            }
        }
    }
}

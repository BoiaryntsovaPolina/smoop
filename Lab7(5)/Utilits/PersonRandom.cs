using Lab7.Classes;
using System;

namespace Lab7.Utilits
{
    internal class PersonRandom
    {
        private static Random rnd = new Random();
        private static string[] firstNames = { "Іван", "Олександр", "Марія", "Юлія", "Дмитро" };
        private static string[] lastNames = { "Петренко", "Коваль", "Іванова", "Шевченко", "Бондаренко" };

        public static string GetRandomFirstName()
        {
            return firstNames[rnd.Next(firstNames.Length)];
        }

        public static string GetRandomLastName()
        {
            return lastNames[rnd.Next(lastNames.Length)];
        }

        public static Person CreateRandomPerson()
        {
            string firstName = GetRandomFirstName();
            string lastName = GetRandomLastName();

            // Генеруємо випадкову дату народження (від 18 до 70 років тому)
            int years = rnd.Next(18, 70);
            int months = rnd.Next(12);
            int days = rnd.Next(28);
            DateTime birthDate = DateTime.Now.AddYears(-years).AddMonths(-months).AddDays(-days);

            return new Person(firstName, lastName, birthDate);
        }
    }
}

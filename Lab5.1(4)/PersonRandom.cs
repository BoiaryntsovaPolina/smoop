using System;

namespace Lab5._1
{
    class PersonRandom
    {
        private static Random rnd = new Random();
        private static string[] names = { "Іван Петренко", "Олександр Коваль", "Марія Іванова", "Юлія Шевченко", "Дмитро Бондаренко" };

        public static string GetRandomName()
        {
            return names[rnd.Next(names.Length)];
        }

        public static Person CreateRandomPerson()
        {
            return new Person(GetRandomName(), "+38097" + rnd.Next(1000000, 9999999).ToString());
        }
    }
}


namespace Lab6Task1
{
        public static class PersonRandom
        {
        static private Random rnd = new Random();
        static private string[] names = { "Олександр Петров", "Іван Іванов", "Марія Сидорова", "Анастасія Коваленко", "Дмитро Михайлов" };
        static private string[] majors = { "Математика", "Фізика", "Інформатика", "Економіка", "Біологія" };
        static private string[] specializations = { "ІТ", "Фінанси", "Аналітика", "Інженерія" };
        static private string[] workplaces = { "Google", "Microsoft", "Amazon", "Facebook", "Twitter" };
        static private string[][] possibleProfessions = {
            new string[] { "Програміст", "Аналітик", "Вчитель" },
            new string[] { "Інженер", "Дослідник", "Викладач" },
            new string[] { "Розробник", "DevOps", "Тестувальник" },
            new string[] { "Бухгалтер", "Фінансовий аналітик", "Менеджер" },
            new string[] { "Лаборант", "Дослідник", "Фармацевт" }
        };

        public static Student GetRandomStudent()
        {
            int nameIndex = rnd.Next(names.Length);
            int majorIndex = rnd.Next(majors.Length);

            string name = names[nameIndex];
            string major = majors[majorIndex];
            int year = rnd.Next(2015, DateTime.Now.Year + 1);
            double rating = Math.Round(rnd.NextDouble() * 100.0, 1); // В межах 0–100

            string[] professions = majorIndex < possibleProfessions.Length
                ? possibleProfessions[majorIndex]
                : new string[] { "Невідомо" };

            return new Student(name, major, year, rating, professions);
        }

        public static GraduateStudent GetRandomGraduateStudent()
        {
            int nameIndex = rnd.Next(names.Length);
            int majorIndex = rnd.Next(majors.Length);

            string name = names[nameIndex];
            string major = majors[majorIndex];
            int year = rnd.Next(2015, DateTime.Now.Year + 1);
            double rating = Math.Round(rnd.NextDouble() * 100.0, 1); // В межах 0–100
            string specialization = specializations[rnd.Next(specializations.Length)];
            string workplace = workplaces[rnd.Next(workplaces.Length)];

            string[] professions = majorIndex < possibleProfessions.Length
                ? possibleProfessions[majorIndex]
                : new string[] { "Невідомо" };

            return new GraduateStudent(name, major, year, rating, specialization, workplace, professions);
        }
    }
}
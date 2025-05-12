using Lab6Task1;

internal class Program
{
    private static void Main(string[] args)
    {
        Student[] students = new Student[5];

        // Створення масиву студентів
        for (int i = 0; i < students.Length; i++)
        {
            if (i % 2 == 0)
                students[i] = PersonRandom.GetRandomStudent();
            else
                students[i] = PersonRandom.GetRandomGraduateStudent();
        }

        // Виведення інформації про студентів
        Console.WriteLine("===== Інформація про всіх студентів =====");
        for (int i = 0; i < students.Length; i++)
        {
            Console.WriteLine($"\n--- Студент #{i+1} ---");
            students[i].Print();  // Демонстрація віртуального методу

            // Перевірка професійних уподобань (віртуальний метод)
            string testProfession = "Програміст";
            Console.WriteLine($"Чи хоче бути {testProfession}? {students[i].WantsThisProfession(testProfession)}");

            // Перевірка для випускників на відповідність спеціальності
            GraduateStudent? graduateStudent = students[i] as GraduateStudent;
            if (graduateStudent != null)
            {
                Console.WriteLine($"Чи відповідає спеціалізація напряму навчання? {graduateStudent.IsProfessionMatchingMajor()}");
            }
        }

        // Демонстрація роботи з різними конструкторами та методами
        Console.WriteLine("\n===== Додаткові тести =====");

        // Студент за замовчуванням
        Student defaultStudent = new Student();
        Console.WriteLine("\nСтудент за замовчуванням:");
        defaultStudent.Print();

        // Студент із параметрами
        string[] programmingProfessions = { "Розробник ПЗ", "Веб-розробник", "Мобільний розробник" };
        Student student1 = new Student("Іваненко Іван", "Інформатика", 2022, 90.5, programmingProfessions);
        Console.WriteLine("\nСтудент із параметрами:");
        student1.Print();

        // Коротка інформація
        Console.WriteLine("\nКоротка інформація:");
        Console.WriteLine(student1.ToShortString());

        // Зміна спеціальності та рейтингу
        student1.ChangeMajor("Кібербезпека");
        student1.ChangeRating(95.2);
        Console.WriteLine("\nПісля зміни спеціальності та рейтингу:");
        student1.Print();

        // Порівняння студентів
        Student student2 = new Student("Іваненко Іван", "Кібербезпека", 2022, 95.2);
        Student student3 = new Student("Петренко Петро", "Комп'ютерні науки", 2021, 80.1);

        Console.WriteLine("\nПорівняння студентів:");
        Console.WriteLine("Student1 == Student2? " + student1.Equals(student2));
        Console.WriteLine("Student1 == Student3? " + student1.Equals(student3));

        Console.WriteLine("\nHashCode для Student1: " + student1.GetHashCode());
        Console.WriteLine("HashCode для Student2: " + student2.GetHashCode());
        Console.WriteLine("HashCode для Student3: " + student3.GetHashCode());

        // Тестування студентів-випускників
        Console.WriteLine("\n===== Тестування студентів-випускників =====");

        string[] securityProfessions = { "Спеціаліст з кібербезпеки", "Аудитор", "Пентестер" };
        GraduateStudent graduate1 = new GraduateStudent(
            "Петренко Максим",
            "Інформатика",
            2020,
            97.5,
            "Кібербезпека",
            "Microsoft",
            securityProfessions
        );

        Console.WriteLine("\nІнформація про випускника:");
        graduate1.Print();

        Console.WriteLine($"\nЧи відповідає спеціалізація напряму навчання? {graduate1.IsProfessionMatchingMajor()}");
        Console.WriteLine($"Чи хоче бути Спеціаліст з кібербезпеки? {graduate1.WantsThisProfession("Спеціаліст з кібербезпеки")}");
        Console.WriteLine($"Чи хоче працювати в Microsoft? {graduate1.WantsThisProfession("Microsoft")}");
        Console.WriteLine($"Чи хоче бути Програмістом? {graduate1.WantsThisProfession("Програміст")}");

        // Тестування обробки null-значень
        Console.WriteLine("\n===== Тестування обробки null-значень =====");

        Student nullStudent = new Student(null, null, 2025, 105.0);
        Console.WriteLine("\nСтудент з null-значеннями:");
        nullStudent.Print();

        GraduateStudent nullGraduate = new GraduateStudent(null, null, 2018, -5.0, null, null);
        Console.WriteLine("\nВипускник з null-значеннями:");
        nullGraduate.Print();
    }
}

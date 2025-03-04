namespace Lab1Task3
{
    internal class Program
    {

        static int AskQuestion(string question, int correctAnswer)
        {
            Console.WriteLine(question);
            Console.Write("Ваша відповідь (або -1 для виходу): ");

            if (int.TryParse(Console.ReadLine(), out int answer))
            {
                if (answer == -1)
                {
                    Console.WriteLine("Тест завершено достроково.");
                    Environment.Exit(0);
                }
                return answer == correctAnswer ? 1 : 0;
            }

            return 0; // Якщо введено не число, відповідь вважається неправильною.
        }


        static void Main(string[] args)
        {
            int score = 0;

            Console.WriteLine("Ласкаво просимо до тесту 'Перевір свої можливості'!\n");
            Console.WriteLine("Введіть -1 у будь-який момент, щоб завершити тест.\n");

            score += AskQuestion("Професор ліг спати о 8 годині, а встав о 9 годині. Скільки годин проспав професор?", 1);
            score += AskQuestion("На двох руках десять пальців. Скільки пальців на 10 руках?", 50);
            score += AskQuestion("Скільки цифр у дюжині?", 2);
            score += AskQuestion("Скільки потрібно зробити розпилів, щоб розпиляти колоду на 12 частин?", 11);
            score += AskQuestion("Лікар зробив три уколи в інтервалі 30 хвилин. Скільки часу він витратив?", 30);
            score += AskQuestion("Скільки цифр 9 в інтервалі 1-100?", 1);
            score += AskQuestion("Пастух мав 30 овець. Усі, окрім однієї, розбіглися. Скільки овець лишилося?", 1);

            Console.WriteLine("\nВаш результат:");
            switch (score)
            {
                case 7: Console.WriteLine("Геній"); break;
                case 6: Console.WriteLine("Ерудит"); break;
                case 5: Console.WriteLine("Нормальний"); break;
                case 4: Console.WriteLine("Здібності середні"); break;
                case 3: Console.WriteLine("Здібності нижче середнього"); break;
                default: Console.WriteLine("Вам треба відпочити!"); break;
            }
            Console.ReadKey();
        }
    }
}





namespace Lab2Task3
{
    internal class Program
    {
        // Загальні змінні
        static int playerLives; // Кількість життів гравця
        static int playerScore = 0; // Очки гравця
        static int computerScore = 0; // Очки комп'ютера
        static Random random = new Random(); // Об'єкт для генерації випадкових чисел

        // Основна програма
        static void Main(string[] args)
        {
            DisplayIntro();

            // Перший рівень гри (діапазон від 1 до 10, 3 раунди)
            bool firstLevelCompleted = PlayLevel(1, 1, 10, 3);

            // Якщо гравець завершив перший рівень, пропонуємо перейти на другий рівень
            if (firstLevelCompleted)
            {
                Console.Write("Бажаєте продовжити на другий рівень? (так/ні): ");
                if (Console.ReadLine().ToLower() == "так")
                {
                    // Другий рівень гри (діапазон від 10 до 100, 2 раунди)
                    PlayLevel(2, 10, 100, 2);
                }
            }

            // Виводимо фінальні результати гри
            DisplayResults();
        }

        // Виведення вступної інформації
        static void DisplayIntro()
        {
            Console.WriteLine("Ласкаво просимо до гри GUESS MY NUMBER!");
            Console.WriteLine("Ваша мета – вгадати число, яке загадує комп'ютер.");
            Console.WriteLine("Будьте обережні: за кожну неправильну спробу ви втрачаєте життя!");
        }

        // Функція, що відповідає за логіку гри на рівні
        static bool PlayLevel(int level, int min, int max, int rounds)
        {
            // Визначаємо початкову кількість життів для рівня
            playerLives = GetLivesForLevel(level, min, max);
            int initialLives = playerLives;

            // Виводимо інформацію про рівень
            Console.WriteLine($"\n--- Рівень {level} ---");
            Console.WriteLine($"Діапазон чисел: {min}-{max}");
            Console.WriteLine($"Кількість життів на старті: {playerLives}");

            // Ігровий процес для кожного раунду
            for (int round = 1; round <= rounds; round++)
            {
                Console.WriteLine($"\nРаунд {round}. У вас {playerLives} життів.");
                // Генеруємо випадкове число для вгадування
                int numberToGuess = random.Next(min, max + 1);
                // Гравець намагається вгадати число
                bool roundWon = PlayRound(numberToGuess, initialLives, level);

                // Якщо гравець виграв раунд, додаємо очки
                if (roundWon)
                {
                    UpdateScores(level, initialLives, true);
                }
                else
                {
                    // Якщо гравець програв, додаємо очки комп'ютеру та завершуємо рівень
                    UpdateScores(level, initialLives, false);
                    return false;
                }

                // Відновлюємо кількість життів перед наступним раундом
                playerLives = initialLives;
            }

            // Повертаємо true, якщо гравець успішно пройшов рівень
            return true;
        }

        // Функція, що визначає кількість життів для кожного рівня
        static int GetLivesForLevel(int level, int min, int max)
        {
            // Для першого рівня життів буде половина від діапазону, для другого — чверть
            return (level == 1) ? (max - min + 1) / 2 : (max - min + 1) / 4;
        }

        // Функція для гри в одному раунді
        static bool PlayRound(int numberToGuess, int initialLives, int level)
        {
            // Цикл, який триває, поки у гравця є життя
            while (IsAlive())
            {
                Console.Write("Введіть вашу спробу: ");
                if (int.TryParse(Console.ReadLine(), out int guess)) // Перевіряємо введене число
                {
                    if (guess == numberToGuess) // Якщо вгадано, раунд виграно
                    {
                        Console.WriteLine("Вітаю! Ви вгадали число!");
                        return true;
                    }
                    else
                    {
                        // Якщо число неправильне, втрачаємо життя
                        LoseLife();
                        Console.WriteLine($"Неправильно! У вас залишилось {playerLives} життів.");

                        if (IsAlive()) // Якщо ще є життя, пропонуємо підказку
                        {
                            Console.Write("Хочете підказку? (так/ні): ");
                            if (Console.ReadLine().ToLower() == "так")
                            {
                                GiveHint(numberToGuess, guess); // Даемо підказку
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Будь ласка, введіть число!"); // Якщо введено не число
                }
            }

            // Якщо гравець втратив всі життя, раунд програний
            return false;
        }

        // Функція для підказки (число більше або менше)
        static void GiveHint(int numberToGuess, int guess)
        {
            LoseLife(); // За підказку віднімається одне життя
            Console.WriteLine(numberToGuess > guess ? "Загадане число більше." : "Загадане число менше.");
            Console.WriteLine($"Після підказки залишилось {playerLives} життів.");
        }

        // Перевірка, чи залишилися в гравця життя
        static bool IsAlive()
        {
            return playerLives > 0;
        }

        // Зменшуємо кількість життів гравця
        static void LoseLife()
        {
            playerLives--;
        }

        // Оновлення очок після кожного раунду
        static void UpdateScores(int level, int initialLives, bool roundWon)
        {
            if (roundWon) // Якщо раунд виграно
            {
                // Оцінка гравця: життів помножити на коефіцієнт рівня
                int roundScore = playerLives * (level == 1 ? 5 : 10);
                AddScore(roundScore);
                Console.WriteLine($"Ви виграли раунд! Ваш рахунок: {playerScore}");
            }
            else // Якщо програно, очки отримує комп'ютер
            {
                int roundScore = initialLives * (level == 1 ? 5 : 10);
                computerScore += roundScore;
                Console.WriteLine($"Ви програли раунд! Комп'ютер отримує {roundScore} очок.");
            }
        }

        // Функція для додавання очок гравцеві
        static void AddScore(int roundScore)
        {
            playerScore += roundScore;
        }

        // Виведення підсумкових результатів
        static void DisplayResults()
        {
            Console.WriteLine($"\nГра завершена! Ваш підсумковий рахунок: {playerScore}");
            Console.WriteLine($"Очки комп'ютера: {computerScore}");
        }
    }
}
   



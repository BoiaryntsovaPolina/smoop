using Lab6Task2;

internal class Program
{
    private static void Main(string[] args)
    {
        // Створення масиву Worker (абстрактного типу)
        Worker?[] workers = new Worker?[10]; // 10 випадкових працівників

        // Заповнюємо масив випадковими працівниками
        for (int i = 0; i < workers.Length; i++)
        {
            workers[i] = WorkerRandom.CreateRandomWorker();
        }

        // Лічильник для кількості чоловіків
        int maleCount = 0;
        int validWorkersCount = 0;

        // Виведення інформації про працівників і підрахунок кількості чоловіків
        Console.WriteLine("List of all workers:");
        for (int i = 0; i < workers.Length; i++)
        {
            Worker? worker = workers[i];
            if (worker != null)
            {
                if (worker.IsValid())
                {
                    Console.Write($"{i+1}. ");
                    worker.ShowInfo();
                    validWorkersCount++;

                    if (worker.IsMale)
                    {
                        maleCount++;
                    }
                }
                else
                {
                    Console.WriteLine($"{i+1}. Invalid worker data");
                }
            }
            else
            {
                Console.WriteLine($"{i+1}. Worker creation failed");
            }
        }

        // Виведення результату запиту
        if (validWorkersCount > 0)
        {
            Console.WriteLine($"\nTotal valid workers: {validWorkersCount}");
            Console.WriteLine($"Total male workers: {maleCount}");

            // Виведення процентного співвідношення
            double malePercentage = (double)maleCount / validWorkersCount * 100;
            Console.WriteLine($"Percentage of male workers: {malePercentage:F2}%");
        }
        else
        {
            Console.WriteLine("\nNo valid workers found.");
        }
    }
}
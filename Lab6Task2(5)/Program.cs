using Lab6Task2;

internal class Program
{
    private static void Main(string[] args)
    {
        // Створення масиву Worker 
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
            if (workers[i] != null)
            {
                Worker? worker = workers[i];
                Console.Write($"{i+1}. ");
                worker.Print();
                validWorkersCount++;

                if (worker.IsMale)
                {
                    maleCount++;
                }
            }
            else
            {
                Console.WriteLine("Не створено.");
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

        // Демонстрація методів ToString, Equals, GetHashCode
        Console.WriteLine("\n===== Demonstration of ToString, Equals, GetHashCode =====");

        if (workers[0] != null && workers[1] != null)
        {
            Console.WriteLine($"\nToString demonstration:");
            Console.WriteLine($"worker1.ToString(): {workers[0].ToString()}");
            Console.WriteLine($"worker2.ToString(): {workers[1].ToString()}");

            Console.WriteLine($"\nEquals demonstration:");
            Console.WriteLine($"worker1.Equals(worker2): {workers[0].Equals(workers[1])}");

            Console.WriteLine($"\nGetHashCode demonstration:");
            Console.WriteLine($"worker1.GetHashCode(): {workers[0].GetHashCode()}");
            Console.WriteLine($"worker2.GetHashCode(): {workers[1].GetHashCode()}");

            // Створення копії для демонстрації
            Worker clone;
            if (workers[0] is Staff staff)
            {
                clone = new Staff(staff.Name, staff.IsMale, staff.ExperienceYears);
                Console.WriteLine($"\nCreated a clone of worker1");
                Console.WriteLine($"clone.ToString(): {clone.ToString()}");
                Console.WriteLine($"worker1.Equals(clone): {workers[0].Equals(clone)}");
                Console.WriteLine($"worker1.GetHashCode() == clone.GetHashCode(): {workers[0].GetHashCode() == clone.GetHashCode()}");

                // Демонстрація конструктора за замовчуванням
                Staff defaultStaff = new Staff();
                Console.WriteLine($"\nDefault constructor demonstration:");
                Console.WriteLine($"defaultStaff.ToString(): {defaultStaff.ToString()}");
            }
            else if (workers[0] is Engineer engineer)
            {
                clone = new Engineer(engineer.Name, engineer.IsMale, engineer.Specialization);
                Console.WriteLine($"\nCreated a clone of worker1");
                Console.WriteLine($"clone.ToString(): {clone.ToString()}");
                Console.WriteLine($"worker1.Equals(clone): {workers[0].Equals(clone)}");
                Console.WriteLine($"worker1.GetHashCode() == clone.GetHashCode(): {workers[0].GetHashCode() == clone.GetHashCode()}");

                // Демонстрація конструктора за замовчуванням
                Engineer defaultEngineer = new Engineer();
                Console.WriteLine($"\nDefault constructor demonstration:");
                Console.WriteLine($"defaultEngineer.ToString(): {defaultEngineer.ToString()}");
            }
            else if (workers[0] is Administration admin)
            {
                clone = new Administration(admin.Name, admin.IsMale, admin.Department);
                Console.WriteLine($"\nCreated a clone of worker1");
                Console.WriteLine($"clone.ToString(): {clone.ToString()}");
                Console.WriteLine($"worker1.Equals(clone): {workers[0].Equals(clone)}");
                Console.WriteLine($"worker1.GetHashCode() == clone.GetHashCode(): {workers[0].GetHashCode() == clone.GetHashCode()}");

                // Демонстрація конструктора за замовчуванням
                Administration defaultAdmin = new Administration();
                Console.WriteLine($"\nDefault constructor demonstration:");
                Console.WriteLine($"defaultAdmin.ToString(): {defaultAdmin.ToString()}");
            }
        }
    }
}


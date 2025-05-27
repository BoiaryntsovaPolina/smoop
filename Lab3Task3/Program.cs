namespace Lab3Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n;
            int[,] A; // Прямокутний масив
            int[,] B; // Прямокутний масив
            int[] Z;
            int g;

            Console.WriteLine("Оберіть спосіб отримання початкових значень:\n1 - Ввести вручну\n2 - Зчитати з файлу\n3 - Згенерувати випадково");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 2:
                    try
                    {
                        using (StreamReader sr = new StreamReader("input.txt"))
                        {
                            n = Convert.ToInt32(sr.ReadLine()); // Читаємо розмірність матриці з файлу
                            A = new int[n, n]; // Створюємо прямокутний масив для A
                            B = new int[n, n]; // Створюємо прямокутний масив для B
                            Z = new int[n];

                            string[] line = sr.ReadLine().Split();
                            for (int i = 0; i < n; i++)
                                Z[i] = Convert.ToInt32(line[i]);

                            for (int i = 0; i < n; i++)
                            {
                                line = sr.ReadLine().Split();
                                for (int j = 0; j < n; j++)
                                    A[i, j] = Convert.ToInt32(line[j]);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Помилка читання файлу: " + e.Message);
                        return;
                    }
                    break;

                case 1:
                    Console.Write("Введіть розмірність матриці n: ");
                    n = Convert.ToInt32(Console.ReadLine());

                    A = new int[n, n]; // Створюємо прямокутний масив для A
                    B = new int[n, n]; // Створюємо прямокутний масив для B
                    Z = new int[n];

                    Console.WriteLine("Введіть елементи вектора Z:");
                    for (int i = 0; i < n; i++)
                        Z[i] = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Введіть елементи матриці A:");
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < n; j++)
                            A[i, j] = Convert.ToInt32(Console.ReadLine());
                    break;

                case 3:
                    Console.Write("Введіть розмірність матриці n: ");
                    n = Convert.ToInt32(Console.ReadLine());

                    A = new int[n, n]; // Створюємо прямокутний масив для A
                    B = new int[n, n]; // Створюємо прямокутний масив для B
                    Z = new int[n];

                    Random rnd = new Random();
                    for (int i = 0; i < n; i++)
                        Z[i] = rnd.Next(1, 101);

                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < n; j++)
                            A[i, j] = rnd.Next(1, 101);
                    break;

                default:
                    Console.WriteLine("Неправильний вибір!");
                    return;
            }

            g = Z.Min(); // Знайти найменший елемент вектора Z

            // Обчислення матриці B як добутку g на A
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    B[i, j] = g * A[i, j];

            Console.WriteLine("\nМатриця A:");
            PrintMatrix(A);

            Console.WriteLine("\nВектор Z:");
            PrintVector(Z);

            Console.WriteLine("\nМатриця B:");
            PrintMatrix(B);

            // Замінити діагональні елементи матриці B на найбільші значення в кожному стовпці
            for (int i = 0; i < n; i++)
            {
                int maxColumn = B[0, i];
                for (int j = 1; j < n; j++)
                    if (B[j, i] > maxColumn)
                        maxColumn = B[j, i];

                B[i, i] = maxColumn;
            }

            Console.WriteLine("\nМатриця B (змінена діагональ):");
            PrintMatrix(B);

            using (StreamWriter sw = new StreamWriter("output.txt"))
            {
                sw.WriteLine("Матриця B:");
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                        sw.Write(B[i, j] + " ");
                    sw.WriteLine();
                }
            }

            Console.WriteLine("\nРезультат збережено у файл 'output.txt'.");
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++) // matrix.GetLength(0) - кількість рядків
            {
                for (int j = 0; j < matrix.GetLength(1); j++) // matrix.GetLength(1) - кількість стовпців
                    Console.Write(matrix[i, j] + "\t");
                Console.WriteLine();
            }
        }

        static void PrintVector(int[] vector)
        {
            foreach (int v in vector)
                Console.Write(v + " ");
            Console.WriteLine();
        }
    }
}

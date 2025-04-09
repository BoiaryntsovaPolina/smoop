

namespace Lab3Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n;
            int[][] A;
            int[][] B;
            int[] Z;
            int g;

            Console.WriteLine("Оберіть спосіб отримання початкових значень:\n1 - Ввести вручну\n2 - Зчитати з файлу\n3 - Згенерувати випадково");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 2:
                    if (!File.Exists("input.txt"))
                    {
                        Console.WriteLine("Помилка: файл 'input.txt' не знайдено!");
                        return;
                    }

                    string[] lines = File.ReadAllLines("input.txt");
                    int expectedLines = lines.Length;

                    if (expectedLines < 1 || !int.TryParse(lines[0], out n) || n <= 0)
                    {
                        Console.WriteLine("Помилка: некоректне значення розмірності матриці!");
                        return;
                    }

                    if (expectedLines < 1 + n + n * n)
                    {
                        Console.WriteLine("Помилка: у файлі недостатньо даних!");
                        return;
                    }

                    A = new int[n][];
                    B = new int[n][];
                    Z = new int[n];

                    int index = 1;

                    for (int i = 0; i < n; i++)
                    {
                        if (!int.TryParse(lines[index++], out Z[i]))
                        {
                            Console.WriteLine($"Помилка: некоректний елемент вектора Z у рядку {index}.");
                            return;
                        }
                    }

                    for (int i = 0; i < n; i++)
                    {
                        A[i] = new int[n];
                        B[i] = new int[n];
                        for (int j = 0; j < n; j++)
                        {
                            if (!int.TryParse(lines[index++], out A[i][j]))
                            {
                                Console.WriteLine($"Помилка: некоректний елемент матриці A у рядку {index}.");
                                return;
                            }
                        }
                    }
                    break;

                case 1:
                    Console.Write("Введіть розмірність матриці n: ");
                    n = Convert.ToInt32(Console.ReadLine());

                    A = new int[n][];
                    B = new int[n][];
                    Z = new int[n];

                    Console.WriteLine("Введіть елементи вектора Z:");
                    for (int i = 0; i < n; i++)
                        Z[i] = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Введіть елементи матриці A:");
                    for (int i = 0; i < n; i++)
                    {
                        A[i] = new int[n];
                        B[i] = new int[n];
                        for (int j = 0; j < n; j++)
                            A[i][j] = Convert.ToInt32(Console.ReadLine());
                    }
                    break;

                case 3:
                    Console.Write("Введіть розмірність матриці n: ");
                    n = Convert.ToInt32(Console.ReadLine());

                    A = new int[n][];
                    B = new int[n][];
                    Z = new int[n];

                    Random rnd = new Random();
                    for (int i = 0; i < n; i++)
                        Z[i] = rnd.Next(1, 101);

                    for (int i = 0; i < n; i++)
                    {
                        A[i] = new int[n];
                        B[i] = new int[n];
                        for (int j = 0; j < n; j++)
                            A[i][j] = rnd.Next(1, 101);
                    }
                    break;

                default:
                    Console.WriteLine("Неправильний вибір!");
                    return;
            }

            g = Z.Min(); // Знайти найменший елемент вектора Z

            // Обчислення матриці B як добутку g на A
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    B[i][j] = g * A[i][j];

            Console.WriteLine("\nМатриця A:");
            PrintMatrix(A);

            Console.WriteLine("\nВектор Z:");
            PrintVector(Z);

            Console.WriteLine("\nМатриця B:");
            PrintMatrix(B);

            // Замінити діагональні елементи матриці B на найбільші значення в кожному стовпці
            for (int j = 0; j < n; j++)
            {
                int maxColumn = B[0][j];
                for (int i = 1; i < n; i++)
                    if (B[i][j] > maxColumn)
                        maxColumn = B[i][j];

                B[j][j] = maxColumn;
            }

            Console.WriteLine("\nМатриця B (змінена діагональ):");
            PrintMatrix(B);

            using (StreamWriter sw = new StreamWriter("output.txt"))
            {
                sw.WriteLine("Матриця B:");
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                        sw.Write(B[i][j] + " ");
                    sw.WriteLine();
                }
            }

            Console.WriteLine("\nРезультат збережено у файл 'output.txt'.");
        }

        static void PrintMatrix(int[][] matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var value in row)
                    Console.Write(value + "\t");
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

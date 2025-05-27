//namespace Lab1Task4
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            try
//            {
//                StreamReader sr = new StreamReader("input.txt");
//                int fuelCapacity = int.Parse(sr.ReadLine());
//                int distanceAB = int.Parse(sr.ReadLine());
//                int distanceBC = int.Parse(sr.ReadLine());
//                int cargoWeight = int.Parse(sr.ReadLine());
//                sr.Close();

//                if (cargoWeight > 2000)
//                {
//                    Console.WriteLine("Літак не може підняти таку вагу.");
//                    return;
//                }

//                int fuelConsumption = GetFuelConsumption(cargoWeight);
//                int fuelNeededAB = distanceAB * fuelConsumption;
//                int fuelNeededBC = distanceBC * fuelConsumption;

//                if (fuelNeededAB > fuelCapacity)
//                {
//                    Console.WriteLine("Неможливо долетіти до пункту В.");
//                    return;
//                }

//                int fuelRemaining = fuelCapacity - fuelNeededAB;
//                int additionalFuelNeeded = 0;
//                if (fuelNeededBC > fuelRemaining)
//                {
//                    additionalFuelNeeded = fuelNeededBC - fuelRemaining;
//                }

//                if (fuelNeededBC > fuelCapacity)
//                {
//                    Console.WriteLine("Неможливо долетіти від В до С навіть з повним баком.");
//                }
//                else
//                {
//                    Console.WriteLine($"Мінімальна кількість палива для дозаправки в пункті В: {additionalFuelNeeded} літрів.");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Помилка обробки файлу: " + ex.Message);
//            }
//            Console.ReadKey();
//        }

//        static int GetFuelConsumption(int weight)
//        {
//            if (weight <= 500) return 1;
//            if (weight <= 1000) return 4;
//            if (weight <= 1500) return 7;
//            return 9; // Для ваги 1501-2000 кг
//        }
//    }

//}
   



namespace Lab1Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Читаємо вхідні дані
                double fuelCapacity, distanceAB, distanceBC, cargoWeight;
                using (StreamReader sr = new StreamReader("input.txt"))
                {
                    fuelCapacity = ReadPositiveDouble(sr, "Ємність бака");
                    distanceAB = ReadPositiveDouble(sr, "Відстань A-B");
                    distanceBC = ReadPositiveDouble(sr, "Відстань B-C");
                    cargoWeight = ReadPositiveDouble(sr, "Вага вантажу");
                }

                // Перевіряємо вантажопідйомність літака
                if (cargoWeight > 2000)
                {
                    Console.WriteLine("Літак не може підняти таку вагу.");
                    return;
                }

                // Запускаємо основний розрахунок
                CalculateFuelRequirements(fuelCapacity, distanceAB, distanceBC, cargoWeight);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }

            Console.ReadKey();
        }

        // Метод для перевірки та зчитування позитивного числа
        static double ReadPositiveDouble(StreamReader sr, string parameterName)
        {
            if (!double.TryParse(sr.ReadLine(), out double value) || value <= 0)
                throw new Exception($"Невірне або відсутнє значення для: {parameterName}");
            return value;
        }

        // Основна логіка розрахунку пального
        static void CalculateFuelRequirements(double fuelCapacity, double distanceAB, double distanceBC, double cargoWeight)
        {
            double fuelConsumption = GetFuelConsumption(cargoWeight);
            double fuelNeededAB = distanceAB * fuelConsumption;
            double fuelNeededBC = distanceBC * fuelConsumption;

            // Чи можна долетіти до B
            if (fuelNeededAB > fuelCapacity)
            {
                Console.WriteLine("Неможливо долетіти до пункту В.");
                return;
            }

            double fuelRemaining = fuelCapacity - fuelNeededAB;
            double additionalFuelNeeded = Math.Max(0, fuelNeededBC - fuelRemaining);

            // Чи можна долетіти до C
            if (fuelNeededBC > fuelCapacity)
            {
                Console.WriteLine("Неможливо долетіти від В до С навіть з повним баком.");
            }
            else
            {
                Console.WriteLine($"Мінімальна кількість палива для дозаправки в пункті В: {additionalFuelNeeded:F2} літрів.");
            }
        }

        // Метод визначення витрати пального
        static double GetFuelConsumption(double weight)
        {
            if (weight <= 500) return 1;
            if (weight <= 1000) return 4;
            if (weight <= 1500) return 7;
            return 9; // Для ваги 1501-2000 кг
        }
    }
}

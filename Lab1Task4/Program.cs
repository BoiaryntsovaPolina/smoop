namespace Lab1Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StreamReader sr = new StreamReader("input.txt");
                int fuelCapacity = int.Parse(sr.ReadLine());
                int distanceAB = int.Parse(sr.ReadLine());
                int distanceBC = int.Parse(sr.ReadLine());
                int cargoWeight = int.Parse(sr.ReadLine());
                sr.Close();

                if (cargoWeight > 2000)
                {
                    Console.WriteLine("Літак не може підняти таку вагу.");
                    return;
                }

                int fuelConsumption = GetFuelConsumption(cargoWeight);
                int fuelNeededAB = distanceAB * fuelConsumption;
                int fuelNeededBC = distanceBC * fuelConsumption;

                if (fuelNeededAB > fuelCapacity)
                {
                    Console.WriteLine("Неможливо долетіти до пункту В.");
                    return;
                }

                int fuelRemaining = fuelCapacity - fuelNeededAB;
                int additionalFuelNeeded = 0;
                if (fuelNeededBC > fuelRemaining)
                {
                    additionalFuelNeeded = fuelNeededBC - fuelRemaining;
                }

                if (fuelNeededBC > fuelCapacity)
                {
                    Console.WriteLine("Неможливо долетіти від В до С навіть з повним баком.");
                }
                else
                {
                    Console.WriteLine($"Мінімальна кількість палива для дозаправки в пункті В: {additionalFuelNeeded} літрів.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка обробки файлу: " + ex.Message);
            }
            Console.ReadKey();
        }

        static int GetFuelConsumption(int weight)
        {
            if (weight <= 500) return 1;
            if (weight <= 1000) return 4;
            if (weight <= 1500) return 7;
            return 9; // Для ваги 1501-2000 кг
        }
    }

}
   

namespace Lab7Task2_2_
{
    internal class CommonUtils
    {
        private static Random rnd = new Random();

        // Масиви даних для випадкової генерації
        private static string[] materials = { "Алюміній", "Карбон", "Нейлон", "Композит", "Титан", "Шовк", "Сталь" };

        // Повертає випадковий матеріал
        public static string GetRandomMaterial()
        {
            return materials[rnd.Next(materials.Length)];
        }

        // Повертає випадковий рік виробництва (від 2000 до поточного року)
        public static int GetRandomYear()
        {
            return rnd.Next(2000, DateTime.Now.Year + 1);
        }

        // Повертає випадкову вагу (від min до max)
        public static double GetRandomWeight(double min, double max)
        {
            return Math.Round(min + (max - min) * rnd.NextDouble(), 1);
        }        
    }
}

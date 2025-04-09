using System.Text.RegularExpressions;

namespace Lab4Task1._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] testStrings = {
                "abcdefghijklmnopqrstuv18340",
                "abcdefg18340",
                "abcdefghijklmnopqrstuv18341",
                "abcdefghijklmnopqrstuv18340 ",
                " abcdefghijklmnopqrstuv18340",
                "abcdefghijklmnopqrstuv18340",
                "abcdefghijklmnopqrstuv18340extra"
            };

            string pattern = @"^abcdefghijklmnopqrstuv18340$";

            foreach (string value in testStrings)
            {
                if (Regex.IsMatch(value, pattern))
                    Console.WriteLine($"'{value}' — валідний рядок.");
                else
                    Console.WriteLine($"'{value}' — невірний рядок.");
            }

            Console.ReadKey();
        }
    }
}

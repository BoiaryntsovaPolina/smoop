using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9Task3_3_
{
    internal class ConsoleInput
    {
        public static string ReadString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? string.Empty;
        }

        public static int ReadInt(string prompt)
        {
            int value;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Невірне введення. Введіть ціле число: ");
            }
            return value;
        }

        public static int ReadPositiveInt(string prompt)
        {
            int value;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out value) || value <= 0)
            {
                Console.Write("Невірне введення. Введіть додатне ціле число: ");
            }
            return value;
        }

        public static string ReadStringWithDefault(string prompt, string defaultValue)
        {
            Console.Write($"{prompt} ({defaultValue}): ");
            string input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? defaultValue : input;
        }

        public static int ReadIntWithDefault(string prompt, int defaultValue)
        {
            Console.Write($"{prompt} ({defaultValue}): ");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int value))
            {
                return defaultValue;
            }
            return value;
        }
    }
}

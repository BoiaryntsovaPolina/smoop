using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8Task1_2_
{
    internal class NumberChecker
    {
        public bool CheckNumber(int number, Predicate<int> checkMethod)
        {
            // Викликаємо переданий метод для перевірки числа
            return checkMethod(number);
        }

        // Метод для перевірки, чи є число простим
        public bool IsPrime(int number)
        {
            // Якщо число менше 2, то воно не є простим
            if (number < 2)
                return false;

            // Перевіряємо, чи ділиться число на будь-яке інше число
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        // Метод для перевірки, чи є число числом Фібоначчі
        public bool IsFibonacci(int number)
        {
            if (number < 0)
                return false;

            if (number == 0 || number == 1)
                return true;


            return IsPerfectSquare(5 * number * number + 4) ||
                   IsPerfectSquare(5 * number * number - 4);
        }

        // Допоміжний метод для перевірки, чи є число повним квадратом
        private bool IsPerfectSquare(int number)
        {
            int sqrt = (int)Math.Sqrt(number);
            return sqrt * sqrt == number;
        }
    }
}

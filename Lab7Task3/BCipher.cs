using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task3
{
    internal class BCipher : ICipher
    {
        public string encode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            char[] result = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                char currentChar = text[i];

                // Перевіряємо, чи є символ літерою
                if (char.IsLetter(currentChar))
                {
                    bool isUpper = char.IsUpper(currentChar); // Визначаємо регістр
                    char lowerChar = char.ToLower(currentChar); // Перетворюємо на нижній регістр для простішої обробки

                    char newChar;

                    if (lowerChar >= 'а' && lowerChar <= 'я')
                    {
                        newChar = (char)('а' + ('я' - lowerChar));
                    }
                    else if (lowerChar >= 'a' && lowerChar <= 'z')
                    {
                        newChar = (char)('a' + ('z' - lowerChar));
                    }
                    else
                    {
                        newChar = lowerChar;
                    }

                    result[i] = isUpper ? char.ToUpper(newChar) : newChar;
                }
                else
                {
                    result[i] = currentChar;
                }
            }

            return new string(result);
        }

        public string decode(string text)
        {
            // Використовуємо той самий алгоритм, що й для шифрування,
            // оскільки заміна літер симетрична
            return encode(text);
        }

    }
}

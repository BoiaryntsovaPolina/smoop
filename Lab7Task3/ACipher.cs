using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task3
{
    internal class ACipher : ICipher   
    {
        public string decode(string text)
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

                    // Визначаємо, чи це українська літера
                    if (lowerChar >= 'а' && lowerChar <= 'я')
                    {
                        // Обробка українських літер
                        char newChar;
                        if (lowerChar == 'а')
                            newChar = 'я'; // Якщо 'а', то циклічно повертаємось до 'я'
                        else
                            newChar = (char)(lowerChar - 1); // Зсув на одну позицію назад

                        // Відновлюємо оригінальний регістр
                        result[i] = isUpper ? char.ToUpper(newChar) : newChar;
                    }
                    // Для латинських літер
                    else if (lowerChar >= 'a' && lowerChar <= 'z')
                    {
                        char newChar;
                        if (lowerChar == 'a')
                            newChar = 'z'; 
                        else
                            newChar = (char)(lowerChar - 1); 

                        result[i] = isUpper ? char.ToUpper(newChar) : newChar;
                    }
                    else
                    {
                        // Інші символи залишаємо без змін
                        result[i] = currentChar;
                    }
                }
                else
                {
                    // Якщо символ не є літерою, залишаємо його без змін
                    result[i] = currentChar;
                }
            }

            // Повертаємо дешифрований рядок
            return new string(result);
        }

        public string encode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            char[] result = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                char currentChar = text[i];

                if (char.IsLetter(currentChar))
                {
                    bool isUpper = char.IsUpper(currentChar); // Визначаємо регістр
                    char lowerChar = char.ToLower(currentChar); // Перетворюємо на нижній регістр для простішої обробки

                    if (lowerChar >= 'а' && lowerChar <= 'я')
                    {
                        char newChar;
                        if (lowerChar == 'я')
                            newChar = 'а';
                        else
                            newChar = (char)(lowerChar + 1);

                        result[i] = isUpper ? char.ToUpper(newChar) : newChar;
                    }
                    else if (lowerChar >= 'a' && lowerChar <= 'z')
                    {
                        char newChar;
                        if (lowerChar == 'z')
                            newChar = 'a'; 
                        else
                            newChar = (char)(lowerChar + 1); 

                        result[i] = isUpper ? char.ToUpper(newChar) : newChar;
                    }
                    else
                    {
                        result[i] = currentChar;
                    }
                }
                else
                {
                    result[i] = currentChar;
                }
            }

            return new string(result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9Task2
{
    internal class TextCensor
    {
        // Метод для цензури тексту
        public string CensorText(string text, string[] bannedWords)
        {
            string result = text;

            foreach (string bannedWord in bannedWords)
            {
                if (!string.IsNullOrEmpty(bannedWord))
                {
                    // Створюємо рядок із зірочок відповідної довжини
                    string censorship = new string('*', bannedWord.Length);

                    // Замінюємо всі входження слова (без урахування регістру)
                    result = result.Replace(bannedWord, censorship, StringComparison.OrdinalIgnoreCase);
                }
            }

            return result;
        }

        // Метод для підрахунку кількості замінених слів
        public int CountCensoredWords(string originalText, string[] bannedWords)
        {
            int count = 0;

            string lowerOriginalText = originalText.ToLower();

            foreach (string bannedWord in bannedWords)
            {
                if (!string.IsNullOrEmpty(bannedWord))
                {
                    string lowerBannedWord = bannedWord.ToLower();
                    int index = 0;

                    // Шукаємо всі входження слова
                    while ((index = lowerOriginalText.IndexOf(lowerBannedWord, index)) != -1)
                    {
                        count++;
                        index += lowerBannedWord.Length;
                    }
                }
            }

            return count;
        }
    }
}

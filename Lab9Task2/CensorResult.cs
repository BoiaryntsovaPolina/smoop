using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9Task2
{
    internal class CensorResult
    {
        public string OriginalText { get; set; } = "";
        public string CensoredText { get; set; } = "";
        public int WordsCount { get; set; }
        public string[] BannedWords { get; set; } = new string[0];
        public DateTime ProcessDate { get; set; }

        public CensorResult(string originalText, string censoredText, int wordsCount, string[] bannedWords)
        {
            // Присвоюємо значення параметрів властивостям об'єкта
            OriginalText = originalText;
            CensoredText = censoredText;
            WordsCount = wordsCount;
            BannedWords = bannedWords;
            ProcessDate = DateTime.Now; // Дата обробки завжди встановлюється при створенні об'єкта
        }
    }
}

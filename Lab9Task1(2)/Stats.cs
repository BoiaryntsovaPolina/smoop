using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9Task1
{
    internal class Stats
    {
        public string File { get; set; }
        public int Sentences { get; set; }
        public int Upper { get; set; }
        public int Lower { get; set; }
        public int Vowels { get; set; }
        public int Consonants { get; set; }
        public int Digits { get; set; }

        public Stats() { }

        public Stats(string file, int sentences, int upper, int lower, int vowels, int consonants, int digits)
        {
            File = file;
            Sentences = sentences;
            Upper = upper;
            Lower = lower;
            Vowels = vowels;
            Consonants = consonants;
            Digits = digits;
        }

        public void Show()
        {
            Console.WriteLine($"\nФайл: {File}");
            Console.WriteLine($"Речення: {Sentences}");
            Console.WriteLine($"Великі літери: {Upper}");
            Console.WriteLine($"Маленькі літери: {Lower}");
            Console.WriteLine($"Голосні: {Vowels}");
            Console.WriteLine($"Приголосні: {Consonants}");
            Console.WriteLine($"Цифри: {Digits}");
        }
    }
}

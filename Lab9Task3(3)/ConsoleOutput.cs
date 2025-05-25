using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9Task3_3_
{
    internal class ConsoleOutput
    {
        public static void SetConsoleEncoding()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
        }

        public static void ShowMenu(int songCount)
        {
            Console.WriteLine("=== КОЛЕКЦІЯ ПІСЕНЬ ===");
            Console.WriteLine($"У колекції: {songCount} пісень");
            Console.WriteLine();
            Console.WriteLine("1. Додати пісню");
            Console.WriteLine("2. Видалити пісню");
            Console.WriteLine("3. Змінити інформацію про пісню");
            Console.WriteLine("4. Шукати пісню (за назвою/автором)");
            Console.WriteLine("5. Показати всі пісні");
            Console.WriteLine("6. Зберегти колекцію у файл");
            Console.WriteLine("7. Завантажити колекцію з файлу");
            Console.WriteLine("8. Знайти пісні за виконавцем");
            Console.WriteLine("9. Показати текст пісні");
            Console.WriteLine("0. Вихід");
            Console.WriteLine();
            Console.Write("Ваш вибір: ");
        }

        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void DisplaySong(Song song, int number = 0)
        {
            if (song == null)
            {
                DisplayMessage("Помилка: Спроба відобразити нульовий об'єкт пісні.");
                return;
            }

            if (number > 0)
                Console.WriteLine($"\n--- Пісня {number} ---");
            else
                Console.WriteLine("\n--- Пісня ---");

            Console.WriteLine($"Назва: {song.Title}");
            Console.WriteLine($"Автор: {song.Author}");
            Console.WriteLine($"Композитор: {song.Composer}");
            Console.WriteLine($"Рік: {song.Year}");
            Console.WriteLine($"Файл тексту: {song.LyricsFileName}");
            Console.Write("Виконавці: ");
            for (int i = 0; i < song.Performers.Length; i++)
            {
                Console.Write(song.Performers[i]);
                if (i < song.Performers.Length - 1)
                    Console.Write(", ");
            }
            Console.WriteLine();
        }

        public static void DisplaySongs(Song[] songs, string header)
        {
            if (songs == null || songs.Length == 0)
            {
                DisplayMessage("Пісень не знайдено!");
                return;
            }

            DisplayMessage($"\n=== {header} (Знайдено {songs.Length}) ===");
            for (int i = 0; i < songs.Length; i++)
            {
                DisplaySong(songs[i], i + 1);
            }
        }

        public static void WaitForAnyKey()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

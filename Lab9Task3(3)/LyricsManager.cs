using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9Task3_3_
{
    internal class LyricsManager
    {
        public static async Task DisplayLyricsFromFileAsync(string lyricsFileName)
        {
            try
            {
                if (File.Exists(lyricsFileName))
                {
                    string lyrics = await File.ReadAllTextAsync(lyricsFileName, Encoding.UTF8);
                    ConsoleOutput.DisplayMessage("\n=== ТЕКСТ ПІСНІ ===");
                    ConsoleOutput.DisplayMessage(lyrics);
                    ConsoleOutput.DisplayMessage("===================");
                }
                else
                {
                    ConsoleOutput.DisplayMessage("Файл з текстом пісні не знайдено!");
                }
            }
            catch (Exception ex)
            {
                ConsoleOutput.DisplayMessage($"Помилка при читанні файлу: {ex.Message}");
            }
        }

        public static async Task CreateSampleLyricsFileAsync(string fileName, string content)
        {
            try
            {
                await File.WriteAllTextAsync(fileName, content, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                ConsoleOutput.DisplayMessage($"Помилка при створенні файлу {fileName}: {ex.Message}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab9Task2
{
    internal class FileHandler
    {
        public string ReadTextFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл не знайдено: {filePath}");
            }

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer);
            }
        }

        public string[] ReadBannedWords(string filePath)
        {
            string content = ReadTextFromFile(filePath);
            string[] lines = content.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
            }

            return lines;
        }

        public async Task SaveResultToJsonAsync(CensorResult result, string fileName = "censor_result.json")
        {
            try
            {
                await using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {

                    await JsonSerializer.SerializeAsync(fs, result);
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"Помилка при асинхронному збереженні JSON у файл '{fileName}': {ex.Message}", ex);
            }
        }
    }
}

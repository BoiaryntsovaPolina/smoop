using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab9Task3_3_
{
    internal class SongCollection
    {
        private Song[] songs;
        private int count;

        public SongCollection()
        {
            songs = new Song[10];
            count = 0;
        }

        private void ResizeArray()
        {
            Song[] newSongs = new Song[songs.Length * 2];
            for (int i = 0; i < count; i++)
            {
                newSongs[i] = songs[i];
            }
            songs = newSongs;
        }

        public void AddSong(Song song)
        {
            if (count >= songs.Length)
            {
                ResizeArray();
            }
            songs[count] = song;
            count++;
        }

        public bool RemoveSong(string title)
        {
            for (int i = 0; i < count; i++)
            {
                if (songs[i].Title.ToLower() == title.ToLower())
                {
                    for (int j = i; j < count - 1; j++)
                    {
                        songs[j] = songs[j + 1];
                    }
                    songs[count - 1] = null;
                    count--;
                    return true;
                }
            }
            return false;
        }

        public Song FindSongByTitle(string title)
        {
            for (int i = 0; i < count; i++)
            {
                if (songs[i].Title.ToLower() == title.ToLower())
                {
                    return songs[i];
                }
            }
            return null;
        }

        public Song[] FindSongsByAuthor(string author)
        {
            Song[] result = new Song[count];
            int resultCount = 0;

            for (int i = 0; i < count; i++)
            {
                if (songs[i].Author.ToLower().Contains(author.ToLower()))
                {
                    result[resultCount] = songs[i];
                    resultCount++;
                }
            }

            Song[] finalResult = new Song[resultCount];
            for (int i = 0; i < resultCount; i++)
            {
                finalResult[i] = result[i];
            }

            return finalResult;
        }

        public Song[] FindSongsByPerformer(string performer)
        {
            Song[] result = new Song[count];
            int resultCount = 0;

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < songs[i].Performers.Length; j++)
                {
                    if (songs[i].Performers[j].ToLower().Contains(performer.ToLower()))
                    {
                        result[resultCount] = songs[i];
                        resultCount++;
                        break;
                    }
                }
            }

            Song[] finalResult = new Song[resultCount];
            for (int i = 0; i < resultCount; i++)
            {
                finalResult[i] = result[i];
            }

            return finalResult;
        }

        public Song[] GetAllSongs()
        {
            Song[] allSongs = new Song[count];
            for (int i = 0; i < count; i++)
            {
                allSongs[i] = songs[i];
            }
            return allSongs;
        }

        public async Task<bool> SaveToFileAsync(string fileName)
        {
            try
            {
                Song[] songsToSave = new Song[count];
                for (int i = 0; i < count; i++)
                {
                    songsToSave[i] = songs[i];
                }

                using (FileStream createStream = File.Create(fileName))
                {
                    await JsonSerializer.SerializeAsync(createStream, songsToSave, new JsonSerializerOptions { WriteIndented = true });
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving file: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> LoadFromFileAsync(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    using (FileStream openStream = File.OpenRead(fileName))
                    {
                        Song[] loadedSongs = await JsonSerializer.DeserializeAsync<Song[]>(openStream);

                        if (loadedSongs != null)
                        {
                            songs = new Song[loadedSongs.Length * 2];
                            count = 0;

                            for (int i = 0; i < loadedSongs.Length; i++)
                            {
                                songs[count] = loadedSongs[i];
                                count++;
                            }
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error loading file: {ex.Message}");
                return false;
            }
        }

        public bool EditSong(string title)
        {
            Song song = FindSongByTitle(title);
            if (song == null)
            {
                return false;
            }

            ConsoleOutput.DisplayMessage("Поточна інформація:");
            ConsoleOutput.DisplaySong(song);

            ConsoleOutput.DisplayMessage("\nВведіть нову інформацію (залиште порожнім для збереження поточного значення):");

            string newTitle = ConsoleInput.ReadStringWithDefault($"Нова назва", song.Title);
            song.Title = newTitle;

            string newAuthor = ConsoleInput.ReadStringWithDefault($"Новий автор", song.Author);
            song.Author = newAuthor;

            string newComposer = ConsoleInput.ReadStringWithDefault($"Новий композитор", song.Composer);
            song.Composer = newComposer;

            int newYear = ConsoleInput.ReadIntWithDefault($"Новий рік", song.Year);
            song.Year = newYear;

            return true;
        }

        public int Count => count;
    }
}

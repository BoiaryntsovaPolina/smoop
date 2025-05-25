using Lab9Task3_3_;

internal class Program
{
    static SongCollection collection = new SongCollection();
    const string CollectionFileName = "songs_collection.json";

    static async Task Main(string[] args)
    {
        ConsoleOutput.SetConsoleEncoding();
        await InitializeApplicationAsync();

        while (true)
        {
            ConsoleOutput.ShowMenu(collection.Count);
            string choice = ConsoleInput.ReadString("").Trim();

            switch (choice)
            {
                case "1":
                    AddNewSong();
                    break;
                case "2":
                    RemoveSongMenu();
                    break;
                case "3":
                    EditSongMenu();
                    break;
                case "4":
                    SearchMenu();
                    break;
                case "5":
                    DisplayAllSongsMenu();
                    break;
                case "6":
                    await SaveCollectionMenuAsync();
                    break;
                case "7":
                    await LoadCollectionMenuAsync();
                    break;
                case "8":
                    FindSongsByPerformerMenu();
                    break;
                case "9":
                    await DisplayLyricsMenuAsync();
                    break;
                case "0":
                    ConsoleOutput.DisplayMessage("До побачення!");
                    return;
                default:
                    ConsoleOutput.DisplayMessage("Невірний вибір! Спробуйте ще раз.");
                    break;
            }

            ConsoleOutput.WaitForAnyKey();
        }
    }

    // Cтворює файли з текстами пісень та завантажує/створює колекцію пісень.
    private static async Task InitializeApplicationAsync()
    {
 
        await LyricsManager.CreateSampleLyricsFileAsync("Monsters.txt", "");
        await LyricsManager.CreateSampleLyricsFileAsync("Arcade.txt", "");
        await LyricsManager.CreateSampleLyricsFileAsync("Human.txt", "");

        // Спроба завантажити колекцію пісень при запуску
        bool loadedSuccessfully = await collection.LoadFromFileAsync(CollectionFileName);
        if (loadedSuccessfully)
        {
            ConsoleOutput.DisplayMessage($"Завантажено {collection.Count} пісень з файлу {CollectionFileName}");
        }
        else
        {
            ConsoleOutput.DisplayMessage("Колекція не знайдена або порожня. Створено нову колекцію з прикладами.");
            // Додаємо приклади, якщо файл не знайдено або помилка під час завантаження
            collection.AddSong(new Song(
                "Monsters", "Timeflies", "Katie Sky", 2014,
                "Monsters.txt", new string[] { "Timeflies" }));
            collection.AddSong(new Song(
                "Arcade", "Duncan Laurence", "Wouter Hardy", 2020,
                "Arcade.txt", new string[] { "Duncan Laurence" }));
            collection.AddSong(new Song(
                "Human", "Rory Graham", "Jamie Hartman", 2017,
                "Human.txt", new string[] { "Rag'n'Bone Man" }));
            ConsoleOutput.DisplayMessage("Колекцію заповнено прикладами.");
            await collection.SaveToFileAsync(CollectionFileName); // Зберігаємо початкову колекцію
        }
    }

    static void AddNewSong()
    {
        ConsoleOutput.DisplayMessage("\n=== ДОДАТИ НОВУ ПІСНЮ ===");

        string title = ConsoleInput.ReadString("Назва пісні: ");
        string author = ConsoleInput.ReadString("Автор пісні: ");
        string composer = ConsoleInput.ReadString("Композитор: ");
        int year = ConsoleInput.ReadInt("Рік написання: ");
        string lyricsFileName = ConsoleInput.ReadString("Файл з текстом пісні (наприклад, monsters_lyrics.txt): ");

        int performerCount = ConsoleInput.ReadPositiveInt("Кількість виконавців: ");
        string[] performers = new string[performerCount];
        for (int i = 0; i < performerCount; i++)
        {
            performers[i] = ConsoleInput.ReadString($"Виконавець {i + 1}: ");
        }

        Song newSong = new Song(title, author, composer, year, lyricsFileName, performers);
        collection.AddSong(newSong);
        ConsoleOutput.DisplayMessage("Пісню успішно додано!");
    }

    static void RemoveSongMenu()
    {
        ConsoleOutput.DisplayMessage("\n=== ВИДАЛИТИ ПІСНЮ ===");
        string title = ConsoleInput.ReadString("Введіть назву пісні, яку потрібно видалити: ");
        if (collection.RemoveSong(title))
        {
            ConsoleOutput.DisplayMessage("Пісню видалено!");
        }
        else
        {
            ConsoleOutput.DisplayMessage("Пісню не знайдено!");
        }
    }

    static void EditSongMenu()
    {
        ConsoleOutput.DisplayMessage("\n=== РЕДАГУВАТИ ПІСНЮ ===");
        string title = ConsoleInput.ReadString("Введіть назву пісні, яку потрібно редагувати: ");
        if (collection.EditSong(title))
        {
            ConsoleOutput.DisplayMessage("Інформацію оновлено!");
        }
        else
        {
            ConsoleOutput.DisplayMessage("Пісню не знайдено!");
        }
    }

    static void SearchMenu()
    {
        ConsoleOutput.DisplayMessage("\n=== ПОШУК ПІСЕНЬ ===");
        ConsoleOutput.DisplayMessage("1. Пошук за назвою");
        ConsoleOutput.DisplayMessage("2. Пошук за автором");
        string choice = ConsoleInput.ReadString("Ваш вибір: ").Trim();

        switch (choice)
        {
            case "1":
                string title = ConsoleInput.ReadString("Введіть назву пісні: ");
                Song foundSong = collection.FindSongByTitle(title);
                if (foundSong != null)
                {
                    ConsoleOutput.DisplaySong(foundSong);
                }
                else
                {
                    ConsoleOutput.DisplayMessage("Пісню не знайдено!");
                }
                break;
            case "2":
                string author = ConsoleInput.ReadString("Введіть ім'я автора: ");
                Song[] foundSongsByAuthor = collection.FindSongsByAuthor(author);
                ConsoleOutput.DisplaySongs(foundSongsByAuthor, $"Знайдено пісень автора '{author}'");
                break;
            default:
                ConsoleOutput.DisplayMessage("Невірний варіант пошуку!");
                break;
        }
    }

    static void DisplayAllSongsMenu()
    {
        ConsoleOutput.DisplaySongs(collection.GetAllSongs(), "УСІ ПІСНІ У КОЛЕКЦІЇ");
    }

    static async Task SaveCollectionMenuAsync()
    {
        ConsoleOutput.DisplayMessage("\n=== ЗБЕРЕГТИ КОЛЕКЦІЮ ===");
        string fileName = ConsoleInput.ReadStringWithDefault("Введіть ім’я файлу (залиште порожнім для 'songs_collection.json')", CollectionFileName);
        if (await collection.SaveToFileAsync(fileName))
        {
            ConsoleOutput.DisplayMessage($"Колекцію збережено у файл {fileName}");
        }
        else
        {
            ConsoleOutput.DisplayMessage("Помилка під час збереження колекції.");
        }
    }

    static async Task LoadCollectionMenuAsync()
    {
        ConsoleOutput.DisplayMessage("\n=== ЗАВАНТАЖИТИ КОЛЕКЦІЮ ===");
        string fileName = ConsoleInput.ReadStringWithDefault("Введіть ім’я файлу (залиште порожнім для 'songs_collection.json')", CollectionFileName);
        if (await collection.LoadFromFileAsync(fileName))
        {
            ConsoleOutput.DisplayMessage($"Завантажено {collection.Count} пісень з файлу {fileName}");
        }
        else
        {
            ConsoleOutput.DisplayMessage("Файл не знайдено або сталася помилка під час завантаження.");
        }
    }

    static void FindSongsByPerformerMenu()
    {
        ConsoleOutput.DisplayMessage("\n=== ПОШУК ПІСЕНЬ ЗА ВИКОНАВЦЕМ ===");
        string performer = ConsoleInput.ReadString("Введіть ім’я виконавця: ");
        Song[] foundSongsByPerformer = collection.FindSongsByPerformer(performer);
        ConsoleOutput.DisplaySongs(foundSongsByPerformer, $"Знайдено пісень виконавця '{performer}'");
    }

    static async Task DisplayLyricsMenuAsync()
    {
        ConsoleOutput.DisplayMessage("\n=== ПОКАЗАТИ ТЕКСТ ПІСНІ ===");
        string title = ConsoleInput.ReadString("Введіть назву пісні, текст якої хочете переглянути: ");
        Song song = collection.FindSongByTitle(title);

        if (song != null)
        {
            await LyricsManager.DisplayLyricsFromFileAsync(song.LyricsFileName);
        }
        else
        {
            ConsoleOutput.DisplayMessage("Пісню не знайдено!");
        }
    }
}
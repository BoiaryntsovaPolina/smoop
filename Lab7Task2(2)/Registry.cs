namespace Lab7Task2_2_
{
    class Registry
    {
        private Device[] devices;
        private int count;

        public Registry()
        {
            devices = new Device[10];
            count = 0;
        }

        // Метод для додавання пристрою до реєстру
        public void AddDevice(Device device)
        {
            if (count == devices.Length)
            {
                // Якщо масив заповнено, збільшуємо його розмір вдвічі
                Device[] newDevices = new Device[devices.Length * 2];

                // Копіюємо елементи зі старого масиву в новий
                for (int i = 0; i < count; i++)
                {
                    newDevices[i] = devices[i];
                }

                // Замінюємо старий масив новим
                devices = newDevices;
            }
            // Додаємо новий пристрій
            devices[count] = device;
            count++;
        }

        // Метод для видалення пристрою за індексом
        public bool RemoveDevice(int index)
        {
            if (index < 0 || index >= count)
            {
                return false;
            }

            // Зсуваємо всі елементи після видаленого на одну позицію вліво
            for (int i = index; i < count - 1; i++)
            {
                devices[i] = devices[i + 1];
            }

            // Зменшуємо лічильник елементів і обнуляємо останній елемент
            count--;
            devices[count] = null;

            return true;
        }

        // Метод для виведення списку всього обладнання
        public void DisplayAllDevices()
        {
            Console.WriteLine("\n===== РЕЄСТР УСЬОГО ОБЛАДНАННЯ =====");

            if (count == 0)
            {
                Console.WriteLine("Реєстр порожній.");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\n--- Пристрій #{i + 1} ---");
                devices[i].DisplayInfo();
            }
        }

        // Метод для виведення електронного обладнання
        public void DisplayElectronicDevices()
        {
            Console.WriteLine("\n===== РЕЄСТР ЕЛЕКТРОННОГО ОБЛАДНАННЯ =====");

            bool found = false;

            for (int i = 0; i < count; i++)
            {
                if (devices[i].HasElectronics)
                {
                    Console.WriteLine($"\n--- Електронний пристрій #{i + 1} ---");
                    devices[i].DisplayInfo();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Електронного обладнання не знайдено.");
            }
        }

        // Метод для виведення устаткування без двигунів
        public void DisplayDevicesWithoutEngines()
        {
            Console.WriteLine("\n===== РЕЄСТР ОБЛАДНАННЯ БЕЗ ДВИГУНІВ =====");

            bool found = false;

            for (int i = 0; i < count; i++)
            {
                // Перевіряємо, чи не є пристрій таким, що має двигун
                if (!(devices[i] is IEngine))
                {
                    Console.WriteLine($"\n--- Пристрій без двигуна #{i + 1} ---");
                    devices[i].DisplayInfo();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Обладнання без двигунів не знайдено.");
            }
        }

        // Метод для сортування пристроїв за вагою (перший критерій) 
        public void SortByWeight()
        {
            if (count <= 1) return; // Нічого сортувати

            bool swapped;
            for (int i = 0; i < count - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < count - i - 1; j++)
                {
                    // Порівнюємо за вагою (за замовчуванням в CompareTo)
                    if (devices[j].CompareTo(devices[j + 1]) > 0)
                    {
                        // Міняємо місцями
                        Device temp = devices[j];
                        devices[j] = devices[j + 1];
                        devices[j + 1] = temp;
                        swapped = true;
                    }
                }

                // Якщо на поточній ітерації не було жодного обміну, то масив відсортований
                if (!swapped)
                    break;
            }

            Console.WriteLine("\nОбладнання відсортовано за вагою.");
        }

        // Метод для сортування пристроїв за роком виробництва (другий критерій) 
        public void SortByYear()
        {
            if (count <= 1) return; // Нічого сортувати

            bool swapped;
            for (int i = 0; i < count - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < count - i - 1; j++)
                {
                    // Порівнюємо за роком виробництва
                    if (devices[j].YearOfManufacture > devices[j + 1].YearOfManufacture)
                    {
                        // Міняємо місцями
                        Device temp = devices[j];
                        devices[j] = devices[j + 1];
                        devices[j + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped)
                    break;
            }

            Console.WriteLine("\nОбладнання відсортовано за роком виробництва.");
        }

        // Метод для клонування пристрою за індексом
        public Device CloneDevice(int index)
        {
            if (index < 0 || index >= count)
            {
                Console.WriteLine("Помилка: неправильний індекс для клонування.");
                return null;
            }

            // Використовуємо метод Clone з інтерфейсу ICloneable
            return (Device)devices[index].Clone();
        }
    }
}

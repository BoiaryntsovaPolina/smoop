using Lab7.Classes;
using Lab7.Interfaces;
using Lab7.Utilits;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab7
{
    // Клас-контейнер для зберігання та операцій з магазинами
    internal class ShopContainer : IFileContainer<Shop>, IEnumerable<Shop>
    {
        private Shop[] shops;
        private int count;
        private bool isDataSaved;

        // Конструктор з вказанням початкової ємності контейнера
        public ShopContainer(int capacity = 10)
        {
            shops = new Shop[capacity];
            count = 0;
            isDataSaved = false;
        }

        // Кількість елементів у контейнері
        public int Count
        {
            get { return count; }
        }

        // Індексатор для доступу до елементів контейнера
        public Shop this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Індекс за межами діапазону");
                return shops[index];
            }
            set
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Індекс за межами діапазону");
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Значення не може бути null");

                shops[index] = value;
                isDataSaved = false;
            }
        }

        // Додає новий елемент до контейнера
        public void Add(Shop element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element), "Елемент не може бути null");

            // Перевірка, чи потрібно розширити масив
            if (count == shops.Length)
            {
                Array.Resize(ref shops, shops.Length * 2);
            }

            // Додавання магазину
            shops[count] = element;
            count++;
            isDataSaved = false;
        }

        // Видаляє вказаний елемент з контейнера
        public void Delete(Shop element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element), "Елемент не може бути null");

            int index = -1;

            // Пошук індексу елемента
            for (int i = 0; i < count; i++)
            {
                if (shops[i].Equals(element))
                {
                    index = i;
                    break;
                }
            }

            // Видалення елемента, якщо знайдено
            if (index != -1)
            {
                for (int i = index; i < count - 1; i++)
                {
                    shops[i] = shops[i + 1];
                }
                shops[count - 1] = null;
                count--;
                isDataSaved = false;
            }
        }

        // Показує, чи були дані збережені у файл        
        public bool IsDataSaved
        {
            get { return isDataSaved; }
        }

        // Зберігає дані контейнера у текстовий файл
        public void Save(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    writer.WriteLine($"Кількість магазинів: {count}");

                    for (int i = 0; i < count; i++)
                    {
                        writer.WriteLine($"--- Магазин {i + 1} ---");
                        writer.WriteLine(shops[i].ToShortString());

                        writer.WriteLine("Товари:");

                        Product[] products = shops[i].Products;

                        if (products == null || products.Length == 0)
                        {
                            writer.WriteLine("  Немає товарів");
                        }
                        else
                        {
                            for (int j = 0; j < products.Length; j++)
                            {
                                if (products[j] != null)
                                {
                                    writer.WriteLine($"  {products[j].ToString()}");
                                }
                            }
                        }
                    }
                }
                isDataSaved = true;
                Console.WriteLine($"Дані успішно збережено у файл {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при збереженні даних: {ex.Message}");
                throw;
            }
        }

        public Shop[] Load(string filename)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string line;

                    // Читаємо першу строку для визначення кількості магазинів
                    line = reader.ReadLine();
                    if (line == null || !line.StartsWith("Кількість магазинів:"))
                    {
                        throw new FormatException("Неправильний формат файлу. Очікувалось 'Кількість магазинів:'");
                    }

                    // Парсинг кількості магазинів
                    int totalShops = int.Parse(line.Split(':')[1].Trim());
                    Shop[] loadedShops = new Shop[totalShops];

                    int currentShopIndex = -1;
                    Shop currentShop = null;
                    bool processingProducts = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();

                        // Перевірка нового магазину
                        if (line.StartsWith("--- Магазин"))
                        {
                            currentShopIndex++;
                            processingProducts = false;

                            // Перевірка на вихід за межі масиву
                            if (currentShopIndex >= totalShops)
                            {
                                Console.WriteLine($"Попередження: у файлі більше магазинів, ніж вказано ({totalShops}). Зайві будуть проігноровані.");
                                break;
                            }
                            continue;
                        }

                        // Якщо ще не почали обробку магазину, пропускаємо рядок
                        if (currentShopIndex < 0)
                            continue;

                        // Читаємо дані про магазин
                        if (line.StartsWith("Магазин:"))
                        {
                            // Парсинг даних магазину
                            string[] mainParts = line.Split(',');

                            if (mainParts.Length < 4)
                            {
                                Console.WriteLine($"Попередження: рядок з даними магазину має неправильний формат: {line}");
                                continue;
                            }

                            // Парсинг назви
                            string namePart = mainParts[0].Trim();
                            string name = namePart.Substring(namePart.IndexOf(':') + 1).Trim();

                            // Парсинг категорії
                            string categoryPart = mainParts[1].Trim();
                            string categoryStr = categoryPart.Substring(categoryPart.IndexOf(':') + 1).Trim();
                            ProductCategory category;
                            if (!Enum.TryParse(categoryStr, out category))
                            {
                                Console.WriteLine($"Попередження: неможливо перетворити '{categoryStr}' у тип ProductCategory. Використано значення за замовчуванням.");
                                category = ProductCategory.OfficeEquipment;
                            }

                            // Парсинг дати
                            string datePart = mainParts[2].Trim();
                            string dateStr = datePart.Substring(datePart.IndexOf(':') + 1).Trim();
                            DateTime openDate;
                            if (!DateTime.TryParse(dateStr, out openDate))
                            {
                                Console.WriteLine($"Попередження: неможливо перетворити '{dateStr}' у дату. Використано поточну дату.");
                                openDate = DateTime.Now;
                            }

                            // Створюємо новий магазин
                            currentShop = new Shop(name, category, openDate);
                            loadedShops[currentShopIndex] = currentShop;
                            continue;
                        }

                        // Обробка списку товарів
                        if (line == "Товари:")
                        {
                            processingProducts = true;
                            continue;
                        }

                        // Обробка товарів, якщо ми в режимі обробки товарів і маємо поточний магазин
                        if (processingProducts && currentShop != null && !line.StartsWith("--- Магазин"))
                        {
                            // Пропускаємо рядок "Немає товарів"
                            if (line.Contains("Немає товарів"))
                                continue;

                            // Видаляємо початкові пробіли та табуляції (зокрема, "  " на початку рядка)
                            if (line.StartsWith("  "))
                                line = line.Substring(2);

                            // Пропускаємо порожні рядки
                            if (string.IsNullOrWhiteSpace(line))
                                continue;

                            // Парсинг даних товару
                            try
                            {
                                // Розбиваємо рядок на частини за комами, але враховуємо, що коми можуть бути частиною дати
                                string[] productData = line.Split(new string[] { ", " }, StringSplitOptions.None);

                                if (productData.Length < 4)
                                {
                                    Console.WriteLine($"Попередження: рядок з даними товару має неправильний формат: {line}");
                                    continue;
                                }

                                // Парсинг даних про особу
                                string personPart = productData[0];
                                string personFullName = personPart.Substring(personPart.IndexOf(':') + 1).Trim();
                                string[] nameParts = personFullName.Split(' ');

                                // Перевірка правильності формату імені
                                if (nameParts.Length < 2)
                                {
                                    Console.WriteLine($"Попередження: неправильний формат імені постачальника: {personFullName}");
                                    continue;
                                }

                                string firstName = nameParts[0];
                                string lastName = nameParts[1];

                                // Парсинг дати народження
                                string birthPart = productData[1];
                                string birthDateStr = birthPart.Substring(birthPart.IndexOf(':') + 1).Trim();
                                DateTime birthDate;
                                if (!DateTime.TryParse(birthDateStr, out birthDate))
                                {
                                    Console.WriteLine($"Попередження: неможливо перетворити '{birthDateStr}' у дату. Використано значення за замовчуванням.");
                                    birthDate = new DateTime(1990, 1, 1);
                                }

                                // Парсинг назви товару
                                string productNamePart = productData[2];
                                string productName = productNamePart.Substring(productNamePart.IndexOf(':') + 1).Trim();

                                // Парсинг ціни
                                string pricePart = productData[3];
                                string priceStr = pricePart.Substring(pricePart.IndexOf(':') + 1).Replace("грн", "").Trim();
                                decimal price;
                                if (!decimal.TryParse(priceStr, out price))
                                {
                                    Console.WriteLine($"Попередження: неможливо перетворити '{priceStr}' у ціну. Використано значення 0.");
                                    price = 0;
                                }

                                // Створюємо об'єкти і додаємо товар до магазину
                                Person supplier = new Person(firstName, lastName, birthDate);
                                Product product = new Product(supplier, productName, price);
                                currentShop.AddProducts(product);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Помилка при обробці товару: {ex.Message} - {line}");
                                // Продовжуємо обробку наступних товарів
                            }
                        }
                    }

                    // Перевірка, чи всі магазини були заповнені
                    for (int i = 0; i < loadedShops.Length; i++)
                    {
                        if (loadedShops[i] == null)
                        {
                            Console.WriteLine($"Попередження: магазин з індексом {i} відсутній у файлі. Створено порожній магазин.");
                            loadedShops[i] = new Shop($"Магазин {i+1}", ProductCategory.OfficeEquipment, DateTime.Now);
                        }
                    }

                    // Оновлюємо внутрішній стан контейнера
                    shops = new Shop[totalShops];
                    Array.Copy(loadedShops, shops, totalShops);
                    count = totalShops;
                    isDataSaved = true;

                    return loadedShops;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при завантаженні даних з файлу: {ex.Message}");
                throw;
            }
        }

        // Створює новий контейнер з перших n елементів
        public ShopContainer GetFirstN(int n)
        {
            if (n <= 0)
                throw new ArgumentException("Кількість елементів повинна бути більше 0");

            ShopContainer result = new ShopContainer(n);
            int elementsToTake = Math.Min(n, count);

            for (int i = 0; i < elementsToTake; i++)
            {
                result.Add(shops[i]);
            }

            return result;
        }

        // Сортує магазини за їх загальною вартістю         
        public void SortByTotalValue()
        {
            Array.Sort(shops, 0, count); // Використовується IComparable<Shop>
            isDataSaved = false;
        }

        public IEnumerator<Shop> GetEnumerator()
        {
            return new ShopEnumerator(shops, count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
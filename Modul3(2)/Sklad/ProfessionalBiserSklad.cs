using ConsoleApp2.BaseClasses;
using ConsoleApp2.Enums;
using ConsoleApp2.Interfaces;
using ConsoleApp2.ConcreteBiser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Sklad
{
    public class ProfessionalBiserSklad : ISklad
    {
        private IBiser[] biserStorage;
        private int currentBiserCount; // Фактична кількість бісеру на складі
        private const int InitialCapacity = 50; // Початкова ємність складу
        public double TotalSpentCost { get; private set; } // Загальні витрати на докупівлю бісеру

        private static readonly Random _random = new Random(); // Для рандомного розміру в BuyBiserPackage

        public ProfessionalBiserSklad()
        {
            biserStorage = new IBiser[InitialCapacity];
            currentBiserCount = 0;
            TotalSpentCost = 0;
        }

        // Додає намистину на склад. При необхідності розширює масив.
        public void AddBiser(IBiser biser)
        {
            if (biser == null) return;

            if (currentBiserCount >= biserStorage.Length)
            {
                // Розширюємо масив вдвічі
                IBiser[] newStorage = new IBiser[biserStorage.Length * 2];
                Array.Copy(biserStorage, newStorage, biserStorage.Length);
                biserStorage = newStorage;
            }
            biserStorage[currentBiserCount++] = biser;

            Console.WriteLine($"Додано бісер: {biser.Color}, {biser.Size:F1}мм, Якість:{biser.Quality}");
        }

        // Відбирає певну кількість намистин за заданими критеріями.
        public IBiser[] VidibratyBiser(int kilkist, BiserColor requiredColor, int minQuality, double minSize) 
        {
            if (kilkist <= 0) return new IBiser[0];

            IBiser[] tempSelectedBiserArray = new IBiser[kilkist]; // Масив для відібраних намистин
            int selectedCount = 0; // Кількість фактично відібраних намистин

            // Масив для тимчасового зберігання індексів намистин, які потрібно видалити
            int[] indexesToRemove = new int[currentBiserCount];
            int removeCount = 0;

            // Збираємо індекси бісеру, який відповідає критеріям
            for (int i = 0; i < currentBiserCount; i++)
            {
                if (biserStorage[i] != null &&
                    biserStorage[i].Color == requiredColor &&
                    biserStorage[i].Quality >= minQuality &&
                    biserStorage[i].Size >= minSize)
                {
                    if (selectedCount < kilkist)
                    {
                        tempSelectedBiserArray[selectedCount++] = biserStorage[i];
                        indexesToRemove[removeCount++] = i;
                    }
                }
            }

            if (selectedCount < kilkist)
            {
                Console.WriteLine($"Недостатньо бісеру для відбору за критеріями: {requiredColor}, Q>={minQuality}, S>={minSize}мм. Потрібно: {kilkist}, Знайдено: {selectedCount}");
                return new IBiser[0]; // Повертаємо порожній масив, якщо недостатньо
            }

            // Видаляємо відібрані елементи зі складу
            for (int i = 0; i < removeCount; i++)
            {
                biserStorage[indexesToRemove[i]] = null;
            }

            CompactBiserStorage(); // Ущільнюємо масив після видалення

            Console.WriteLine($"Відібрано {selectedCount} бісеру: Колір: {requiredColor}, Якість>={minQuality}, Розмір>={minSize}мм");

            // Якщо було відібрано рівно 'kilkist' намистин, повертаємо tempSelectedBiserArray
            IBiser[] finalSelected = new IBiser[selectedCount];
            Array.Copy(tempSelectedBiserArray, finalSelected, selectedCount);
            return finalSelected;
        }

        // Ущільнює масив `biserStorage`, переміщуючи всі не-null елементи на початок.
        private void CompactBiserStorage()
        {
            int writeIndex = 0;
            for (int readIndex = 0; readIndex < currentBiserCount; readIndex++)
            {
                if (biserStorage[readIndex] != null)
                {
                    biserStorage[writeIndex++] = biserStorage[readIndex];
                }
            }
            // Заповнюємо залишок null-ами
            for (int i = writeIndex; i < currentBiserCount; i++)
            {
                biserStorage[i] = null;
            }
            currentBiserCount = writeIndex; // Оновлюємо фактичну кількість
        }

        // Перевіряє, чи потрібна докупівля бісеру, виходячи із загальної кількості.
        public bool ChyPotribnaDocupivlya(int minTotal)
        {
            int totalCount = GetTotalBiserCount();
            Console.WriteLine($"    Загальна кількість бісеру на складі: {totalCount}. Мінімальна потрібна: {minTotal}");
            if (totalCount < minTotal)
            {
                Console.WriteLine($"Потрібна докупівля бісеру! Загальна кількість нижча за мінімальну.");
                return true;
            }
            Console.WriteLine($"Докупіля бісеру не потрібна.");
            return false;
        }

        // Повертає загальну кількість бісеру на складі.
        public int GetTotalBiserCount()
        {
            return currentBiserCount;
        }

        // Купує пакетик бісеру і додає його вміст на склад.
        public void BuyBiserPackage(BiserColor color, int quality, double bagPrice, int beadsInBag)
        {
            Console.WriteLine($"\nКупуємо пакет бісеру: {color}, Якість:{quality}/10, {beadsInBag} шт. за {bagPrice:C}");
            TotalSpentCost += bagPrice; // Додаємо до загальних витрат

            for (int i = 0; i < beadsInBag; i++)
            {
                // Створюємо бісер без індивідуальної ціни. Розмір може бути випадковим, імітуючи варіативність в пакеті.
                AddBiser(new Biser(color, (double)_random.Next(20, 41) / 10, quality)); // Розмір від 2.0 до 4.0
            }
            Console.WriteLine($"Додано {beadsInBag} намистин на склад. Загальні витрати: {TotalSpentCost:C}");
        }

        // Спроба створити виріб, відбираючи необхідний бісер зі складу.
        // Вартість бісеру, що вже є, не додається до вартості виробу.
        public IBiser[] SprobuvatiStvorityVirob(Virob virob)
        {
            Console.WriteLine($"\nСпроба створити виріб: '{virob.Name}'...");
            BiserRequirement[] requirements = virob.GetRequirements();

            // Загальний масив для збору ВСЬОГО бісеру, який потрібен для виробу
            int maxTotalRequired = 0;
            for (int i = 0; i < requirements.Length; i++)
            {
                maxTotalRequired += requirements[i].Quantity;
            }
            IBiser[] allCollectedBiser = new IBiser[maxTotalRequired];
            int currentCollectedIndex = 0;

            for (int i = 0; i < requirements.Length; i++)
            {
                BiserRequirement req = requirements[i];
                IBiser[] selected = VidibratyBiser(req.Quantity, req.Color, req.MinQuality, req.MinSize);

                if (selected.Length < req.Quantity)
                {
                    Console.WriteLine("Не вистачає бісеру для '{virob.Name}'. Відміна операції.");
                    // Повертаємо бісер, який встигли відібрати
                    for (int k = 0; k < currentCollectedIndex; k++)
                    {
                        AddBiser(allCollectedBiser[k]); // Повертаємо назад
                    }
                    return null;
                }

                // Додаємо відібраний бісер до загального масиву
                for (int k = 0; k < selected.Length; k++)
                {
                    allCollectedBiser[currentCollectedIndex++] = selected[k];
                }
            }

            // Якщо все успішно, повідомляємо виробу про його створення
            virob.OnCreated();

            // Створюємо фінальний масив тільки з використаним бісером
            IBiser[] finalUsedBiser = new IBiser[currentCollectedIndex];
            Array.Copy(allCollectedBiser, finalUsedBiser, currentCollectedIndex);

            return finalUsedBiser;
        }

        // Виводить детальний стан складу, включаючи кількість бісеру за кольорами та якістю.
        public void VyvestyDetalnyyStanSkladu()
        {
            Console.WriteLine("\nСТАН СКЛАДУ БІСЕРУ:");
            Console.WriteLine($"    Загальна кількість намистин: {currentBiserCount}");
            Console.WriteLine($"    Загальні витрати на докупівлю бісеру: {TotalSpentCost:C}"); // Виводимо загальні витрати

            // Масиви для підрахунку кількості бісеру за кольорами та якістю
            int[] colorCounts = new int[Enum.GetValues(typeof(BiserColor)).Length];
            int[] qualityCounts = new int[11]; // Для якості від 1 до 10 (індекс 0 не використовується)

            for (int i = 0; i < currentBiserCount; i++)
            {
                if (biserStorage[i] != null)
                {
                    colorCounts[(int)biserStorage[i].Color]++;
                    qualityCounts[biserStorage[i].Quality]++;
                }
            }

            Console.WriteLine("    Кількість бісеру за кольорами:");
            for (int i = 0; i < colorCounts.Length; i++)
            {
                if (colorCounts[i] > 0)
                {
                    Console.WriteLine($"      {((BiserColor)i).ToString()}: {colorCounts[i]} шт.");
                }
            }
            Console.WriteLine("    Кількість бісеру за якістю:");
            for (int i = 1; i < qualityCounts.Length; i++) // Починаємо з 1
            {
                if (qualityCounts[i] > 0)
                {
                    Console.WriteLine($"      Якість {i}: {qualityCounts[i]} шт.");
                }
            }
            Console.WriteLine("----------------------------------");
        }

        // Реалізація IEnumerable<IBiser> для ProfessionalBiserSklad.
        public IEnumerator<IBiser> GetEnumerator()
        {
            return new SkladEnumerator(biserStorage, currentBiserCount);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private class SkladEnumerator : IEnumerator<IBiser>
        {
            private IBiser[] _biserStorage;
            private int _currentBiserCount;
            private int _position = -1; // -1, щоб перший MoveNext перевів на 0

            public SkladEnumerator(IBiser[] storage, int count)
            {
                _biserStorage = storage;
                _currentBiserCount = count;
            }

            public IBiser Current
            {
                get
                {
                    if (_position < 0 || _position >= _currentBiserCount)
                    {
                        throw new InvalidOperationException();
                    }
                    return _biserStorage[_position];
                }
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _position++;
                // Пропускаємо null елементи, які могли залишитися після видалення
                while (_position < _currentBiserCount && _biserStorage[_position] == null)
                {
                    _position++;
                }
                return _position < _currentBiserCount;
            }

            public void Reset()
            {
                _position = -1;
            }

            public void Dispose()
            {
                // Немає керованих ресурсів для звільнення
            }
        }
    }
}

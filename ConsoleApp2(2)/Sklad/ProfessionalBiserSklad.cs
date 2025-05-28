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
        private IBiser[] PerformBiserSelection(int kilkist, BiserColor requiredColor, int minQuality, double minSize) 
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
                // Виклик SprobuvatiStvorityVirob вирішить, що робити далі.
                return new IBiser[0]; // Повертаємо порожній масив, якщо недостатньо
            }

            // Видаляємо відібрані елементи зі складу
            for (int i = 0; i < removeCount; i++)
            {
                biserStorage[indexesToRemove[i]] = null;
            }

            CompactBiserStorage(); // Ущільнюємо масив після видалення

            return tempSelectedBiserArray; // Повертаємо відібраний бісер
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
            Console.WriteLine($"\nКупуємо пакет бісеру: {color}, Якість:{quality}/10, {beadsInBag} шт. за {bagPrice:F2}грн.");
            TotalSpentCost += bagPrice; // Додаємо до загальних витрат

            for (int i = 0; i < beadsInBag; i++)
            {
                // Створюємо бісер без індивідуальної ціни. Розмір може бути випадковим, імітуючи варіативність в пакеті.
                AddBiser(new Biser(color, (double)_random.Next(20, 41) / 10, quality)); // Розмір від 2.0 до 4.0
            }
            Console.WriteLine($"Додано {beadsInBag} намистин на склад. Загальні витрати: {TotalSpentCost:F2}грн.");
        }

        // Спроба створити виріб, відбираючи необхідний бісер зі складу.
        // Вартість бісеру, що вже є, не додається до вартості виробу.
        public BiserRequirement[] SprobuvatiStvorityVirob(Virob virob, out IBiser[] usedBiser)
        {
            Console.WriteLine($"\nСпроба створити виріб: '{virob.Name}'...");
            BiserRequirement[] requirements = virob.GetRequirements();

            // Масив для збору ВСЬОГО бісеру, який був би відібраний для виробу.
            // Якщо щось піде не так, ми повернемо цей бісер на склад.
            int maxTotalRequired = 0;
            for (int i = 0; i < requirements.Length; i++)
            {
                maxTotalRequired += requirements[i].Quantity;
            }
            IBiser[] allCollectedBiser = new IBiser[maxTotalRequired];
            int currentCollectedIndex = 0; // Фактична кількість зібраного бісеру

            // Масив для збереження вимог до бісеру, якого не вистачило
            BiserRequirement[] missingRequirements = new BiserRequirement[requirements.Length];
            int missingCount = 0;

            // Спершу перевіряємо, скільки бісеру є для кожної вимоги
            for (int i = 0; i < requirements.Length; i++)
            {
                BiserRequirement req = requirements[i];
                int foundCount = 0;
                for (int j = 0; j < currentBiserCount; j++)
                {
                    if (biserStorage[j] != null &&
                        biserStorage[j].Color == req.Color &&
                        biserStorage[j].Quality >= req.MinQuality &&
                        biserStorage[j].Size >= req.MinSize)
                    {
                        foundCount++;
                    }
                }
                req.AvailableQuantity = foundCount; // Оновлюємо доступну кількість
            }


            // Тепер перевіряємо, чого не вистачає
            bool canCreate = true;
            for (int i = 0; i < requirements.Length; i++)
            {
                BiserRequirement req = requirements[i];
                if (req.AvailableQuantity < req.Quantity)
                {
                    canCreate = false;
                    // Додаємо копію вимоги до списку відсутніх
                    missingRequirements[missingCount++] = new BiserRequirement(req);
                    Console.WriteLine($"Не вистачає: Колір {req.Color}, Розмір >={req.MinSize:F1}мм, Якість >={req.MinQuality}, Потрібно: {req.Quantity}, В наявності: {req.AvailableQuantity}.");
                }
            }

            if (!canCreate)
            {
                Console.WriteLine($"Неможливо створити '{virob.Name}' зараз. Потрібна докупівля.");
                usedBiser = null;
                // Повертаємо тільки ті вимоги, де була нестача
                BiserRequirement[] finalMissing = new BiserRequirement[missingCount];
                Array.Copy(missingRequirements, finalMissing, missingCount);
                return finalMissing;
            }

            // Якщо ми дійшли сюди, значить бісеру вистачає.
            // Тепер реально відбираємо бісер.
            for (int i = 0; i < requirements.Length; i++)
            {
                BiserRequirement req = requirements[i];
                IBiser[] selected = PerformBiserSelection(req.Quantity, req.Color, req.MinQuality, req.MinSize);
                for (int k = 0; k < selected.Length; k++)
                {
                    allCollectedBiser[currentCollectedIndex++] = selected[k];
                }
            }

            virob.OnCreated();
            Console.WriteLine($"\nВиріб '{virob.Name}' успішно завершено!");

            usedBiser = new IBiser[currentCollectedIndex];
            Array.Copy(allCollectedBiser, usedBiser, currentCollectedIndex);
            return new BiserRequirement[0];
        }


        // Виводить детальний стан складу, включаючи кількість бісеру за кольорами та якістю.
        public void VyvestyDetalnyyStanSkladu()
        {
            Console.WriteLine("\nСТАН СКЛАДУ БІСЕРУ:");
            Console.WriteLine($"    Загальна кількість намистин: {currentBiserCount}");
            Console.WriteLine($"    Загальні витрати на докупівлю бісеру: {TotalSpentCost:F2}грн."); // Виводимо загальні витрати

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

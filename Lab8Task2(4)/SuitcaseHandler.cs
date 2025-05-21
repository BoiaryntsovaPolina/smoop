using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8Task2
{
    internal class SuitcaseHandler
    {
        // Метод, який буде викликатися при додаванні предмету до валізи
        public void OnThingAdded(object sender, ThingEventArgs e)
        {
            Suitcase suitcase = sender as Suitcase;
            Thing thing = e.Thing;

            if (suitcase != null && thing != null)
            {
                Console.WriteLine("ПОДІЯ: До валізи додано предмет '{0}'", thing.Name);
                Console.WriteLine("       Зайнятий об'єм: {0:F2} л з {1:F2} л",
                                 suitcase.CurrentVolume, suitcase.MaxVolume);
                Console.WriteLine("       Вільно об'єму: {0:F2} л",
                                 suitcase.MaxVolume - suitcase.CurrentVolume);
                Console.WriteLine("       Загальна вага: {0:F2} кг", suitcase.TotalWeight);
                
                // Додаємо інформацію про важливість
                if (thing.IsEssential)
                {
                    Console.WriteLine("       Це обов'язковий предмет!");
                }
                Console.WriteLine("       Важливість: {0}/10", thing.Importance);
            }
        }
    }
}

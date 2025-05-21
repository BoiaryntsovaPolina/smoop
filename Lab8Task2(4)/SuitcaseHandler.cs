using System;

namespace Lab8Task2
{
    internal class SuitcaseListener
    {
        // Метод, який буде викликатися при додаванні предмету до валізи
        public void OnThingAdded(Suitcase sender, Thing addedThing)
        {
            if (sender != null && addedThing != null)
            {
                Console.WriteLine("ПОДІЯ: До валізи додано предмет '{0}'", addedThing.Name);
                Console.WriteLine("       Зайнятий об'єм: {0:F2} л з {1:F2} л",
                                  sender.CurrentVolume, sender.MaxVolume);
                Console.WriteLine("       Вільно об'єму: {0:F2} л",
                                  sender.MaxVolume - sender.CurrentVolume);
                Console.WriteLine("       Загальна вага: {0:F2} кг", sender.TotalWeight);

                // інформація про важливість
                if (addedThing.IsEssential)
                {
                    Console.WriteLine("       Це обов'язковий предмет!");
                }
                Console.WriteLine("       Важливість: {0}/10", addedThing.Importance);
            }
        }
    }
}
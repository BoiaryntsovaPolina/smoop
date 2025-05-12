using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8Task1_2_
{
    internal class TimeDisplayer
    {
        public void DisplayTimeInfo(Action timeAction)
        {
            timeAction();
        }

        // Метод для відображення поточного часу
        public void ShowCurrentTime()
        {
            Console.WriteLine("Поточний час: " + DateTime.Now.ToLongTimeString());
        }

        // Метод для відображення поточної дати
        public void ShowCurrentDate()
        {
            Console.WriteLine("Поточна дата: " + DateTime.Now.ToLongDateString());
        }

        // Метод для відображення поточного дня тижня
        public void ShowCurrentDayOfWeek()
        {
            Console.WriteLine("Поточний день тижня: " + DateTime.Now.DayOfWeek);
        }
    }
}

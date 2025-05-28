using ConsoleApp2.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Interfaces
{
    public interface ISklad : IEnumerable<IBiser>
    {
        // Додає намистину на склад.
        void AddBiser(IBiser biser);

        // Перевіряє, чи потрібна докупівля бісеру, виходячи із загальної кількості.
        bool ChyPotribnaDocupivlya(int minTotal);

        // Повертає загальну кількість бісеру на складі.
        int GetTotalBiserCount();

        // Купує пакетик бісеру і додає його вміст на склад.
        void BuyBiserPackage(BiserColor color, int quality, double bagPrice, int beadsInBag);

        // Виводить детальний стан складу.
        void VyvestyDetalnyyStanSkladu();
    }
}

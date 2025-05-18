using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.Interfaces
{
    public interface IContainer<T>
    {
        int Count { get; }

        T this[int index] { get; set; }

        // Метод для додавання елемента в контейнер
        void Add(T element);

        // Метод для видалення елемента з контейнера
        void Delete(T element);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.Interfaces
{
    public interface IFileContainer<T> : IContainer<T>
    {
        // Зберегти вміст контейнера у текстовий файл
        void Save(string fileName);

        // Завантажити дані з текстового файлу до контейнера
        T[] Load(string path);

        // Повертає true, якщо дані контейнеру були збережені у файл.
        bool IsDataSaved { get; }
    }
}

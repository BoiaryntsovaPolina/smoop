using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7Task3
{
    internal interface ICipher
    {
        string encode(string text); // Метод для шифрування рядка
        string decode (string text); // дешифрування
    }
}

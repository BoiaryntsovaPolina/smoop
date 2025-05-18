using Lab7.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    internal class ShopEnumerator : IEnumerator<Shop>
    {
        private Shop[] shops;
        private int count;
        private int position = -1; // починаємо до першого елемента

        public ShopEnumerator(Shop[] shops, int count)
        {
            this.shops = shops;
            this.count = count;
        }

        public Shop Current
        {
            get
            {
                if (position < 0 || position >= count)
                    throw new InvalidOperationException();
                return shops[position];
            }
        }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (position < count - 1)
            {
                position++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
            // якщо немає ресурсів — можна залишити пустим
        }
    }

}

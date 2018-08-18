using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console4
{
    public class Stack2<T>
    {
        private T[] item;
        int number;
        public Stack2(int capacity)
        {
            item = new T[capacity];
        }
        public void Push(T item)
        {

        }
        private void Resize(int capacity)
        {
            T[] temp = new T[capacity];
            for (int i = 0; i < item.Length; i++)
            {
                temp[i] = item[i];
            }
            item = temp;
        }
    }
}

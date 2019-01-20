using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace Common
{
    public class StackArray<T>
    {
        T[] items;
        int number = 0;
        public StackArray(int capacity)
        {
            items = new T[capacity];
        }
        public void Push(T data)
        {
            if (number == items.Length) Resize(2 * items.Length);
            items[number++] = data;
        }
        public T Pop()
        {
            T temp = items[--number];
            items[number] = default(T);
            if (number > 0 && number == items.Length / 4) Resize(items.Length / 2);
            return temp;
        }
        private void Resize(int capacity)
        {
            T[] temp = new T[capacity];
            for (int i = 0; i < items.Length; i++)
            {
                temp[i] = items[i];
            }
            items = temp;
        }
    }
}

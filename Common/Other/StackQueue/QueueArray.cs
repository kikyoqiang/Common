using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class QueueArray<T>
    {
        T[] items;
        int head;
        int tail;
        public QueueArray(int capacity)
        {
            items = new T[capacity];
        }
        public void Enqueue(T data)
        {
            if ((head - tail + 1) == items.Length)
                Resize(items.Length * 2);
            items[tail++] = data;
        }
        public T Dequque()
        {
            T temp = items[--head];
            items[head] = default(T);
            if (head > 0 && (tail - head + 1) == items.Length / 4)
                Resize(items.Length / 2);
            return temp;
        }
        private void Resize(int capacity)
        {
            T[] temp = new T[capacity];
            int index = 0;
            for (int i = head; i < tail; i++)
            {
                temp[++index] = items[i];
            }
            items = temp;
        }
    }
}

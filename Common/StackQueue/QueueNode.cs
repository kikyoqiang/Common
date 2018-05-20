using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class QueueNode<T>
    {
        class Node
        {
            public T item { get; set; }
            public Node Next { get; set; }
        }
        private bool IsEmpty() { return number == 0; }
        private Node first = null;
        private Node last = null;
        private int number = 0;
        public void Enqueue(T data)
        {
            Node oldLast = last;
            last = new Node();
            last.item = data;
            if (IsEmpty())
                first = last;
            else
                oldLast.Next = last;
            number++;
        }
        public T Dequeue()
        {
            T temp = first.item;
            first = first.Next;
            number--;
            if (IsEmpty())
                last = null;
            return temp;
        }
    }
}

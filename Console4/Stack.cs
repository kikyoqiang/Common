using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console4
{
    public class Stack<T>
    {
        private class Node
        {
            public T Item { get; set; }
            public Node Next { get; set; }
        }
        private Node first;
        private int number;
        public void Push(T data)
        {
            Node old = first;
            first = new Node();
            first.Item = data;
            first.Next = first;
            number++;
        }
        public T Pop()
        {
            T item = first.Item;
            first = first.Next;
            number--;
            return item;
        }
    }
}

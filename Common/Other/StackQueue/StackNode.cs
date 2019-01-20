using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace Common
{
    public class StackNode<T>
    {
        class Node
        {
            public T Item { get; set; }
            public Node Next { get; set; }
        }
        private Node first = null;
        private int number = 0;
        public void Push(T data)
        {
            Node oldFirst = first;
            first = new Node();
            first.Item = data;
            first.Next = oldFirst;
            number++;
        }
        public T Pop()
        {
            T temp = first.Item;
            first = first.Next;
            number--;
            return temp;
        }
    }
}

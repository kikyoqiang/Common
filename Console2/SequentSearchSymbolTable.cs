using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2
{
    public class SequentSearchSymbolTable<TKey, TValue>
    {
        private int length = 0;
        Node first;

        public TValue Get(TKey key)
        {
            TValue result = default(TValue);
            Node temp = first;
            while (temp != null)
            {
                if (temp.Key.Equals(key))
                {
                    result = temp.Value;
                    break;
                }
                temp = temp.Next;
            }
            return result;
        }

        public void Put(TKey key, TValue value)
        {
            Node temp = first;
            while (temp != null)
            {
                if (temp.Key.Equals(key))
                {
                    temp.Value = value;
                    return;
                }
                temp = temp.Next;
            }
            first = new Node(key, value, first);
            length++;
        }

        #region Node
        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Next { get; set; }

            public Node(TKey key, TValue value, Node next)
            {
                this.Key = key;
                this.Value = value;
                this.Next = next;
            }
        }
        #endregion
    }
}

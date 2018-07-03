using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 无序链表实现查找表
    /// </summary>
    public class SequentSearchSymbolTable<TKey, TValue> : SymbolTables<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private int length = 0;
        Node first;

        public override TValue Get(TKey key)
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

        public override void Put(TKey key, TValue value)
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

        #region MyRegion
        public override bool Contains(TKey key)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TKey key)
        {
            throw new NotImplementedException();
        }

        public override bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        public override TKey[] Keys()
        {
            throw new NotImplementedException();
        }

        public override int Size()
        {
            throw new NotImplementedException();
        } 
        #endregion

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

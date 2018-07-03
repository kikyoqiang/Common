using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2
{
    public class BSS<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node root;
        private class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Number { get; set; }
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node(TKey key, TValue value, int number)
            {
                this.Key = key;
                this.Value = value;
                this.Number = number;
            }
        }

        public TValue Get(TKey key)
        {
            TValue result = default(TValue);
            while (root != null)
            {
                int cmp = key.CompareTo(root.Key);
                if (cmp < 0)
                {
                    root = root.Left;
                }
                else if (cmp > 0)
                {
                    root = root.Right;
                }
                else
                {
                    result = root.Value;
                    break;
                }
            }
            return result;
        }

        public TValue Get2(TKey key)
        {
            return GetValue(root, key);
        }
        private TValue GetValue(Node root, TKey key)
        {
            if (root == null) return default(TValue);
            int cmp = key.CompareTo(root.Key);
            if (cmp < 0)
                return GetValue(root.Left, key);
            else if (cmp > 0)
                return GetValue(root.Right, key);
            else
                return root.Value;
        }

        public void Put(TKey key, TValue value)
        {
            root = Put(root, key, value);
        }
        private Node Put(Node x, TKey key, TValue value)
        {
            if (x == null) return new Node(key, value, 1);
            int cmp = key.CompareTo(x.Key);
            if (cmp < 0)
                x.Left = Put(x.Left, key, value);
            else if (cmp > 0)
                x.Right = Put(x.Right, key, value);
            else
                x.Value = value;
            x.Number = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }
        private int Size(Node x)
        {
            return x == null ? 0 : x.Number;
        }

        public TKey GetMax()
        {
            TKey max = default(TKey);
            Node s = root;
            while (s != null)
            {
                s = s.Right;
            }
            max = s.Key;
            return max;
        }

        public TKey GetMin()
        {
            TKey min = default(TKey);
            Node s = root;
            while (s != null)
            {
                s = s.Left;
            }
            min = s.Key;
            return min;
        }

        public TKey GetMin2()
        {
            return GetMinPrivate(root);
        }
        private TKey GetMinPrivate(Node root)
        {
            if (root.Left == null) return root.Key;
            return GetMinPrivate(root.Left);
        }

        public TKey GetMax2()
        {
            return GetMaxPrivate(root);
        }
        private TKey GetMaxPrivate(Node x)
        {
            if (x.Right == null) return x.Key;
            return GetMaxPrivate(x.Right);
        }

        public TKey Floor(TKey key)
        {
            return default(TKey);
        }
        private Node Floor(Node x, TKey key)
        {
            if (x == null) return null;
            int cmp = key.CompareTo(x.Key);
            if (cmp == 0) return x;
            else if (cmp < 0) return Floor(x.Left, key);
            else
            {
                Node right = Floor(x.Right, key);
                if (right == null) return x;
                else return right;
            }
        }

        public void DelMin()
        {
            Node x = DelMin(root);
        }
        private Node DelMin(Node root)
        {
            if (root.Left == null) return root.Right;
            root.Left = DelMin(root.Left);
            root.Number = Size(root.Left) + Size(root.Right) + 1;
            return root;
        }

        public void Delete(TKey key)
        {
            root = Delete(root, key);
        }
        private Node Delete(Node x, TKey key)
        {
            int cmp = key.CompareTo(x.Key);
            if (cmp > 0)
                x.Right = Delete(x.Right, key);
            else if (cmp < 0)
                x.Left = Delete(x.Left, key);
            else
            {
                if (x.Left == null)
                    return x.Right;
                else if (x.Right == null)
                    return x.Left;
                else
                {
                    Node t = x;
                    x = GetMinNode(t.Right);
                    x.Right = DelMin(t.Right);
                    x.Left = t.Left;
                }
            }
            x.Number = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }
        private Node GetMinNode(Node x)
        {
            if (x.Left == null) return x.Right;
            else return GetMinNode(x.Left);
        }
    }
}

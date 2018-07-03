using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2
{
    public class BinarySearchTreeSymbolTable<TKey, TValue> : SymbolTables<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private Node root;
        private class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public int Number { get; set; }
            public Node(TKey key, TValue value, int number)
            {
                this.Key = key;
                this.Value = value;
                this.Number = number;
            }
        }

        public override TValue Get(TKey key)
        {
            TValue result = default(TValue);
            Node node = root;
            while (node != null)
            {
                int cmp = key.CompareTo(node.Key);
                if (cmp <= 0)
                {
                    node = node.Left;
                }
                else if (cmp > 0)
                {
                    node = node.Right;
                }
                else
                {
                    result = node.Value;
                    break;
                }
            }
            return result;
        }
        public TValue GetValue2(TKey key)
        {
            return GetValue(root, key);
        }
        private TValue GetValue(Node x, TKey key)
        {
            if (x == null) return default(TValue);
            int cmp = key.CompareTo(x.Key);
            if (cmp < 0)
                return GetValue(x.Left, key);
            else if (cmp > 0)
                return GetValue(x.Right, key);
            else
                return x.Value;
        }

        public override void Put(TKey key, TValue value)
        {
            Put(root, key, value);
        }

        private Node Put(Node x, TKey key, TValue value)
        {
            if (x == null) return new Node(key, value, 1);
            int cmp = key.CompareTo(x.Key);
            if (cmp < 0)
            {
                x.Left = Put(x.Left, key, value);
            }
            else if (cmp > 0)
            {
                x.Right = Put(x.Right, key, value);
            }
            else
            {
                x.Value = value;
            }
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

        public TKey Floor(TKey key)
        {
            Node x = Floor(root, key);
            if (x != null)
                return x.Key;
            else
                return default(TKey);
        }

        public void DelMin()
        {
            root = DelMin(root);
        }
        private Node DelMin(Node root)
        {
            if (root.Left == null) return root.Right;
            root.Left = DelMin(root.Left);
            root.Number = Size(root.Left) + Size(root.Right) + 1;
            return root;
        }

        private Node Floor(Node root, TKey key)
        {
            if (root == null) return null;
            int cmp = key.CompareTo(root.Key);
            if (cmp == 0)
                return root;
            else if (cmp <= 0)
                return Floor(root.Left, key);
            else
            {
                Node right = Floor(root.Right, key);
                if (right == null)
                    return root;
                else
                    return right;
            }
        }

        public override void Delete(TKey key)
        {
            root = Delete(root, key);
        }

        private Node Delete(Node x, TKey key)
        {
            int cmp = key.CompareTo(x.Key);
            if (cmp < 0)
                x.Left = Delete(x.Left, key);
            else if (cmp > 0)
                x.Right = Delete(x.Right, key);
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
            if (x.Left == null)
                return x;
            else
                return GetMinNode(x.Right);
        }

        public override bool Contains(TKey key)
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
    }
}

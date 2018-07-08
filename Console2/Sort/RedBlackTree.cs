using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2
{
    /// <summary> 红黑树 Todu Put未实现</summary>
    public class RedBlackTree<TKey, TValue> : SymbolTables<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        #region ready
        private const bool RED = true;
        private const bool BLACK = false;
        private Node root;
        #endregion

        #region Node
        private class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public int Number { get; set; }
            public bool Color { get; set; }

            public Node(TKey key, TValue value, int number, bool color)
            {
                this.Key = key;
                this.Value = value;
                this.Number = number;
                this.Color = color;
            }
        }
        #endregion

        #region Get
        public override TValue Get(TKey key)
        {
            return GetValue(root, key);
        }
        private TValue GetValue(Node node, TKey key)
        {
            if (node == null) return default(TValue);
            int cmp = key.CompareTo(node.Key);
            if (cmp == 0) return node.Value;
            else if (cmp > 0) return GetValue(node.Right, key);
            else return GetValue(node.Left, key);
        }
        #endregion

        #region Put
        public override void Put(TKey key, TValue value)
        {
            root = Put(root, key, value);
            root.Color = BLACK;
        }
        private Node Put(Node h, TKey key, TValue value)
        {
            if (h == null) return new Node(key, value, 1, RED);
            int cmp = key.CompareTo(h.Key);
            if (cmp < 0) h.Left = Put(h.Left, key, value);
            else if (cmp > 0) h.Right = Put(h.Right, key, value);
            else h.Value = value;

            //if (IsRed(h.Right) && !IsRed(h.Left)) RotateLeft(h);
            if (IsRed(h.Right) && IsRed(h.Left.Left)) RotateRight(h);
            if (IsRed(h.Left.Left)) RotateRight(h);
            if (IsRed(h.Left) && IsRed(h.Right)) h = FlipColor(h);

            h.Number = (h.Left == null ? 0 : h.Left.Number) + (h.Right == null ? 0 : h.Right.Number) + 1;
            return h;
        }
        #endregion

        #region RotateLeft
        private Node RotateLeft(Node h)
        {
            Node x = h.Right;
            h.Right = x.Left;
            x.Left = h;
            x.Color = h.Color;
            h.Color = RED;
            return x;
        }
        #endregion

        #region RotateRight
        private Node RotateRight(Node h)
        {
            Node x = h.Left;
            h.Left = x.Right;
            x.Right = h;
            x.Color = h.Color;
            h.Color = RED;
            return x;
        }
        #endregion

        #region common
        private bool IsRed(Node node)
        {
            if (node == null) return false;
            return node.Color == RED;
        }
        private int Size(Node node)
        {
            if (node == null) return 0;
            return node.Number;
        }
        private Node FlipColor(Node node)
        {
            if (node == null || node.Left == null || node.Right == null) return null;
            node.Left.Color = BLACK;
            node.Right.Color = BLACK;
            node.Color = RED;
            return node;
        }
        #endregion

        #region other
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2
{
    /// <summary>
    /// 二分查找实现查找表
    /// </summary>
    public class BinarySearchSymbolTable<TKey, TValue> : SymbolTables<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private TKey[] keys;
        private TValue[] values;
        private int length;
        private static readonly int INIT_Capacity = 2;
        public BinarySearchSymbolTable(int capacity)
        {
            keys = new TKey[capacity];
            values = new TValue[capacity];
            length = capacity;
        }
        public BinarySearchSymbolTable() : this(INIT_Capacity) { }

        public override TValue Get(TKey key)
        {
            int i = Rank(key);
            if (i < length && key.Equals(keys[i]))
                return values[i];
            else
                return default(TValue);
        }

        public override void Put(TKey key, TValue value)
        {
            int i = Rank(key);
            if (i < length && key.Equals(keys[i]))
            {
                values[i] = value;
                return;
            }
            if (length == keys.Length)
                Resize(2 * length);
            for (int j = length; j > i; j--)
            {
                keys[j] = keys[j - 1];
                values[j] = values[j - 1];
            }
            keys[i] = key;
            values[i] = value;
            length++;
        }

        private int Rank(TKey key)
        {
            int lo = 0, hi = length - 1;
            while (lo <= hi)
            {
                int mid = lo + (hi - lo) / 2;
                if (key.CompareTo(keys[mid]) < 0)
                    hi = mid - 1;
                else if (key.CompareTo(keys[mid]) > 0)
                    lo = mid + 1;
                else
                    return mid;
            }
            return lo;
        }

        private int Rank(TKey key, int lo, int hi)
        {
            if (lo >= hi) return lo;
            int mid = lo + (hi - lo) / 2;
            if (key.CompareTo(keys[mid]) < 0)
                return Rank(key, lo, mid - 1);
            else if (key.CompareTo(keys[mid]) > 0)
                return Rank(key, mid + 1, hi);
            else
                return mid;
        }

        private void Resize(int length)
        {
            TKey[] keysTemp = new TKey[length];
            for (int i = 0; i < keys.Length; i++)
            {
                keysTemp[i] = keys[i];
            }
            keys = keysTemp;

            TValue[] valuesTemp = new TValue[length];
            for (int i = 0; i < values.Length; i++)
            {
                valuesTemp[i] = values[i];
            }
            values = valuesTemp;
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
    }
}

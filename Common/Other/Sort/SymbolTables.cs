using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 抽象符号表
    /// </summary>
    public abstract class SymbolTables<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public SymbolTables()
        {

        }
        public abstract void Put(TKey key, TValue value);
        public abstract TValue Get(TKey key);
        public abstract void Delete(TKey key);
        public abstract bool Contains(TKey key);
        public abstract bool IsEmpty();
        public abstract int Size();
        public abstract TKey[] Keys();
    }
}

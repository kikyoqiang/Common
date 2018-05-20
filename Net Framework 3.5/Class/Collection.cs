using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Net_Framework_3._5
{
    public class Collection : CollectionBase
    {
        public void Add(Object item)
        {
            InnerList.Add(item);
        }
        public void Remove(Object item)
        {
            InnerList.Remove(item);
        }
        public new int Count()
        {
            return InnerList.Count;
        }
        public new void Clear()
        {
            InnerList.Clear();
        }
    }
}

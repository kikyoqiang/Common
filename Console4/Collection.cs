using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console4
{
    public class Collection : CollectionBase
    {
        public void Add(object item)
        {
            InnerList.Add(item);
        }
        public void Remove(object item)
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

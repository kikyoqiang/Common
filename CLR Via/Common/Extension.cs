using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLR_Via
{
    public static class Extension
    {
        /// <summary> 对事件的原子操作执行 </summary>
        public static void Raise<TEventArgs>(this TEventArgs e, object sender, ref EventHandler<TEventArgs> eventDelegate) where TEventArgs : EventArgs
        {
            EventHandler<TEventArgs> temp = System.Threading.Volatile.Read(ref eventDelegate);
            temp?.Invoke(sender, e);
        }
    }
}

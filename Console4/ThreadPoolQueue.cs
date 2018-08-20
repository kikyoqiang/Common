using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console4
{
    public class ThreadPoolQueue
    {
        public static int Count = 0;
        public static ManualResetEvent eventX = new ManualResetEvent(false);  //新建ManualResetEvent对象并且初始化为无信号状态
        public static bool QueueUserWorkItem(WaitCallback callBack, ref int count)
        {
            bool isOk = ThreadPool.QueueUserWorkItem(callBack);
            Interlocked.Increment(ref Count);
            if (Count == count)
            {
                eventX.Set();
            }
            return isOk;
        }
        public static bool QueueUserWorkItem(WaitCallback callBack, object state, ref int Count)
        {
            bool isOk = ThreadPool.QueueUserWorkItem(callBack, state);
            Interlocked.Increment(ref Count);

            return isOk;
        }
    }
}

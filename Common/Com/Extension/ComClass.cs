using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    class ComClass
    {
    }

    #region 线程池
    /// <summary>
    /// 线程池
    /// </summary>
    public class Pool
    {
        /// <summary>
        /// 将方法排入队列以便执行，并指定包含该方法所用数据的对象。此方法在有线程池线程变得可用时执行。
        /// <para>System.Threading.WaitCallback，它表示要执行的方法。</para>
        /// </summary>
        /// <returns></returns>
        public static bool Go(System.Threading.WaitCallback callBack)
        {
            return System.Threading.ThreadPool.QueueUserWorkItem(callBack);
        }

        /// <summary>
        /// 将方法排入队列以便执行，并指定包含该方法所用数据的对象。此方法在有线程池线程变得可用时执行。
        /// <para>System.Threading.WaitCallback，它表示要执行的方法。</para>
        /// <para>state 包含方法所用数据的对象。</para>
        /// </summary>
        /// <returns></returns>
        public static bool Go(System.Threading.WaitCallback callBack, object state)
        {
            return System.Threading.ThreadPool.QueueUserWorkItem(callBack, state);
        }

        /// <summary>
        /// 将当前线程挂起指定的时间
        /// <para>millisecondsTimeout 线程被阻塞的毫秒数。指定零 (0) 以指示应挂起此线程以使其他等待线程能够执行。指定 System.Threading.Timeout.Infinite 以无限期阻止线程。</para>
        /// </summary>
        public static void Sleep(int millisecondsTimeout)
        {
            System.Threading.Thread.Sleep(millisecondsTimeout);
        }

        /// <summary>
        /// 将当前线程阻塞指定的时间
        /// <para>timeout: 设置为线程被阻塞的时间量的 System.TimeSpan。指定零以指示应挂起此线程以使其他等待线程能够执行。指定 System.Threading.Timeout.Infinite 以无限期阻止线程。</para>
        /// </summary>
        public static void Sleep(TimeSpan timeout)
        {
            System.Threading.Thread.Sleep(timeout);
        }
    } 
    #endregion
}

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
        public static bool Run(System.Threading.WaitCallback callBack)
        {
            return System.Threading.ThreadPool.QueueUserWorkItem(callBack);
        }
        /// <summary>
        /// 将方法排入队列以便执行，并指定包含该方法所用数据的对象。此方法在有线程池线程变得可用时执行。
        /// <para>System.Threading.WaitCallback，它表示要执行的方法。</para>
        /// <para>state 包含方法所用数据的对象。</para>
        /// </summary>
        /// <returns></returns>
        public static bool Run(System.Threading.WaitCallback callBack, object state)
        {
            return System.Threading.ThreadPool.QueueUserWorkItem(callBack, state);
        }
    } 
    #endregion
}

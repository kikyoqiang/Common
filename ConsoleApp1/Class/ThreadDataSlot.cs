using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// 包含线程方法和数据插槽
    /// </summary>
    public class ThreadDataSlot
    {
        //[ThreadStatic]
        //private static string currentData;
        private static LocalDataStoreSlot localSlot = Thread.AllocateDataSlot();

        public static void Work()
        {
            Thread.SetData(localSlot, Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine("线程{0}内的数据是：{1}", Thread.CurrentThread.ManagedThreadId.ToSafeString(),
                Thread.GetData(localSlot).ToSafeString());
            Thread.Sleep(1000);
            Console.WriteLine("线程{0}内的数据是：{1}", Thread.CurrentThread.ManagedThreadId.ToSafeString(),
                Thread.GetData(localSlot).ToSafeString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console3
{
    public class Lock
    {
        private static object staticLock = new object();
        private object instanceLock = new object();

        private static int staticNumber;
        private int instanceNumber;

        public static void StaticIncrement(object state)
        {
            lock (staticLock)
            {
                Console.WriteLine("当前线程id：{0}", Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("staticNumber的值为:{0}", staticNumber);
                Thread.Sleep(200);
                staticNumber++;
                Console.WriteLine("staticNumber自增后的值为 {0}", staticNumber);
            }
        }

        public void InstanceIncrement(object state)
        {
            lock (instanceLock)
            {
                Console.WriteLine("当前线程id：{0}", Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("instanceNumber的值为:{0}", staticNumber);
                Thread.Sleep(200);
                instanceNumber++;
                Console.WriteLine("instanceNumber自增后的值为 {0}", staticNumber);
            }
        }
    }
}

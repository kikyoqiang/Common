using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console3
{
    class Class12
    {
        static Semaphore semaphore = new Semaphore(2, 2);

        const int threadSize = 4;

        static void Main22()
        {
            for (int i = 0; i < threadSize; i++)
            {
                Thread thread = new Thread(ThreadEntry);
                thread.Start(i + 1);
            }
            Console.ReadKey();
        }

        static void ThreadEntry(object id)
        {
            Console.WriteLine("线程{0}申请进入本方法", id);
            semaphore.WaitOne();
            Console.WriteLine("线程{0}成功进入本方法", id);
            Thread.Sleep(10000);
            Console.WriteLine("线程{0}执行完毕离开了", id);
            semaphore.Release();
        }
    }
}

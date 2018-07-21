using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console3
{
    public class ThreadStatic
    {
        [ThreadStatic]
        private static int threadId = 0;
        
        private static Ref refThreadId = new Ref();

        public static void Work()
        {
            threadId = Thread.CurrentThread.ManagedThreadId;
            refThreadId.Id= Thread.CurrentThread.ManagedThreadId;

            // 查看一下刚刚插入的数据
            Console.WriteLine("[线程{0}]：线程静态值变量：{1}，线程静态引用变量：{2}", 
                Thread.CurrentThread.ManagedThreadId.ToString(), threadId, refThreadId.Id.ToString());
            // 睡眠1s
            Thread.Sleep(1000);
            // 查看其他线程的运行是否干扰了当前线程静态数据
            Console.WriteLine("[线程{0}]：线程静态值变量：{1}，线程静态引用变量：{2}", 
                Thread.CurrentThread.ManagedThreadId.ToString(), threadId, refThreadId.Id.ToString());
        }

        private class Ref
        {
            private int _Id;

            public int Id
            {
                get
                {
                    return _Id;
                }

                set
                {
                    _Id = value;
                }
            }

        }
    }
}

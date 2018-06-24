using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class10
    {
        static void Main22()
        {
            Console.WriteLine("开始使用");
            SynchroThis st = new SynchroThis();
            Monitor.Enter(st);

            Thread t = new Thread(st.Work);
            t.Start();
            t.Join();
            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
    class SynchroThis
    {
        private int number;
        public void Work(object state)
        {
            lock (this)
            {
                Console.WriteLine("number现在的值为:{0}", number);
                number++;
                Thread.Sleep(200);
                Console.WriteLine("number自增后的值为:{0}", number);
            }
        }
    }
}

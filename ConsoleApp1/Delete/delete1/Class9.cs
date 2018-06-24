using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class9
    {
        static void Main22()
        {
            Console.WriteLine("开始测试静态方法的同步");
            for (int i = 0; i < 5; i++)
            {
                Thread t = new Thread(Lock.StaticIncrement);
                t.Start();
            }
            Thread.Sleep(5 * 1000);
            Console.WriteLine("-------------------------------");
            Console.WriteLine("开始测试实例方法的同步：");
            Lock l = new Lock();
            for (int i = 0; i < 5; i++)
            {
                Thread t = new Thread(l.InstanceIncrement);
                t.Start();
            }

            Console.ReadKey();
        }
        static int haha(int a, int b)
        {
            return a + b;
        }
    }
    public class Person22
    {
        private int a;

        public int A
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }
    }
}

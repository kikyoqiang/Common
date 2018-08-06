using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace IFMTest
{
    class Program
    {
        static void Main()
        {
            string s = "嘎嘎";
            var a = SpecialCodeHelper.ToBase64String(s);

            var b = SpecialCodeHelper.UnBase64String(a);

            Console.ReadKey();
        }

        private static void Queue2()
        {
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(1000);
                string s2 = i.ToSafeString();
                //LogHelper.Instance.WriteInfo(s2);
            }
        }
        private static void Queue()
        {
            object locker = new object();
            Queue<int> q = new Queue<int>();
            for (int i = 0; i < 20; i++)
            {
                string s2 = i.ToSafeString();
                ThreadPool.QueueUserWorkItem(obj =>
                {
                    Thread.Sleep(1000);
                    lock (locker)
                    {
                        //LogHelper.Instance.WriteInfo(s2);
                    }
                });
            }
            int i1;
            int i2;
            while (true)
            {
                ThreadPool.GetAvailableThreads(out i1, out i2);
                if (i1 == 1000)
                {
                    Console.WriteLine("OK");
                    break;
                }
            }
        }
    }
}

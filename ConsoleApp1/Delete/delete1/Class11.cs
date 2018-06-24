using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class11
    {
        const string testFile = @"E:\0Project\0FaBu\2.txt";
        static Mutex mutex = new Mutex(false, "TestMutex");
        static void Main222()
        {
            string s = "22";
            Console.WriteLine(s.GetHashCode());
            //Thread.Sleep(3000);
            //DoWork();
            //mutex.Close();
            Console.ReadKey();
        }
        static void DoWork()
        {
            long d1 = DateTime.Now.Ticks;
            mutex.WaitOne();
            long d2 = DateTime.Now.Ticks;
            Console.WriteLine("经过了 {0} 个Tick后进程 {1} 得到互斥体，进入临界区代码。",
                (d2 - d1).ToSafeString(), Process.GetCurrentProcess().Id.ToSafeString());
            try
            {
                if (!File.Exists(testFile))
                {
                    Stream sm = File.Create(testFile);
                    sm.Dispose();
                }
                for (int i = 0; i < 5; i++)
                {
                    using (Stream sm = File.Open(testFile, FileMode.Append))
                    {
                        string content = $"[进程 {Process.GetCurrentProcess().Id} ]: {i} \r\n";
                        byte[] data = Encoding.Default.GetBytes(content);
                        sm.Write(data, 0, data.Length);
                    }
                    Thread.Sleep(300);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError("", ex);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}

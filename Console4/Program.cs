using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console4
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlServerHelper.
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                ThreadPool.QueueUserWorkItem(obj =>
                {
                    Thread.Sleep(1000 * random.Next(0, 5));
                    Console.WriteLine("haha");
                });
            }

            Console.ReadKey();

























            Console.ReadKey();
        }
    }
}

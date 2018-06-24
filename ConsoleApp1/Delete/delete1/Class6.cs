using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class6
    {
        static void Main22()
        {
            //Console.WriteLine("开始测试数据插槽：");

            //for (int i = 0; i < 5; i++)
            //{
            //    Thread thread = new Thread(ThreadStatic.Work);
            //    thread.Start();
            //}
            DataTable dt = new DataTable();
            if (dt.IsNullOrEmpty())
            {
                Console.WriteLine("null");
            }

            Console.ReadKey();
        }
    }
}
 
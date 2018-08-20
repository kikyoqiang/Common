using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console4
{
    class Program
    {
        static void Main(string[] args)
        {
            Process[] process = Process.GetProcesses();
            List<Process> list = process.ToList();
            Process lol = list.Find(e => e.ProcessName.Contains(@"Legends"));
            for (int i = 0; i < process.Length; i++)
            {
                if (process[i].ProcessName.Contains("Legends"))
                {
                    process[i].Kill();
                }
            }
            return;
            while (true)
            {
                Console.WriteLine("请输入一个十进制数：");

                int num = int.Parse(Console.ReadLine());

                string result = string.Empty;

                Console.WriteLine("转化的二进制为：" + ConvertToBinary(out result, num));


            }
            Console.ReadKey();
        }
        static string ConvertToBinary(out string str, int num)
        {
            str = string.Empty;
            if (num == 0)
                return str;
            ConvertToBinary(out str, num / 2);
            return str += num % 2;
        }
    }
}

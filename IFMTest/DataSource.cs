using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IFMTest
{
    public class DataSource
    {
        /// <summary>
        /// 模拟从数据库读取数据
        /// 耗时、耗CPU
        /// </summary>
        /// <param name="count"></param>
        public static int GetDataByDB(int count)
        {
            Console.WriteLine("-------GetDataByDB-------");
            int result = 0;
            for (int i = count; i < 99999999; i++)
            {
                result += i;
            }
            Thread.Sleep(2000);
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFMTest
{
    class Class1
    {
        static void Main()
        {
            Console.ReadKey();
        }
        private static string GetMaxTime(string timestr1, string timestr2)
        {
            DateTime time1 = timestr1.ToDateTime();
            DateTime time2 = timestr2.ToDateTime();
            int cmp = time2.CompareTo(time1);
            return cmp >= 0 ? timestr2 : timestr1;
        }
    }
}

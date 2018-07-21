using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Globalization;

namespace Console3
{
    class Class5
    {
        private static GregorianCalendar gcom = new GregorianCalendar();
        static void Main22()
        {
            for (int i = 0; i < 2500; i++)
            {
                DateTime NowTime = DateTime.Parse("2018-01-01");
                DateTime date = NowTime.AddDays(i);
                if (Utility.GetWeekByDate(date) != Utility.GetWeekStrOfYear(date))
                {
                    Console.WriteLine(date);
                }
            }
            ////DateTime date = DateTime.Parse("2024-01-01");
            //Console.WriteLine(string.Format("{0} 是 {1}", date, Utility.GetWeekByDate(date)));
            //Console.WriteLine(string.Format("{0} 是 {1}", date, Utility.GetWeekStrOfYear(date)));
            //Console.WriteLine();

            Console.ReadKey();
        }

        /// <summary>获得指定时间 是一年中的第几周</summary> 
        public static int GetWeekIntOfYear(DateTime dateTime)
        {
            return new GregorianCalendar().GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        /// <summary>获得指定时间 单周还是双周</summary> 
        public static string GetWeekStrOfYear(DateTime dateTime)
        {
            return GetWeekIntOfYear(dateTime) % 2 == 1 ? "单周" : "双周";
        }
    }
}

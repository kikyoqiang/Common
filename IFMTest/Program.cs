using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFMTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = DateTime.MinValue;
            string date = "1991-09-10";
            var a = Utility.GetAgeByBirthDay(date.ToDateTime());
            var b = date.ToDateTime().GetAgeByBirthDate();
            //string s = "1.6307692307692307692307692308";
            //string ss = s.Substring(0, s.IndexOf('.') + 4);
            //string[] strs1 = new string[] { "", "", "" };
            //string[] strs2 = new string[] { "", "1", "" };
            //string[] strs3 = new string[] { "", " ", "" };
            //var a = IsAllEmpty(strs1);
            //var b = IsAllEmpty(strs2);
            //var c = IsAllEmpty(strs3);
            Console.ReadKey();
        }
        private static bool IsAllEmpty(string[] strs)
        {
            foreach (string item in strs)
            {
                if (!string.IsNullOrEmpty(item.Trim()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

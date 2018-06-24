using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // 定义一个委托
    public delegate string GetStringDelegate();
    class Class13
    {
        static void Main22()
        {
            // GetSelfDefinedString方法被最后添加
            GetStringDelegate myDelegate1 = GetDateTimeString;
            myDelegate1 += GetTypeNameString;
            myDelegate1 += GetSelfDefinedString;
            //Console.WriteLine(myDelegate1());
            //Console.WriteLine();

            foreach (var item in myDelegate1.GetInvocationList())
            {
                Console.WriteLine(item.DynamicInvoke());
            }
            // GetDateTimeString方法被最后添加
            //GetStringDelegate myDelegate2 = GetSelfDefinedString;
            //myDelegate2 += GetTypeNameString;
            //myDelegate2 += GetDateTimeString;
            //Console.WriteLine(myDelegate2());
            //Console.WriteLine();
            //// GetTypeNameString方法被最后添加
            //GetStringDelegate myDelegate3 = GetSelfDefinedString;
            //myDelegate3 += GetDateTimeString;
            //myDelegate3 += GetTypeNameString;
            //Console.WriteLine(myDelegate3());

            Console.ReadKey();
        }
        static string GetDateTimeString()
        {
            return DateTime.Now.ToString();
        }

        static string GetTypeNameString()
        {
            return typeof(Class13).ToString();
        }

        static string GetSelfDefinedString()
        {
            string result = "我是一个字符串！";
            return result;
        }
    }
}

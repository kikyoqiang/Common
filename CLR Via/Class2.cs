using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CLR_Via
{
    delegate void Feedback(int value);
    delegate object MyCallback(FileStream fs);
    delegate object TwoInts(int a, int b);
    delegate object OneString(string str);
    class Class2
    {
        static void Main22(string[] args)
        {
            if (args.Length < 2)
            {

            }

            Type deltype = Type.GetType(args[0]);

            Delegate d;
            try
            {
                MethodInfo mi = typeof(Class2).GetTypeInfo().GetDeclaredMethod(args[1]);
                d = mi.CreateDelegate(deltype);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private static object Add(int a, int b)
        {
            return a + b;
        }
        private static object Subtract(int a, int b)
        {
            return a - b;
        }
        private static object NumChars(string s1)
        {
            return s1.Length;
        }
        private static object Reverse(string s1)
        {
            return new string(s1.Reverse().ToArray());
        }
        static string SomeMethod(Stream s)
        {
            return "";
        }
        private static void UseLocal(int num)
        {
            int[] ints = new int[num];
            AutoResetEvent done = new AutoResetEvent(false);
            for (int i = 0; i < ints.Length; i++)
            {
                ThreadPool.QueueUserWorkItem(obj =>
                {
                    int a = (int)obj;
                    ints[a] = a * a;
                    if (Interlocked.Decrement(ref num) == 0)
                    {
                        done.Set();
                    }
                }, i);
            }
            done.WaitOne();
            for (int i = 0; i < ints.Length; i++)
            {
                Console.WriteLine("index {0} , ints={1}", i, ints[i]);
            }
        }
    }
}

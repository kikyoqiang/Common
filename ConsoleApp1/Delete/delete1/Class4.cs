using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace ConsoleApp1
{
    class Class4
    {
        static void Main22()
        {
            Person2 p = new Person2();
            p.Name = "AAA";
            p.Sex = "1";
            p.Age = 18;
            Person2 p2 = new Person2();
            p2.Name = "AAA";
            p2.Sex = "1";
            p2.Age = 20;
            Person2 p3 = new Person2();
            p3.Name = "AAA";
            p3.Sex = "1";
            p3.Age = 22;
            List<Person2> list = new List<Person2>();
            list.Add(p);
            list.Add(p2);
            list.Add(p3);
            var a = from m in list
                    group m by new { m.Name, m.Sex } into g
                    where g.Any(e => e.Age > 20)
                    select new { key = g.Key, Value = g.Where(e => e.Age > 20) };
            var b = a.ToList();
            foreach (var item in b)
            {
                //Console.WriteLine(string.Format(" {0} {1} {2} ", item.key.Name, item.key.Sex, item.Value.FirstOrDefault().Age));
                foreach (var item2 in item.Value)
                {
                    Console.WriteLine(string.Format(" {0} {1} {2} ", item2.Name, item2.Sex, item2.Age));
                }
            }
            Console.ReadKey();
            //// 测试二者的功能
            //OverrideBase ob = new OverrideBase();
            //NewBase nb = new NewBase();

            //Console.WriteLine(ob.ToString() + ":" + ob.GetString());
            //Console.WriteLine(nb.ToString() + ":" + nb.GetString());

            //Console.WriteLine();

            //// 测试二者的区别
            //BaseClass obc = ob as BaseClass;
            //BaseClass nbc = nb as BaseClass;

            //Console.WriteLine(obc.ToString() + ":" + obc.GetString());
            //Console.WriteLine(nbc.ToString() + ":" + nbc.GetString());

            //Console.ReadKey();
        }
    }
    // Base class
    public class BaseClass
    {
        public virtual string GetString()
        {
            return "我是基类";
        }
    }
    public class Person2
    {
        public string Name;
        public string Sex;
        public int Age;
    }
    // Override
    public class OverrideBase : BaseClass
    {
        public override string GetString()
        {
            return "我重写了基类";
        }
    }

    // Hide
    public class NewBase : BaseClass
    {
        public new virtual string GetString()
        {
            return "我隐藏了基类";
        }
    }
}

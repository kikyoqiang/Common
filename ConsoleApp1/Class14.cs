using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class14
    {
        static void Main22()
        {
            Cat c = new Cat("Tom");
            Mouse m = new Mouse("Jerry", c);
            Master ma = new Master("唐嫣", c);
            c.CatCry();
            Console.ReadKey();
        }
    }
    public class Cat
    {
        public event Action<string> CatCryEvent;
        private string name;
        public Cat(string name)
        {
            this.name = name;
            CatCryEvent = new Action<string>(obj => { Console.WriteLine("{0}叫了", name); });
        }
        public void CatCry()
        {
            CatCryEvent(name);
        }
    }
    public class Mouse
    {
        private string name;
        public Mouse(string name, Cat cat)
        {
            this.name = name;
            cat.CatCryEvent += Run;
        }
        public void Run(string name)
        {
            Console.WriteLine("{0}逃走了：我勒个去，赶紧跑啊！", this.name);
        }
    }
    public class Master
    {
        private string name;
        public Master(string name, Cat cat)
        {
            this.name = name;
            cat.CatCryEvent += WakeUp;
        }
        public void WakeUp(string name)
        {
            Console.WriteLine("{0}醒了：我勒个去，叫个锤子！", this.name);
        }
    }
}

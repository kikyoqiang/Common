using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console3
{
    public abstract class Creator
    {
        public abstract Food2 CteateFoodFactory();
    }

    public class TomatoFractory : Creator
    {
        public override Food2 CteateFoodFactory()
        {
            return new Tomato();
        }
    }

    public class TuDouSiFactory : Creator
    {
        public override Food2 CteateFoodFactory()
        {
            return new TuDouSi2();
        }
    }

    public abstract class Food2
    {
        public abstract void Print();
    }

    public class Tomato : Food2
    {
        public override void Print()
        {
            Console.WriteLine("一份炒鸡蛋");
        }
    }

    public class TuDouSi2 : Food2
    {
        public override void Print()
        {
            Console.WriteLine("一份土豆丝");
        }
    }
}

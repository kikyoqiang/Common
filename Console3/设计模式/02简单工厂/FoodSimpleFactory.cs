using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console3
{
    public class FoodSimpleFactory
    {
        public static Food CreateFood(string type)
        {
            Food food = null;
            if (type.Equals("土豆丝"))
            {
                food = new TuDouSi();
            }
            else if (type.Equals("炒鸡蛋"))
            {
                food = new TomatoScrambleEggs();
            }
            return food;
        }
    }

    public abstract class Food
    {
        public abstract void Print();
    }

    public class TomatoScrambleEggs : Food
    {
        public override void Print()
        {
            Console.WriteLine("一份西红柿柴鸡蛋");
        }
    }

    public class TuDouSi : Food
    {
        public override void Print()
        {
            Console.WriteLine("一份土豆丝");
        }
    }
}

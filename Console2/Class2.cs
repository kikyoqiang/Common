using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2
{
    class Class2
    {
        static void Main()
        {
            string s = "";
            var a = s.ToDateStrHH();
            Console.ReadKey();

        }
        static Dictionary<decimal, int> Exchange(decimal num)
        {
            var money = GetInt();
            int i = 0;
            while (true)
            {
                if (num <= 0.05M)
                {
                    return money;
                }
                var max = money.Keys.ElementAt(i);
                if (num >= max)
                {
                    num = num - max;
                    money[max] = money[max] + 1;
                }
                else
                {
                    if (num < 0.1M && num >= 0.05M)
                    {
                        money[0.10M] = money[0.10M] + 1;
                        num = 0.00M;
                    }
                    else
                    {
                        num++;
                    }
                }
            }
            return null;
        }
        static Dictionary<decimal, int> GetInt()
        {
            Dictionary<decimal, int> money = new Dictionary<decimal, int>();
            //key表示钱，value表示钱的张数
            money.Add(100.00M, 0);
            money.Add(50.00M, 0);
            money.Add(20.00M, 0);
            money.Add(10.00M, 0);
            money.Add(5.00M, 0);
            money.Add(2.00M, 0);
            money.Add(1.00M, 0);
            money.Add(0.50M, 0);
            money.Add(0.20M, 0);
            money.Add(0.10M, 0);
            return money;
        }
    }
}

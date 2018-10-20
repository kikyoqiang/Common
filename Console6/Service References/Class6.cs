using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console6
{
    class Class6
    {
        private static int _quantity = 0;
        private static int _itemPrice = 0;
        static void Main1()
        {
            int a = 2;
            Console.WriteLine(1);
            Console.WriteLine(2);
            if (a > 2)
            {
                Console.WriteLine(222);
            }
            Console.WriteLine(3);
            Console.WriteLine(4);
        }
        double price()
        {
            double basePrice = _quantity * _itemPrice;
            double quantityDiscount = Math.Max(0, _quantity - 500) * _itemPrice * 0.05;
            double shipping = Math.Min(basePrice * 0.1, 100.0);
            return basePrice - quantityDiscount + shipping;
        }
        static double getPrice()
        {
            return BasePrice() * DiscountFactor();
        }
        private static int BasePrice()
        {
            return _quantity * _itemPrice;
        }
        private static double DiscountFactor()
        {
            if (BasePrice() > 1000)
                return 0.95;
            else
                return 0.98;
        }
    }
}

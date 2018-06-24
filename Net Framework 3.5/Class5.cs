using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Net_Framework_3._5
{
    class Class5
    {
        static void Main()
        {
            DateTime dt = DateTime.Today;
            int result = dt.CompareTo(DateTime.MaxValue);
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}

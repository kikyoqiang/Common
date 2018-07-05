using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFMTest
{
    class Class1
    {
        static void Main()
        {
            string now = DateTime.Now.ToDateStr().AddStr23();
            DateTime date = now.ToDateTime();
            Console.ReadKey();  
        }
    }
}

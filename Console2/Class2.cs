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
            //SequentSearchSymbolTable<int, int> s = new SequentSearchSymbolTable<int, int>();
            BinarySearchSymbolTable<int, string> s = new BinarySearchSymbolTable<int, string>();
            s.Put(1, "哈哈1");
            s.Put(2, "哈哈2");
            s.Put(3, "哈哈3");
            s.Put(4, "哈哈4");
            s.Put(5, "哈哈5");
            s.Put(6, "哈哈6");
            Console.WriteLine(s.Get(1));
            Console.WriteLine(s.Get(2));
            Console.WriteLine(s.Get(3));
            Console.WriteLine(s.Get(4));
            Console.ReadKey();
        }
    }
}

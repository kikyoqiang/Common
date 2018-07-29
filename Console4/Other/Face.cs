using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console4
{
    public class Face
    {
        private SystemA systemA;
        private SystemB systemB;
        public Face()
        {
            systemA = new SystemA();
            systemB = new SystemB();
        }
    }
    public class SystemA
    {
        public bool IsOkA(string a)
        {
            Console.WriteLine("正在验证 a");
            return true;
        }
    }
    public class SystemB
    {
        public bool IsOkB(string b)
        {
            Console.WriteLine("正在验证 b");
            return true;
        }
    }
}

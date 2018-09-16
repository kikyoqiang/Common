using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console4
{
    class Class1
    {
        static void Main()
        {
            Bird bird1 = new Bird();
            Bird bird2 = new Sparrow();
            Sparrow sparrow1 = new Sparrow();



            Console.ReadKey();
        }
    }
    /// <summary>
    /// 鸟
    /// </summary>
    public class Bird
    {
        public int Id { get; set; }
    }
    /// <summary>
    /// 麻雀
    /// </summary>
    public class Sparrow : Bird
    {
        public string Name { get; set; }
    }
}

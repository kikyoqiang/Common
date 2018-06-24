using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// 对象的 适配器模式
    /// </summary>
    public class PowerAdapter2 : ThreeHole2
    {
        TwoHole2 twoHole2 = new TwoHole2();

        public override void Request()
        {
            twoHole2.SpecificRequest();
        }
    }

    public class ThreeHole2
    {
        public virtual void Request()
        {

        }
    }

    public class TwoHole2
    {
        public void SpecificRequest()
        {
            Console.WriteLine("两个洞的插座");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// 类的 适配器模式
    /// </summary>
    public class PowerAdapter : TwoHole, IThreeHole
    {
        public void Request()
        {
            this.SpecificRequest();
        }
    }

    public abstract class TwoHole
    {
        public void SpecificRequest()
        {
            Console.WriteLine("两个洞的插座");
        }
    }

    public interface IThreeHole
    {
        void Request();
    }

}

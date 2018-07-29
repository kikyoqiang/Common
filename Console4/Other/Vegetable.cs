using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console4
{
    public abstract class Vegetable
    {
        public void CookVegetable()
        {
            this.PourOil();
            this.HeatOil();
            PourVegetable();
            this.Stir_Fly();
        }
        public void PourOil()
        {
            Console.WriteLine("倒油");
        }
        public void HeatOil()
        {
            Console.WriteLine("把油烧热");
        }
        public abstract void PourVegetable();
        public void Stir_Fly()
        {
            Console.WriteLine("翻炒");
        }
    }
    public class Tomato : Vegetable
    {
        public override void PourVegetable()
        {
            Console.WriteLine("add tomato");
        }
    }
}

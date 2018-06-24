using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class15
    {
        static void Main22()
        {
            //Bird bird = new Bird();
            Bird chicken = new Chicken();
            Chicken chicken2 = new Chicken();
            chicken.ShowType();
            chicken2.ShowType();
            Console.ReadKey();
        }
    }
    public abstract class Animal
    {
        public abstract void ShowType();
        public void Eat()
        {
            Console.WriteLine("Animal always eat.");
        }
    }
    public class Bird : Animal
    {
        private string type = "Bird";
        public override void ShowType()
        {
            Console.WriteLine("type is {0}", type);
        }
        public string Color { get; set; }
    }
    public class Chicken : Bird
    {
        private string type = "Chicken";
        //public override void ShowType()
        //{
        //    Console.WriteLine("type is {0}", type);
        //}
        public new void ShowType()
        {
            Console.WriteLine("666");
        }
        public void ShowColor()
        {
            Console.WriteLine("color is {0}", Color);
        }
    }
}

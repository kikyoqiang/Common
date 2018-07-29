using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console4
{
    public abstract class Graphics
    {
        public string Name { get; set; }
        public Graphics(string name)
        {
            this.Name = name;
        }
        public abstract void Draw();
    }
    public class Line : Graphics
    {
        public Line(string name) : base(name)
        {
        }

        public override void Draw()
        {
            Console.WriteLine("画一个线条");
        }
    }
    public class Circle : Graphics
    {
        public Circle(string name) : base(name)
        {
        }

        public override void Draw()
        {
            Console.WriteLine("circle");
        }
    }
    public class ComplexGraphics : Graphics
    {
        private List<Graphics> list = new List<Graphics>();
        public ComplexGraphics(string name) : base(name)
        {
        }

        public override void Draw()
        {
            foreach (var item in list)
            {
                item.Draw();
            }
        }
        public void Add(Graphics g)
        {
            if (!list.Contains(g))
            {
                list.Add(g);
            }
        }
        public void Remove(Graphics g)
        {
            if (list.Contains(g))
            {
                list.Remove(g);
            }
        }
    }
}

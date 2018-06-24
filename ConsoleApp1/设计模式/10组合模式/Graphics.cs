using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// 组合模式
    /// </summary>
    /// 图形抽象类
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
            Console.WriteLine("画  " + Name);
        }
    }

    public class Circle : Graphics
    {
        public Circle(string name) : base(name)
        {
        }

        public override void Draw()
        {
            Console.WriteLine("画  " + Name);
        }
    }

    public class ComplexGraphics : Graphics
    {
        private List<Graphics> complexGraphicsList = new List<Graphics>();
        public ComplexGraphics(string name) : base(name)
        {
        }

        public override void Draw()
        {
            foreach (var item in complexGraphicsList)
            {
                item.Draw();
            }
        }

        public void Add(Graphics g)
        {
            complexGraphicsList.Add(g);
        }

        public void Remove(Graphics g)
        {
            complexGraphicsList.Remove(g);
        }
    }
}

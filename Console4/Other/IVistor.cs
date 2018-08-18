using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console4
{
    public interface IVistor
    {
        void Visit(Element element);
    }
    public abstract class Element
    {
        public abstract void Accept(IVistor vistor);
    }
    public class ElementA : Element
    {
        public override void Accept(IVistor vistor)
        {
            vistor.Visit(this);
        }
    }
    public class ElementB : Element
    {
        public override void Accept(IVistor vistor)
        {
            vistor.Visit(this);
        }
    }
    public class ConcreteVistor : IVistor
    {
        public void Visit(Element element)
        {
            Console.WriteLine("哈哈 我是访问者 正在访问此元素 {0} ",element.GetType().Name);
        }
    }
}

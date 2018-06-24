using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// 桥接模式
    /// </summary>
    public class RemoteControl
    {
        private TV implementor;
        public TV Implementor
        {
            get { return implementor; }
            set { implementor = value; }
        }
        public virtual void On()
        {
            implementor.On();
        }
        public virtual void Off()
        {
            implementor.Off();
        }
        public virtual void SetChannel()
        {
            implementor.Channel();
        }
    }

    public abstract class TV
    {
        public abstract void On();
        public abstract void Off();
        public abstract void Channel();
    }

    public class ConcreateRemote : RemoteControl
    {
        public override void SetChannel()
        {
            Console.WriteLine("---------------------");
            base.SetChannel();
            Console.WriteLine("---------------------");
        }
    }

    /// <summary>
    /// 长虹牌电视机，重写基类的抽象方法
    /// 提供具体的实现
    /// </summary>
    public class ChangHong : TV
    {
        public override void On()
        {
            Console.WriteLine("长虹牌电视机已经打开了");
        }

        public override void Off()
        {
            Console.WriteLine("长虹牌电视机已经关掉了");
        }

        public override void Channel()
        {
            Console.WriteLine("长虹牌电视机换频道");
        }
    }

    /// <summary>
    /// 三星牌电视机，重写基类的抽象方法
    /// </summary>
    public class Samsung : TV
    {
        public override void On()
        {
            Console.WriteLine("三星牌电视机已经打开了");
        }

        public override void Off()
        {
            Console.WriteLine("三星牌电视机已经关掉了");
        }

        public override void Channel()
        {
            Console.WriteLine("三星牌电视机换频道");
        }
    }
}

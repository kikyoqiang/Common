using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console4
{
    public class RemoteControl
    {
        public TV TV { get; set; }
        public virtual void On()
        {
            TV.On();
        }
        public virtual void Off()
        {
            TV.Off();
        }
        public virtual void SetChannel()
        {
            TV.SetChannel();
        }
    }
    public abstract class TV
    {
        public abstract void On();
        public abstract void Off();
        public abstract void SetChannel();
    }
    public class TV1 : TV
    {
        public override void Off()
        {
            Console.WriteLine("关");
        }

        public override void On()
        {
            Console.WriteLine("开");
        }

        public override void SetChannel()
        {
            Console.WriteLine("取消");
        }
    }
}

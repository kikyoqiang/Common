using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// 装饰者模式
    /// </summary>
    /// 装饰抽象类,要让装饰完全取代抽象组件，所以必须继承自Photo
    public abstract class Decorator : Phone
    {
        private Phone phone;

        public Decorator(Phone phone)
        {
            this.phone = phone;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Print()
        {
            if (phone != null)
            {
                phone.Print();
            }
        }
    }

    public abstract class Phone
    {
        public abstract void Print();
    }

    public class ApplePhone : Phone
    {
        public override void Print()
        {
            Console.WriteLine("开始执行具体的对象——苹果手机");
        }
    }

    public class Sticker : Decorator
    {
        public Sticker(Phone phone) : base(phone)
        {
        }

        public override void Print()
        {
            base.Print();
            AddSticker();
        }

        /// <summary>
        /// 新的行为方法
        /// </summary>
        public void AddSticker()
        {
            Console.WriteLine("现在苹果手机有贴膜了");
        }
    }

    public class accessories : Decorator
    {
        public accessories(Phone phone) : base(phone)
        {
        }

        public override void Print()
        {
            base.Print();

            // 添加新的行为
            AddAccessories();
        }

        /// <summary>
        /// 新的行为方法
        /// </summary>
        public void AddAccessories()
        {
            Console.WriteLine("现在苹果手机有漂亮的挂件了");
        }
    }
}

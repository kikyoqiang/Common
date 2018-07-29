using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console4
{
    public class Decorator : Phone
    {
        public Phone phone;
        public Decorator(Phone phone)
        {
            this.phone = phone;
        }
        public override void Print()
        {
            if (phone != null)
                phone.Print();
        }
    }
    public abstract class Phone
    {
        public abstract void Print();
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
        public void AddSticker()
        {
            Console.WriteLine("贴票");
        }
    }
    public class ApplePhone : Phone
    {
        public override void Print()
        {
            Console.WriteLine("苹果手机");
        }
    }
}

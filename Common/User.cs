using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class User : IUser
    {
        public void Speek()
        {
            Console.WriteLine("哈哈");
        }
    }
    public interface IUser
    {
        void Speek();
    }
}

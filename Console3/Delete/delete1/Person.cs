using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace Console3
{
    [Serializable]
    class Person
    {
        public void SomeMethod()
        {
            Console.WriteLine("current domain is:" + AppDomain.CurrentDomain.FriendlyName);
        }

        public string Name
        {
            get;
            set;
        }

        public int Age
        {
            get;
            set;
        }

        public override string ToString()
        {
            return this.Name + ":" + this.Age.ToString();
        }
    }
}

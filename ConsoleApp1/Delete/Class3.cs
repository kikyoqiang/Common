using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
 

namespace ConsoleApp1
{
    class Class3
    {
        static void Main22()
        {
            AppDomain exampleAppDomain = AppDomain.CreateDomain("new appdomain");
            Person xylee = (Person)exampleAppDomain.CreateInstanceAndUnwrap("ConsoleApp1", "ConsoleApp1.Person");

            Console.WriteLine("Type:{0}", xylee.GetType().ToString());
            Console.WriteLine("Is proxy:{0}", System.Runtime.Remoting.RemotingServices.IsTransparentProxy(xylee));
            xylee.SomeMethod();
            AppDomain.Unload(exampleAppDomain);

            try
            {
                xylee.SomeMethod();
                Console.WriteLine("call is successed");
            }
            catch (AppDomainUnloadedException)
            {
                Console.WriteLine("throw AppDomainUnloadException");
            }

            Console.ReadKey();
        }
    }
    public class Demo1 : Object
    {
        public Int32 Age;
    }

    public class Demo2 : MarshalByRefObject
    {
        public Int32 Age;
    }
}
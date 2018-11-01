using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CLR_Via
{
    //[assembly:CLSCOmpliant(true)]
    class Class3
    {
        static void Main22()
        {
            //int? n  = 9;
            //int result = ((IComparable)n).CompareTo(5);
            int a, b;
            ThreadPool.GetAvailableThreads(out a, out b);
            int c, d;
            ThreadPool.GetMaxThreads(out c, out d);
        }
        static void AA()
        {
            var a = CustomAttributeData.GetCustomAttributes(typeof(Class3));
            foreach (var item in a)
            {
                Type t = item.Constructor.DeclaringType;
                var b = item.ConstructorArguments;
            }
        }
    }
    [AttributeUsage(AttributeTargets.Enum, Inherited = false)]
    public class FlagsAttribute : Attribute
    {
        public FlagsAttribute() { }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class TastyAttribute : Attribute
    {

    }
    [Tasty]
    [Serializable]
    public class BaseType
    {
        [Tasty]
        protected virtual void DoSomething() { }
    }
    public class DerivedType : BaseType
    {

    }
    class AA
    {
        [DllImport("Kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetVersionEx();
        protected void AAA()
        {
            this.GetType().IsDefined(typeof(FlagsAttribute), false);
        }
    }
}

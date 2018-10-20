using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Console6
{
    class Class7
    {
        static void Main()
        {
            StringBuilder sb = new StringBuilder("Hello. My Name is Jeff.");
            int b = sb.Replace('.', '!').IndexOf('!');
            int a = StringBuilderExtensions.IndexOf(sb.Replace('.', '!'), '!');
            Console.ReadKey();
        }
        #region ////////////
        static void M()
        {
            //Random random = new Random();
            //for (int i = 0; i < 20; i++)
            //{
            //    //Console.WriteLine(random.Next());
            //    Console.WriteLine(Utility.GetRandom());
            //}

            //for (int i = 0; i < 20; i++)
            //{
            //    byte[] randomBytes = new byte[8];
            //    System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            //    rng.GetBytes(randomBytes);
            //    int result = BitConverter.ToInt32(randomBytes, 0);
            //    result = System.Math.Abs(result);
            //    Console.WriteLine(result);
            //}
            //Console.ReadKey();
            //object target = "jeffrey Richter";
            //object arg = "ff";

            //Type[] argTypes = new Type[] { arg.GetType() };
            //MethodInfo method = target.GetType().GetMethod("Contains", argTypes);

            //object[] arguments = new object[] { arg };
            //var b = method.Invoke(target, arguments).ToSafeString().ToBoolean();

            //dynamic  target2= "jeffrey Richter";
            //dynamic arg2 = "ff";
        }
        #endregion
    }
    public static partial class StringBuilderExtensions
    {
        public static int IndexOf(this StringBuilder sb, char value)
        {
            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i] == value)
                    return i;
            }
            return -1;
        }
    }
}
namespace CompanyA
{
    public class Phone
    {
        public void Dial()
        {
            Console.WriteLine("Phone Dial");
            EstablishConnection();
        }
        protected virtual void EstablishConnection()
        {
            Console.WriteLine("Phone.EstablishConnection");
        }
    }
}
namespace CompanyB
{
    public class BetterPhone : CompanyA.Phone
    {
        //public new void Dial()
        //{
        //    Console.WriteLine("BetterPhone.Dial");
        //    EstablishConnection();
        //    base.Dial();
        //}
        protected override void EstablishConnection()
        {
            Console.WriteLine("BetterPhone.EstablishConnection");
        }
    }
}

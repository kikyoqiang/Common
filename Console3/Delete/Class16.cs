using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Console3
{
    class Class16
    {
        static void Main222()
        {

            Console.ReadKey();
        }
    }
    public sealed class DataAccessFactory
    {
        private static readonly string assemblyPath = @"C:\Users\Kikyo\Desktop\临时\Common.dll"; //ConfigurationManager.AppSettings["AssemblyPath"];
        private static readonly string accessPath = @"C:\Users\Kikyo\Desktop\临时\Common.dll"; //ConfigurationManager.AppSettings["AccessPath"];

        public static IUser CreateUser()
        {
            string className = accessPath + ".User";
            className = "Common.User";
            return (IUser)Assembly.Load(assemblyPath).CreateInstance(className);
        }
    }
    public interface IUser
    {
        void Speek();
    }
}

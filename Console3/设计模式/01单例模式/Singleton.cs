using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console3
{
    public class Singleton
    {
        private static Singleton _singleton;

        private static object locker = new object();

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (_singleton == null)
                {
                    lock (locker)
                    {
                        if (_singleton == null)
                        {
                            _singleton = new Singleton();
                        }
                    }
                }
                return _singleton;
            }
        }

        public void Hello(string message)
        {
            Console.WriteLine(string.Format("啊 {0} 啊", message));
        }
    }
}

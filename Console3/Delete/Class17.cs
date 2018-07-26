using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console3
{
    class Class17
    {
        static void Main33()
        {
            string xmlString = ReadFile("2");
            DataCommunication da = new DataCommunication(xmlString);
             string resultString = da.Post();
            //Console.WriteLine(ReadFile2("MZ_GetMaxID"));
            Console.ReadKey();
        }
        private static string ReadFile(string s)
        {
            string path = string.Format(@"E:\0Project\IFM\华南\1\{0}.txt", s);
            string result = "";
            using (StreamReader reader = new StreamReader(path, Encoding.GetEncoding("gb2312")))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        private static string ReadFile2(string s)
        {
            string path = string.Format(@"E:\0Project\0FaBu\Test3\1\{0}.txt", s);
            string result = "";
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}

using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console6
{
    class Class3
    {
        static void Main22()
        {
            var a = new StreamWriter(path: "", append: true, encoding: Encoding.UTF8);
            //string s = File.ReadAllText(@"E:\0Project\南昌医院开发\Client\Bin\ErrorLog\ErrorLog_2018_08_31.log", Encoding.Default);
            //FileInfo类    其实跟FileStream类似，只有三个实例类OpenRead, OpenText, OpenWrite 都是一个意思
            //区别在于OpenRead 返回的是FileStream  OpenText返回的是StreamRead   OpenWrite返回的是FileStream  
            //可见FileInfo也可以操作字符数据
            FileInfo fi = new FileInfo("test.txt");
            string contents = string.Empty;
            using (FileStream fsRead = fi.OpenRead())
            {
                byte[] buffer = new byte[1024 * 1024 * 2];
                int r = fsRead.Read(buffer, 0, buffer.Length);
                contents = Encoding.UTF8.GetString(buffer, 0, r);
                contents = contents.Replace(" ", "");
                string[] str = contents.Split('\n');
            }
            using (StreamWriter sw = fi.CreateText())   //这里无法用字节存进去因为容易出现乱码，下次深入讨论这个问题
            {
                sw.Write(contents);
            }
        }
    }
}

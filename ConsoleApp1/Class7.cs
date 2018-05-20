using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class7
    {
        private const string testFile = @"E:\0Project\0FaBu\1.txt";
        private const int bufferSize = 1024;

        static void Main22()
        {
            if (File.Exists(testFile)) File.Delete(testFile);
            using (Stream stream = File.Create(testFile))
            {
                string content = "我是文件具体内容，我是不是帅得掉渣？";
                byte[] contentByte = Encoding.UTF8.GetBytes(content);
                stream.Write(contentByte, 0, contentByte.Length);
            }

            using (Stream stream = new FileStream(testFile, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.Asynchronous))
            {
                byte[] data = new byte[bufferSize];
                ReadFileClass rf = new ReadFileClass(stream, data);
                IAsyncResult result = stream.BeginRead(data, 0, data.Length, FinshCallBack, rf);
                //Thread.Sleep(3 * 1000);
                Console.WriteLine("主线程执行完毕，按回车键退出程序");
            }
            Console.ReadKey();
        }
        static void FinshCallBack(IAsyncResult result)
        {
            if (result.IsNull()) return;
            ReadFileClass rf = result.AsyncState as ReadFileClass;
            int length = rf.stream.EndRead(result);
            byte[] fileData = new byte[length];
            Array.Copy(rf.data, fileData, fileData.Length);
            string content = Encoding.UTF8.GetString(fileData);
            Thread.Sleep(3 * 1000);
            Console.WriteLine("读取文件结束：文件长度为[{0}]，文件内容为[{1}]", length.ToString(), content);
        }
    }
    public class ReadFileClass
    {
        public Stream stream;
        public byte[] data;
        public ReadFileClass(Stream stream, byte[] data)
        {
            this.stream = stream;
            this.data = data;
        }
    }
}

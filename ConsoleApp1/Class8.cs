using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class Class8
    {
        private const string testFile = @"C:\Users\Kikyo\Desktop\临时\1.txt";
        static void Main()
        {

            //string path = @"C:\Users\Kikyo\Desktop\临时\1.txt";
            //FileHelper.ReadFileAsync(testFile, CallBack);
            //string s = File.ReadAllText(path);
            //Console.WriteLine(s);

            CreateTestFile();
            Console.WriteLine("主线程权限测试：");
            JudgePermission(null);

            Console.WriteLine("子线程权限测试：");
            Thread subThread1 = new Thread(CreateTestFile);
            subThread1.Start();
            subThread1.Join();

            FileIOPermission fip = new FileIOPermission(FileIOPermissionAccess.AllAccess, testFile);
            fip.Deny();
            Console.WriteLine("已成功阻止文件访问");

            Console.WriteLine("主线程权限测试：");
            JudgePermission(null);

            Console.WriteLine("子线程权限测试：");
            Thread subThread2 = new Thread(CreateTestFile);
            subThread2.Start();
            subThread2.Join();

            SecurityPermission.RevertDeny();
            Console.WriteLine("已成功恢复文件访问");

            Console.WriteLine("主线程权限测试：");
            JudgePermission(null);

            Console.WriteLine("子线程权限测试：");
            Thread subThread3 = new Thread(CreateTestFile);
            subThread3.Start();
            subThread3.Join();

            Console.ReadKey();
        }

        static void CallBack(byte[] data)
        {
            string content = Encoding.ASCII.GetString(data);
            Console.WriteLine("读取文件结束：文件长度为[{0}]，文件内容为[{1}]", data.Length, content);
        }
        private static void CreateTestFile()
        {
            if (!File.Exists(testFile))
            {
                Stream stream = File.Create(testFile);
                stream.Dispose();
            }
        }
        private static void DeleteTestFile()
        {
            if (File.Exists(testFile))
            {
                File.Delete(testFile);
            }
        }
        // 尝试访问测试文件来测试安全上下文
        private static void JudgePermission(object state)
        {
            try
            {
                // 尝试访问文件
                File.GetCreationTime(testFile);
                // 如果没有异常则测试通过
                Console.WriteLine("权限测试通过");
            }
            catch (SecurityException)
            {
                // 如果出现异常则测试通过
                Console.WriteLine("权限测试没有通过");
            }
            finally
            {
                Console.WriteLine("------------------------");
            }
        }
    }
}

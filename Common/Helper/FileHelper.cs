using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public static class FileHelper
    {
        private static Action<byte[]> action;
        /// <summary>
        /// 异步读文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="callBack">回调返回读取的数据</param>
        /// <param name="bufferSize">缓冲区大小</param>
        public static void ReadFileAsync(string path, Action<byte[]> callBack, int bufferSize = 1024)
        {
            action = callBack;
            using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.Asynchronous))
            {
                byte[] data = new byte[bufferSize];
                ReadFileClass rfc = new ReadFileClass(stream, data);
                IAsyncResult result = stream.BeginRead(data, 0, data.Length, FinshCallBack, rfc);
            }
        }

        private static void FinshCallBack(IAsyncResult result)
        {
            if (result == null) return;
            ReadFileClass rfc = result.AsyncState as ReadFileClass;
            int length = rfc.stream.EndRead(result);
            byte[] fileData = new byte[length];
            Array.Copy(rfc.data, 0, fileData, 0, fileData.Length);
            if (action != null) action(fileData);
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
        /// <summary>
        /// 读取 文本文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ReadFile(string path, Encoding encoding = null)
        {
            string result = "";
            Encoding realEncoding = encoding ?? Encoding.UTF8;
            using (StreamReader reader = new StreamReader(path, realEncoding))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        public static void WriteFile(string fileName, string message, Encoding encoding = null)
        {
            Encoding realEncoding = encoding ?? Encoding.UTF8;
            using (TextWriter writer = new StreamWriter(fileName, true, realEncoding))
            {
                writer.Write(message);
            }
        }

        /// <summary> 读取文件为 Zip Base64 </summary>
        public static string FileToZipBase64(string path)
        {
            using (Stream sm = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var data = Utility.StreamToBytes(sm);
                return Utility.BytesToZipBase64(data);
            }
        }

        /// <summary> 保存 Zip Base64 到文件 </summary>
        public static void ZipBase64ToFile(string path, string zipBase64Data)
        {
            var bytes = Utility.ZipBase64ToBytes(zipBase64Data);
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
        }
    }
}

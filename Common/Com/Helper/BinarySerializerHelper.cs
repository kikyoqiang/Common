using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Common
{
    /// <summary>
    /// 序列化 反序列化
    /// </summary>
    public class BinarySerializerHelper
    {
        /// <summary>
        /// 将类型序列化为字符串
        /// </summary>
        public static string Serialize<T>(T data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
                return System.Text.Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// 将类型序列化为文件
        /// </summary>
        public static void SerializeToFile<T>(T data, string path)
        {
            if (!File.Exists(path))
                File.Create(path).Close();
            
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
                stream.Flush();
            }
        }

        /// <summary>
        /// 将字符串反序列化为类型
        /// </summary>
        public static TResult Deserialize<TResult>(string strData) where TResult : class
        {
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(strData);
            using (MemoryStream stream = new MemoryStream(bs))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(stream) as TResult;
            }
        }

        /// <summary>
        /// 将文件反序列化为类型
        /// </summary>
        public static TResult DeserializeFromFile<TResult>(string path) where TResult : class
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(stream) as TResult;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 序列化 反序列化
    /// </summary>
    public class BinarySerializerHelper
    {
        /// <summary>
        /// 将类型序列化为Json字符串
        /// </summary>
        public static string Serialize<T>(T data)
        {
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(data);
        }

        /// <summary>
        /// 将Json字符串反序列化为类型
        /// </summary>
        public static TResult Deserialize<TResult>(string strData) where TResult : class
        {
            return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<TResult>(strData);
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
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(stream, data);
                stream.Flush();
            }
        }
        
        /// <summary>
        /// 将文件反序列化为类型
        /// </summary>
        public static TResult DeserializeFromFile<TResult>(string path) where TResult : class
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return formatter.Deserialize(stream) as TResult;
            }
        }
    }
}

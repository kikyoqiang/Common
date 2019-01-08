using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Common
{
    public static class XmlHelper
    {
        #region XmlSerializeInternal
        private static void XmlSerializeInternal(Stream stream, object o, Encoding encoding)
        {
            if (o == null)
                throw new ArgumentNullException("o is null");
            if (encoding == null)
                throw new ArgumentNullException("encoding is null");

            XmlSerializer serializer = new XmlSerializer(o.GetType());

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = "\r\n";
            settings.Encoding = encoding;
            settings.IndentChars = "    ";

            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, o);
                writer.Close();
            }
        }
        #endregion

        #region 将一个对象序列化为XML字符串
        /// <summary>
        /// 将一个对象序列化为XML字符串
        /// </summary>
        /// <param name="o">要序列化的对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>序列化产生的XML字符串</returns>
        public static string XmlSerialize(object o, Encoding encoding)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializeInternal(stream, o, encoding);

                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        #endregion

        #region 将一个对象按XML序列化的方式写入到一个文件
        /// <summary>
        /// 将一个对象按XML序列化的方式写入到一个文件
        /// </summary>
        /// <param name="o">要序列化的对象</param>
        /// <param name="path">保存文件路径</param>
        /// <param name="encoding">编码方式</param>
        public static void XmlSerializeToFile(object o, string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                XmlSerializeInternal(file, o, encoding);
            }
        }
        #endregion

        #region 从XML字符串中反序列化对象
        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="s">包含对象的XML字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserialize<T>(string s, Encoding encoding)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentNullException("s");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(s)))
            {
                using (StreamReader sr = new StreamReader(ms, encoding))
                {
                    return (T)mySerializer.Deserialize(sr);
                }
            }
        }
        #endregion

        #region 读入一个文件，并按XML的方式反序列化对象。
        /// <summary>
        /// 读入一个文件，并按XML的方式反序列化对象。
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserializeFromFile<T>(string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            string xml = File.ReadAllText(path, encoding);
            return XmlDeserialize<T>(xml, encoding);
        }
        #endregion

        #region 从xml字符串读取DataSet
        /// <summary> 从xml字符串读取DataSet </summary>
        public static DataSet GetDataSet(string xmlString)
        {
            DataSet set = new DataSet();
            if (xmlString == null || xmlString.Length <= 0)
                return set;
            if (xmlString.StartsWith("<![CDATA["))
            {
                var temp1 = xmlString.Substring(xmlString.IndexOf("[") + 1);
                xmlString = temp1.Substring(temp1.IndexOf("[") + 1).TrimEnd('>').TrimEnd(']');
            }
            using (System.IO.StringReader reader = new System.IO.StringReader(xmlString))
            {
                set.ReadXml(reader);
            }
            return set;
        }
        #endregion

        #region 将DataSet转换为xml
        /// <summary> 将DataSet转换为xml </summary>
        public static string ConvertDataSetToXML(System.Data.DataSet xmlDS)
        {
            MemoryStream stream = null;
            System.Xml.XmlTextWriter writer = null;
            try
            {
                stream = new MemoryStream();
                writer = new System.Xml.XmlTextWriter(stream, Encoding.UTF8);       //从stream装载到XmlTextReader
                xmlDS.WriteXml(writer);     //用WriteXml方法写入文件.
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UTF8Encoding utf = new UTF8Encoding();
                return utf.GetString(arr).Trim().Replace("\n", "");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }
        #endregion
    }
}

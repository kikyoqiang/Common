using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary> 编码 帮助类 </summary> 
    public class SpecialCodeHelper
    {
        /// <summary> 将字符串 转换为 base64编码 默认UTF8</summary> 
        public static string ToBase64String(string value, Encoding encoding = null)
        {
            if (value.IsNullOrEmpty())
                return "";
            if (encoding == null)
                encoding = Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(value);
            string result = Convert.ToBase64String(bytes);
            return result;
        }
        /// <summary> 将base64编码 转换为 字符串 默认UTF8</summary> 
        public static string UnBase64String(string value, Encoding encoding = null)
        {
            if (value.IsNullOrEmpty())
                return "";
            if (encoding == null)
                encoding = Encoding.UTF8;
            byte[] bytes = Convert.FromBase64String(value);
            string result = encoding.GetString(bytes);
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 把object转换为其它类型的值，int bool double等
    /// </summary>
    public static partial class ObjTo
    {
        /// <summary>
        /// 把object转换为float，如果为null，则默认返回 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float ToFloat(object obj)
        {
            if (obj == null)
                return 0;
            float r = 0;
            float.TryParse(obj.ToString(), out r);
            return r;
        }
        /// <summary>
        /// 把object转换为decimal，如果为null，则默认返回 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object obj)
        {
            if (obj == null)
                return 0;
            decimal r = 0;
            Decimal.TryParse(obj.ToString(), out r);
            return r;
        }
        /// <summary>
        /// 把object转换为DateTime，如果为null，则默认返回 "1900/01/01 00:00:00"
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeNotNull(object obj)
        {
            if (obj == null)
                return new DateTime(1900, 1, 1);
            DateTime r = new DateTime();
            if (DateTime.TryParse(obj.ToString(), out r))
                return r;
            else
            {
                return r = new DateTime(1900, 1, 1);
            }
        }
        /// <summary>
        /// 把object转换为DateTime，如果为null，则返回 null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(object obj)
        {
            if (obj == null)
                return null;
            DateTime r = new DateTime();
            if (DateTime.TryParse(obj.ToString(), out r))
                return r;
            else
                return null;
        }
        /// <summary>
        /// 把object转换为Date日期，如果为null，则返回 null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? ToDate(object obj)
        {
            if (obj == null || obj.ToString() == "")
                return null;
            DateTime r = new DateTime();
            if (DateTime.TryParse(obj.ToString(), out r))
                return r.Date;
            else
                return null;

        }
        /// <summary>
        /// 把object转换为DateTime，如果为null，则返回 "1900/01/01 00:00:00"
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime1(object obj)
        {
            if (obj == null)
                return new DateTime(1900, 1, 1);
            DateTime r = new DateTime();
            if (DateTime.TryParse(obj.ToString(), out r))
                return r;
            else
                return new DateTime(1900, 1, 1);
        }
        /// <summary>
        /// 把object转换为bool，如果为null，则返回 false
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBool(object obj)
        {
            if (obj == null)
                return false;
            bool r = false;
            if (obj.ToString() == "1")
                obj = "true";
            else if (obj.ToString() == "0")
                obj = "false";
            Boolean.TryParse(obj.ToString(), out r);
            return r;
        }
        /// <summary>
        /// 把object转换为int，如果为null，则返回 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(object obj)
        {
            if (obj == null)
                return 0;
            int r = 0;
            int.TryParse(obj.ToString(), out r);
            return r;
        }
        /// <summary>
        /// 把object转换为string，如果为null，则返回 ""
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(object obj)
        {
            if (obj == null)
                return "";
            return Convert.ToString(obj);
        }

        /// <summary>
        /// 将bool类型的值，转换为数字1:0 ，如果为null，则返回 "0"
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToBoolToIntStr(object obj)
        {
            if (obj == null)
                return "0";
            bool r = false;
            Boolean.TryParse(obj.ToString(), out r);
            return r ? "1" : "0";
        }
    }
}

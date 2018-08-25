using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 用于扩展方法
    /// </summary>
    public static partial class ExtensionMethods
    {
        #region String扩展方法
        /// <summary>
        /// 判断string是否为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return s == null || s.Trim().Length == 0;
        }

        #region 判断string不为空
        /// <summary> 判断string不为空 </summary>
        public static bool IsNotEmpty(this string s)
        {
            return s.IsNullOrEmpty() == false;
        }
        #endregion

        /// <summary>
        /// 将字符串转换为Int数据类型并返回，如果转换不成功，则返回指定的替代值。
        /// </summary>
        /// <param name="text">要执行转换的字符串</param>
        /// <param name="replaceNumber">替代值</param>
        /// <returns>转换后的值或替代值</returns>
        public static int ToInt(this string text, int replaceNumber = 0)
        {
            int i = 0;
            if (!int.TryParse(text, out i))
                i = replaceNumber;
            return i;
        }

        public static long ToLong(this string text, long replaceNumber = 0)
        {
            long l = 0;
            if (!long.TryParse(text, out l))
                l = replaceNumber;
            return l;
        }

        /// <summary>
        /// 将字符串转换为Double数据类型并返回，如果转换不成功，则返回指定的替代值。
        /// </summary>
        /// <param name="text">要执行转换的字符串</param>
        /// <param name="replaceNumber">替代值</param>
        /// <returns>转换后的值或替代值</returns>
        public static Double ToDouble(this string text, Double replaceNumber = 0)
        {
            Double i = 0;
            if (!Double.TryParse(text, out i))
                i = replaceNumber;
            return i;
        }

        /// <summary>
        /// 将字符串转换为Decimal数据类型并返回，如果转换不成功，则返回指定的替代值。
        /// </summary>
        /// <param name="text">要执行转换的字符串</param>
        /// <param name="replaceNumber">替代值</param>
        /// <returns>转换后的值或替代值</returns>
        public static Decimal ToDecimal(this string text, Decimal replaceNumber = 0)
        {
            Decimal i = 0;
            if (!Decimal.TryParse(text, out i))
                i = replaceNumber;
            return i;
        }

        /// <summary>
        /// 将字符串分割为数组，且不返回空元素。
        /// 只需判断数组长度即可。
        /// </summary>
        /// <param name="text">将要执行分割的字符串.</param>
        /// <param name="separator">指定作为分割符.</param>
        /// <returns></returns>
        public static string[] ToArray(this string text, params char[] separator)
        {
            if (string.IsNullOrEmpty(text))
                return new string[0];
            string[] result = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return result;
        }

        /// <summary>
        /// 将数字转换为字符串，保留两位小数。形如：12.34
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDecimailString(this decimal value)
        {
            return value.ToString("0.00");
        }
        #endregion

        #region Object扩展方法
        /// <summary>
        /// 将Object转换为Int
        /// </summary>
        /// <param name="text"></param>
        /// <param name="replaceNumber">指定的替代值</param>
        /// <returns></returns>
        public static int ToInt(this object text, int replaceNumber = 0)
        {
            int i = 0;
            if (text != null)
                if (!int.TryParse(text.ToString(), out i))
                    i = replaceNumber;
            return i;
        }
        /// <summary>
        /// 将Object转换为long
        /// </summary>
        /// <param name="text"></param>
        /// <param name="replaceNumber">指定的替代值</param>
        /// <returns></returns>
        public static long ToLong(this object text, long replaceNumber = 0)
        {
            long l = 0;
            if (text != null)
                if (!long.TryParse(text.ToString(), out l))
                    l = replaceNumber;
            return l;
        }
        /// <summary>
        /// 将Object转换为double
        /// </summary>
        /// <param name="text"></param>
        /// <param name="replaceNumber">指定的替代值</param>
        /// <returns></returns>
        public static double ToDouble(this object text, double replaceNumber = 0)
        {
            double d = 0;
            if (text != null)
                if (!double.TryParse(text.ToString(), out d))
                    d = replaceNumber;
            return d;
        }
        /// <summary>
        /// 将Object转换为decimal
        /// </summary>
        /// <param name="text"></param>
        /// <param name="replaceNumber">指定的替代值</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object text, decimal replaceNumber = 0)
        {
            decimal d = 0;
            if (text != null)
                if (!decimal.TryParse(text.ToString(), out d))
                    d = replaceNumber;
            return d;
        }

        /// <summary>
        /// 判断object是否为null
        /// </summary>
        public static bool IsNull(this object text)
        {
            return text == null;
        }
        #endregion

        #region Extension
        /// <summary> 根据生日取得年龄 </summary>
        public static string GetAgeByBirthDate(this DateTime BirthDate)
        {
            DateTime DateTimeNow = DateTime.Now;

            int age = DateTimeNow.Year - BirthDate.Year;

            if (BirthDate > DateTimeNow.AddYears(-age))
                age--;

            return age.ToString();
        }

        /// <summary> 转换为安全字符串 </summary> 
        public static string ToSafeString(this object o)
        {
            string value = "";
            if (o != null && o != DBNull.Value)
                value = o.ToString();

            return value;
        }

        public static string ToSafeString(this DateTime d, string format)
        {
            if (d == null) return "";
            return d.ToString(format);
        }

        public static decimal ToDecimal(this string s)
        {
            decimal f = 0;

            if (s.Trim().Length > 0)
                decimal.TryParse(s, out f);
            return f;
        }

        public static float ToFloat(this string s)
        {
            float f = 0;

            if (s.Trim().Length > 0)
                float.TryParse(s, out f);
            return f;
        }

        public static int ToInt(this string s)
        {
            int f = 0;

            if (s != null)
            {
                if (s.Trim().Length > 0)
                    int.TryParse(s, out f);
            }

            return f;
        }

        public static bool ToBoolean(this string s)
        {
            bool result = false;

            if (s == "true" || s == "True" || s == "1" || s == "是")
                result = true;
            else if (s == "false" || s == "False" || s == "0" || s == "否")
                result = false;
            else
                bool.TryParse(s, out result);

            return result;
        }

        /// <summary> 补齐小数位 </summary> 
        public static string PadDigit(this string s, int digit)
        {
            string v = "0";
            string digitStr = ".0".PadRight(digit + 1, '0');
            if (s.Length == 0) return v + digitStr;

            string[] strs = s.Split('.');

            if (strs.Length == 1) return strs[0] + digitStr;

            if (strs.Length == 2)
            {
                v = s;
                if (strs[1].Length < digit)
                {
                    v = strs[0] + "." + strs[1].PadRight(digit, '0');
                }
            }
            return v;
        }

        public static string GetSplitValue(this string s, char[] splitChar, int pIndex)
        {
            if (s.IsNullOrEmpty()) return "";
            string[] strs = s.Split(splitChar);
            if (strs == null) return "";
            if (pIndex >= strs.Length) return "";
            return strs[pIndex];
        }

        public static string GetSplitValue(this string[] s, int pIndex)
        {
            if (s == null) return "";
            if (pIndex >= s.Length) return "";

            return s[pIndex];
        } 
        #endregion

        #region DataRow Extension
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static string GetStringValue(this DataRow row, string pColumnName)
        {
            return GetValue(row, pColumnName).ToSafeString();
        }

        /// <summary>
        /// 获取整型
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static int GetIntValue(this DataRow row, string pColumnName)
        {
            return GetStringValue(row, pColumnName).ToInt();
        }

        /// <summary>
        /// 获取浮点型
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static float GetFloatValue(this DataRow row, string pColumnName)
        {
            return GetStringValue(row, pColumnName).ToFloat();
        }

        /// <summary>
        /// 获取Boolean
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static bool GetBooleanValue(this DataRow row, string pColumnName)
        {
            string v = GetStringValue(row, pColumnName).ToLower();

            if (v == "1" || v == "true") return true;

            return false;
        }

        public static object GetValue(this DataRow row, string pColumnName)
        {
            if (row == null) return null;
            if (pColumnName.IsNullOrEmpty()) return null;
            if (!row.Table.Columns.Contains(pColumnName)) return null;

            return row[pColumnName];
        }
        #endregion

        #region DataTable Extension
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static string GetStringValue(this DataTable data, int rowIndex, string pColumnName)
        {
            return GetValue(data, rowIndex, pColumnName).ToSafeString();
        }

        /// <summary>
        /// 获取整型
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static int GetIntValue(this DataTable data, int rowIndex, string pColumnName)
        {
            return GetStringValue(data, rowIndex, pColumnName).ToInt();
        }

        /// <summary>
        /// 获取浮点型
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static float GetFloatValue(this DataTable data, int rowIndex, string pColumnName)
        {
            return GetStringValue(data, rowIndex, pColumnName).ToFloat();
        }

        /// <summary>
        /// 获取Boolean
        /// </summary>
        /// <param name="pColumnName">列名</param>
        /// <returns></returns>
        public static bool GetBooleanValue(this DataTable data, int rowIndex, string pColumnName)
        {
            string v = GetStringValue(data, rowIndex, pColumnName).ToLower();

            if (v == "1" || v == "true" || v == "True") return true;

            return false;
        }

        public static decimal GetDecimalValue(this DataTable data, int rowIndex, string pColumnName)
        {
            return GetStringValue(data, rowIndex, pColumnName).ToDecimal();
        }

        public static object GetValue(this DataTable data, int rowIndex, string pColumnName)
        {
            if (data == null || rowIndex < 0 || rowIndex >= data.Rows.Count) return null;

            return data.Rows[rowIndex].GetValue(pColumnName);
        }

        /// <summary>  判断DataTable是否为空 </summary>
        public static bool IsNullOrEmpty(this DataTable data)
        {
            return data == null || data.Rows.Count <= 0;
        }
        public static bool IsNotEmpty(this DataTable data)
        {
            return data.IsNullOrEmpty() == false;
        }
        #endregion

        #region DataSet Extension
        public static DataTable GetDataTable(this DataSet set, string tableName)
        {
            if (set.IsNullOrEmpty()) return new DataTable();
            if (!set.Tables.Contains(tableName)) return new DataTable();
            return set.Tables[tableName];
        }
        /// <summary> 判断DataSet 是否为空 </summary> 
        public static bool IsNullOrEmpty(this DataSet set)
        {
            return set == null || set.Tables.Count <= 0;
        }
        #endregion

        #region 得到本周第一天或最后一天
        #region 得到本周第一天(以星期天为第一天)
        /// <summary>
        /// 得到本周第一天(以星期天为第一天)
        /// </summary>
        public static DateTime GetWeekFirstDaySun(DateTime datetime)
        {
            //星期天为第一天
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            int daydiff = (-1) * weeknow;

            //本周第一天
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }
        #endregion

        #region 得到本周第一天(以星期一为第一天)
        /// <summary>
        /// 得到本周第一天(以星期一为第一天)
        /// </summary>
        public static DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }
        #endregion

        #region 得到本周最后一天(以星期六为最后一天)
        /// <summary>
        /// 得到本周最后一天(以星期六为最后一天)
        /// </summary>
        public static DateTime GetWeekLastDaySat(DateTime datetime)
        {
            //星期六为最后一天
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            int daydiff = (7 - weeknow) - 1;

            //本周最后一天
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }
        #endregion

        #region 得到本周最后一天(以星期天为最后一天)
        /// <summary>
        /// 得到本周最后一天(以星期天为最后一天)
        /// </summary>
        public static DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }
        #endregion 
        #endregion

        #region 将时间转换为 yyyy-MM-dd 格式

        /// <summary> 时间转换 失败返回1753-01-01</summary>
        public static string ToDateStr(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
        /// <summary> 时间转换 失败返回1753-01-01 </summary>
        public static string ToDateStr(this string dateTimeStr)
        {
            return dateTimeStr.ToDateTime().ToDateStr();
        }

        /// <summary> 时间转换 失败返回1753-01-01 00:00:00 </summary>
        public static string ToDateStrHH(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary> 时间转换 失败返回1753-01-01 00:00:00 </summary>
        public static string ToDateStrHH(this string dateTimeStr)
        {
            return dateTimeStr.ToDateTime().ToDateStrHH();
        }

        /// <summary> 给时间加上00:00:00.000 </summary>
        public static string AddStr00(this string dateStr)
        {
            return string.Format("{0} 00:00:00.000", dateStr);
        }
        /// <summary> 给时间加上23:59:59.997 </summary>
        public static string AddStr23(this string dateStr)
        {
            return string.Format("{0} 23:59:59.997", dateStr);
        }

        /// <summary> 日期转换 默认值1753-01-01 00:00:00 </summary>
        public static DateTime ToDateTime(this string dateTimeStr)
        {
            DateTime dateTime;
            if (!DateTime.TryParse(dateTimeStr, out dateTime))
                dateTime = DateTime.Parse("1753-01-01 00:00:00");
            return dateTime;
        }

        #endregion
    }
}

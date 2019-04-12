using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary> 常量类 </summary>
    public class Constant
    {
        /// <summary> 空Guid </summary>
        public const string GuidEmpty = "00000000-0000-0000-0000-000000000000";
        /// <summary> 数据库默认日期 </summary>
        public static DateTime DateTimeSql { get { return DateTime.Parse("1753-01-01 00:00:00"); } }
    }
}

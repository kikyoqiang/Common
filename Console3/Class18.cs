using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console3
{
    class Class18
    {
        static void Main()
        {
            var a = "";     
            //DataTable dt = new DataTable();
            //dt.Columns.Add("ID", typeof(string));
            //DataRow row = dt.NewRow();
            //row["ID"] = "1";
            //dt.Rows.Add(row);
            //DataRow row1 = dt.NewRow();
            //row1["ID"] = "2";
            //dt.Rows.Add(row1);
            //List<Sub1Info> list = new List<Sub1Info>();
            //list.Add(new Sub1Info("1"));
            //list.Add(new Sub1Info("3"));
            //var a = from m in dt.AsEnumerable()
            //        where list.AsEnumerable().Any(e => e.ID == m.Field<string>("ID").ToSafeString())
            //        select m;
            //var dt1 = a.CopyToDataTable();
        }
    }
    #region Sub1Info
    public class Sub1Info
    {
        public string ID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 包装序号
        /// </summary>
        public string PackSn { get; set; }
        /// <summary>
        /// 是否发送成功
        /// </summary>
        public string UseMark { get; set; }

        public Sub1Info(string id)
        {
            ID = id;
        }
    }
    #endregion
}

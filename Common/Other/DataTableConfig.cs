using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Common
{
    public class DataTableConfig
    {
        #region ready
        private readonly static string configFilePath = System.AppDomain.CurrentDomain.BaseDirectory + @"DataTableConfig.xml";

        private static Dictionary<string, List<DataTableConfigInfo>> _dic = new Dictionary<string, List<DataTableConfigInfo>>();
        public static Dictionary<string, List<DataTableConfigInfo>> Dic
        {
            get
            {
                return _dic;
            }
        }
        #endregion

        #region InitDataTableConfig
        public static bool InitDataTableConfig()
        {
            if (_dic != null && _dic.Count > 0)
                return true;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                if (!File.Exists(configFilePath))
                {
                    //MessageBox.Show("找不到文件\"TestItemsConfig.xml\", 请确认文件存在和路径正确。");
                    return false;
                }

                xmlDoc.Load(configFilePath);
                XmlNode xn = xmlDoc.SelectSingleNode("DataTable");
                XmlNodeList xnl = xn.ChildNodes;

                foreach (XmlNode xnc in xnl)
                {
                    XmlElement xe = (XmlElement)xnc;
                    string TableName = xe.GetAttribute("TableName");
                    List<DataTableConfigInfo> list = new List<DataTableConfigInfo>();
                    foreach (var xncc in xe.ChildNodes)
                    {
                        XmlElement xec = (XmlElement)xncc;
                        DataTableConfigInfo info = new DataTableConfigInfo();
                        info.OldColumn = xec.GetAttribute("OldColumn");
                        info.NewColumn = xec.GetAttribute("NewColumn");
                        list.Add(info);
                    }
                    _dic.Add(TableName, list);
                }
            }
            catch (System.Exception ex)
            {
                LogHelper.Instance.WriteError("加载配置文件出现异常:", ex);
                //MessageBox.Show(ex.Message.ToString());
                return false;
            }

            return true;
        }
        #endregion
    }

    public class DataTableConfigInfo
    {
        public string OldColumn { get; set; }
        public string NewColumn { get; set; }
    }

    public partial class DataTableHelper
    {
        #region FilterTable
        public static DataTable FilterTable(DataTable dt)
        {
            List<DataTableConfigInfo> list = GetDataTableConfig(dt);
            if (!list.All(e => dt.Columns.Contains(e.OldColumn)))
                return dt;
            string[] strs = list.Select(e => e.OldColumn).ToArray();
            dt = dt.DefaultView.ToTable(false, strs);
            return dt;
        }
        #endregion

        #region ConvertTable
        public static DataTable ConvertTable(DataTable dt)
        {
            List<DataTableConfigInfo> list = GetDataTableConfig(dt);
            foreach (DataTableConfigInfo info in list)
            {
                if (dt.Columns.Contains(info.OldColumn))
                    dt.Columns[info.OldColumn].ColumnName = info.NewColumn;
            }
            return dt;
        }
        #endregion

        #region GetDataTableConfig
        public static List<DataTableConfigInfo> GetDataTableConfig(DataTable dt)
        {
            List<DataTableConfigInfo> list = new List<DataTableConfigInfo>();
            if (dt == null || dt.Rows.Count <= 0 || dt.TableName.IsNullOrEmpty())
                return list;
            DataTableConfig.InitDataTableConfig();
            if (DataTableConfig.Dic.ContainsKey(dt.TableName))
                list = DataTableConfig.Dic[dt.TableName];
            return list;
        }
        #endregion
    }
}

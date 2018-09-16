using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Data;

namespace Console6
{
    class Class2
    {
        static void Main11()
        {
            Console6.ServiceReference1.PUB0018SoapClient p = new ServiceReference1.PUB0018SoapClient();
            string[] strs = new string[] { "2058022", "1454135", "1536138", "1617700", "1784938", "1796655", "2032540" };

            foreach (var item in strs)
            {
                string postStr3 = GetString_Advice(item, "2018-08-01", "2018-08-27");
                string result3 = p.HIPManagerInfo("S0085", postStr3);
                if (result3.StartsWith("-1"))
                    continue;
                Log("获取患者 医嘱 发送", postStr3);
                Log("获取患者 医嘱 返回", result3);
            }
            Console.ReadKey();
        }

        static string GetString_Advice(string PAADMVisitNumber, string StartDate, string EndDate)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<Request>");
            sb.AppendLine(@"  <Header>");
            sb.AppendLine(@"    <SourceSystem></SourceSystem>");
            sb.AppendLine(@"    <MessageID></MessageID>");
            sb.AppendLine(@"  </Header>");
            sb.AppendLine(@"  <Body>");
            sb.AppendFormat(@"    <PAADMVisitNumber>{0}</PAADMVisitNumber>", PAADMVisitNumber).AppendLine();
            sb.AppendFormat(@"    <StartDate>{0}</StartDate>", StartDate).AppendLine();
            sb.AppendFormat(@"    <EndDate>{0}</EndDate>", EndDate).AppendLine();
            sb.AppendLine(@"  </Body>");
            sb.AppendLine(@"</Request>");
            return sb.ToString();
        }
        public static void Log(string message, string content)
        {
            LogHelper.Instance.WriteDebug(message);
            LogHelper.Instance.WriteDebug(content);
        }
        private static string GetPAADMVisitNumber(string result)
        {
            if (result.IsNullOrEmpty() || result.StartsWith("-1"))
                return "";
            return "";
        }
        private static DataTable GetPAADMVisitTable(string result)
        {
            DataTable dt = new DataTable();
            DataSet set = XmlHelper.GetDataSet(result);
            if (set.IsNullOrEmpty())
                return dt;
            if (set.Tables.Contains("PAAdm"))
                dt = set.Tables["PAAdm"];
            return dt;
        }
    }
}

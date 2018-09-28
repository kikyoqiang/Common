using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Console5
{
    class Program
    {
        static void Main(string[] args)
        {
            string StartDate = DateTime.Now.ToString("yyyy-MM-dd");
            string EndDate = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            Console5.WebReference.PUB0018 p = new WebReference.PUB0018();

            var a = p.HIPManagerInfo("S0083", "<PATPatientID>0000806453</PATPatientID>");//000001  0000806453
            var ar = XmlHelper.GetDataSet(a);

            var bb = GetString_Advice("1929106", "2018-08-08", "2018-08-08");
            var b = p.HIPManagerInfo("S0085", bb);
            var br= XmlHelper.GetDataSet(b);
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
    }
}

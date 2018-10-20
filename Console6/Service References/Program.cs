using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Data;
using System.Xml;

namespace Console6
{
    class Program
    {
        static void Main22(string[] args)
        {
            Console6.ServiceReference1.PUB0018SoapClient p = new ServiceReference1.PUB0018SoapClient();
            string PATPatientID = "0001143418";
            string PAADMVisitNumber = "2066900";

            string postString = GetPostString_PatientInfo(PATPatientID);
            string result = p.HIPManagerInfo("S0083", postString);
            //Log("获取患者 基本信息 发送", postString);
            //Log("获取患者 基本信息 返回", result);

            string postString2 = GetPostString_PatientInfo(PATPatientID);
            string result2 = p.HIPManagerInfo("S0084", postString2);
            //Log("获取患者 就诊信息 发送", postString2);
            //Log("获取患者 就诊信息 返回", result2);


            string[] strs = new string[] { "2068764" };

            foreach (var item in strs)
            {
                string postStr33 = GetString_Advice(item, "2018-08-27", "2018-08-28");
                string result33 = p.HIPManagerInfo("S0085", postStr33);
                if (result33.StartsWith("-1"))
                    continue;
                Log("获取患者 医嘱 发送", postStr33);
                Log("获取患者 医嘱 返回", result33);
            }

            string postStr3 = GetString_Advice(PAADMVisitNumber, "2018-08-01", "2018-08-27");
            string result3 = p.HIPManagerInfo("S0085", postStr3);
            //Log("获取患者 医嘱 发送", postStr3);
            //Log("获取患者 医嘱 返回", result3);

            //===========================================================================================
            
            string PATPatientID2 = "00001143418";
            string PAADMVisitNumber2 = "1722839";//1722839 2066900

            ServiceReference2.PUB0001SoapClient p2 = new ServiceReference2.PUB0001SoapClient();

            //01:检验结果, 02:超声检查结果, 04:内镜检查结果, 07:PET-CT检查结果, 08:ECT检查结果 15:微生物结果, 16:心电检查结果
            string postString11 = GetPostString_DocList(PATPatientID2, PAADMVisitNumber2, "99", "2015-01-01", "2018-08-31", "xml");//S0034
            var result11 = p2.HOSDocumentRetrieval("S0034", postString11);
            Log("文档列表 发送", postString11);
            Log("文档列表 返回", result11);

            string DocumentID = "2042111";
            string DocumentType2 = "01";
            string postString22 = GetPostString_DocDetail(PATPatientID2, PAADMVisitNumber2, DocumentType2, DocumentID);
            string result22 = p2.HOSDocumentSearch("S0035", postString22);
            Log("文档详细 发送", postString22);
            Log("文档详细 返回", result22);

            DataSet set = XmlHelper.GetDataSet(result22);

            Console.ReadKey();
        }
        static string GetPostString_PatientInfo(string PATPatientID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<Request>");
            sb.AppendLine(@"  <Header>");
            sb.AppendLine(@"    <SourceSystem></SourceSystem>");
            sb.AppendLine(@"    <MessageID></MessageID>");
            sb.AppendLine(@"  </Header>");
            sb.AppendLine(@"  <Body>");
            sb.AppendFormat(@"    <PATPatientID>{0}</PATPatientID>", PATPatientID).AppendLine();
            sb.AppendLine(@"  </Body>");
            sb.AppendLine(@"</Request>");
            return sb.ToSafeString();
        }

        static string GetPostString_MedicalInformation(string PATPatientID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<Request>");
            sb.AppendLine(@"  <Header>");
            sb.AppendLine(@"    <SourceSystem></SourceSystem>");
            sb.AppendLine(@"    <MessageID></MessageID>");
            sb.AppendLine(@"  </Header>");
            sb.AppendLine(@"  <Body>");
            sb.AppendFormat(@"    <PATPatientID>{0}</PATPatientID>", PATPatientID).AppendLine();
            sb.AppendLine(@"  </Body>");
            sb.AppendLine(@"</Request>");
            return sb.ToSafeString();
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
        static string GetPostString_DocList(string PATPatientID, string PAADMVisitNumber, string DocumentType, string StartDate, string EndDate, string DocumentFormat)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<Request>");
            sb.AppendLine(@"  <Header>");
            sb.AppendLine(@"    <SourceSystem></SourceSystem>");
            sb.AppendLine(@"    <MessageID></MessageID>");
            sb.AppendLine(@"  </Header>");
            sb.AppendLine(@"  <Body>");
            sb.AppendLine(@"    <DocumentRetrievalRt>");
            sb.AppendLine(@"      <BusinessFieldCode>00001</BusinessFieldCode>");
            sb.AppendLine(@"      <HospitalCode>NCDXDEFSYY</HospitalCode>");
            sb.AppendFormat(@"      <PATPatientID>{0}</PATPatientID>", PATPatientID).AppendLine();
            sb.AppendFormat(@"      <PAADMVisitNumber>{0}</PAADMVisitNumber>", PAADMVisitNumber).AppendLine();
            sb.AppendFormat(@"      <DocumentType>{0}</DocumentType>", DocumentType).AppendLine();
            sb.AppendFormat(@"      <StartDate>{0}</StartDate>", StartDate).AppendLine();
            sb.AppendFormat(@"      <EndDate>{0}</EndDate>", EndDate).AppendLine();
            sb.AppendFormat(@"      <DocumentFormat>{0}</DocumentFormat>", DocumentFormat).AppendLine();
            sb.AppendLine(@"    </DocumentRetrievalRt>");
            sb.AppendLine(@"  </Body>");
            sb.AppendLine(@"</Request>");
            return sb.ToSafeString();
        }
        static string GetPostString_DocDetail(string PATPatientID, string PAADMVisitNumber, string DocumentType, string DocumentID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<Request>");
            sb.AppendLine(@"  <Header>");
            sb.AppendLine(@"    <SourceSystem></SourceSystem>");
            sb.AppendLine(@"    <MessageID></MessageID>");
            sb.AppendLine(@"  </Header>");
            sb.AppendLine(@"  <Body>");
            sb.AppendLine(@"    <DocumentSearchRt>");
            sb.AppendLine(@"      <BusinessFieldCode>00001</BusinessFieldCode>");
            sb.AppendLine(@"      <HospitalCode>NCDXDEFSYY</HospitalCode>");
            sb.AppendFormat(@"      <PATPatientID>{0}</PATPatientID>", PATPatientID).AppendLine();
            sb.AppendFormat(@"      <PAADMVisitNumber>{0}</PAADMVisitNumber>", PAADMVisitNumber).AppendLine();
            sb.AppendFormat(@"      <DocumentType>{0}</DocumentType>", DocumentType).AppendLine();
            sb.AppendFormat(@"      <DocumentID>{0}</DocumentID>", DocumentID).AppendLine();
            sb.AppendLine(@"    </DocumentSearchRt>");
            sb.AppendLine(@"  </Body>");
            sb.AppendLine(@"</Request>");
            return sb.ToSafeString();
        }
        public static string ReadFile(string path, Encoding encoding)
        {
            string result = "";
            using (System.IO.StreamReader reader = new System.IO.StreamReader(path, encoding))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        public static void Log(string message, string content)
        {
            LogHelper.Instance.WriteDebug(message);
            LogHelper.Instance.WriteDebug(content);
        }
        static string GetMessage(string resultXml)
        {
            if (resultXml.IsNullOrEmpty() || resultXml.StartsWith("-1") || !resultXml.Contains(@"<ResultCode>-1</ResultCode>"))
                return "";
            DataSet set2 = XmlHelper.GetDataSet(resultXml);
            string message = set2.Tables["Body"].Rows[0]["ResultContent"].ToSafeString();
            return message;
        }
    }
}

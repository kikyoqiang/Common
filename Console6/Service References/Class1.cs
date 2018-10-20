using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console6
{
    class Class1
    {
        static void Main11()
        {
           
            ServiceReference2.PUB0001SoapClient p = new ServiceReference2.PUB0001SoapClient();
            string PATPatientID = "0001595716";
            string PAADMVisitNumber = "1796655";

            //01:检验结果, 02:超声检查结果, 04:内镜检查结果, 07:PET-CT检查结果,
            //08:ECT检查结果 15:微生物结果, 16:心电检查结果
            string postString = GetPostString_DocList(PATPatientID, PAADMVisitNumber, "01", "2018-07-01", "2018-08-27", "xml");//S0034
            var result = p.HOSDocumentRetrieval("S0034", postString);

            string DocumentID = "";
            string DocumentType2 = "01";
            string postString2 = GetPostString_DocDetail(PATPatientID, PAADMVisitNumber, DocumentType2, DocumentID);
            string result2 = p.HOSDocumentSearch("S0035", postString2);

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
    }
}

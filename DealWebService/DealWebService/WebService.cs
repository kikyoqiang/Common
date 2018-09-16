using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealWebService
{
    public class WebService
    {
        private object[] inparms;
        private object outparms = new object();
        private string strType = "";

        public WebService(string type, object[] parms)
        {
            strType = type;
            InParms = parms;
        }

        public object[] InParms
        {
            set { inparms = value; }
            get { return inparms; }
        }

        public object OutParms
        {
            set { outparms = value; }
            get { return outparms; }
        }

        public object GetResult()
        {
            switch (strType)
            {
                case "BYSY_New":
                    BYSY_New.HdrQueryDataService service = new BYSY_New.HdrQueryDataService();
                    OutParms = service.queryData((string)InParms[0]);
                    break;

                case "HBHD_CA_UploadDoc":
                    HBHD_CA.DocumentTransferSoapClient HDCA_UploadDoc = new HBHD_CA.DocumentTransferSoapClient();
                    OutParms = HDCA_UploadDoc.UploadDoc((string)InParms[0]);
                    HDCA_UploadDoc.Close();
                    HDCA_UploadDoc.Abort();
                    break;

                case "HBHD_CA_RemoveDoc":
                    HBHD_CA.DocumentTransferSoapClient HDCA_RemoveDoc = new HBHD_CA.DocumentTransferSoapClient();
                    OutParms = HDCA_RemoveDoc.RemoveDoc((string)InParms[0]);
                    HDCA_RemoveDoc.Close();
                    HDCA_RemoveDoc.Abort();
                    break;

                case "NanChangHisService":
                    NanChangHis.PUB0018SoapClient PUB0018 = new NanChangHis.PUB0018SoapClient();
                    OutParms = PUB0018.HIPManagerInfo((string)InParms[0], (string)InParms[1]);
                    PUB0018.Close();
                    PUB0018.Abort();
                    break;

                case "NanChangHOSDocumentRetrieval":
                    NanChangLis.PUB0001SoapClient PUB0001 = new NanChangLis.PUB0001SoapClient();
                    OutParms = PUB0001.HOSDocumentRetrieval((string)InParms[0], (string)InParms[1]);
                    PUB0001.Close();
                    PUB0001.Abort();
                    break;

                case "NanChangHOSDocumentSearch":
                    NanChangLis.PUB0001SoapClient pUB0001 = new NanChangLis.PUB0001SoapClient();
                    OutParms = pUB0001.HOSDocumentSearch((string)InParms[0], (string)InParms[1]);
                    pUB0001.Close();
                    pUB0001.Abort();
                    break;

                default:
                    break;
            }
            return OutParms;
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
    }
}

using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Common
{
    /// <summary>
    ///  利用WebRequest/WebResponse进行WebService调用的类
    /// </summary>
    public class WebServiceHelper
    {
        #region Tip:使用说明
        //webServices 应该支持Get和Post调用，在web.config应该增加以下代码
        //<webServices>
        //  <protocols>
        //    <add name="HttpGet"/>
        //    <add name="HttpPost"/>
        //  </protocols>
        //</webServices>

        //调用示例：
        //Hashtable ht = new Hashtable();  //Hashtable 为webservice所需要的参数集
        //ht.Add("str", "test");
        //ht.Add("b", "true");
        //XmlDocument xx = WebSvcCaller.QuerySoapWebService("http://localhost:81/service.asmx", "HelloWorld", ht);
        //MessageBox.Show(xx.OuterXml);
        #endregion

        #region Ready
        private static Hashtable _xmlNamespaces = new Hashtable();      //缓存xmlNamespace，避免重复调用GetNamespace 
        #endregion

        #region Get 请求WebService
        /// <summary>
        /// Get 请求WebService
        /// </summary>
        public static string GetString(string URL, string MethodName, Hashtable Pars = null)
        {
            if (Pars != null && Pars.Keys.Count > 0)
                URL = string.Format("{0}/{1}?{2}", URL, MethodName, ParsToString(Pars));
            else
                URL = string.Format("{0}/{1}", URL, MethodName);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "Get";
            request.ContentType = "application/x-www-form-urlencoded";
            SetWebRequest(request);

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                //response = (HttpWebResponse)ex.Response;
                var message = ReadStringResponse(ex.Response);
                throw new Exception(message);
            }
            return ReadStringResponse(response);
        }
        #endregion

        #region Post 请求WebService
        /// <summary>
        /// Post 请求WebService
        /// </summary>
        public static string PostWebService(string url, string MethodName, Hashtable Pars)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "/" + MethodName);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            SetWebRequest(request);
            byte[] data = EncodePars(Pars);
            WriteRequestData(request, data);
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                //response = (HttpWebResponse)ex.Response;
                var message = ReadStringResponse(ex.Response);
                throw new Exception(message);
            }
            return ReadXml(response);
        }

        /// <summary>
        /// Post 请求WebService
        /// </summary>
        public static string PostString(string url, string postString)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            SetWebRequest(request);
            byte[] data = Encoding.UTF8.GetBytes(postString);
            WriteRequestData(request, data);
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                //response = (HttpWebResponse)ex.Response;
                var message = ReadStringResponse(ex.Response);
                throw new Exception(message);
            }
            return ReadXml(response);
        }
        #endregion

        #region Post 请求
        /// <summary>
        /// Post 请求
        /// </summary>
        public static string PostJson(string url, Hashtable Pars)
        {
            byte[] data = EncodePars(Pars);
            string requestMethod = "POST";
            string contentType = "application/json";
            var res = Post(url, data, ReadStringResponse, requestMethod, contentType);
            return res;
        }
        #endregion

        #region Post
        private static string Post(string url, byte[] data, Func<WebResponse, string> func, string requestMethod, string contentType)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = requestMethod;
            request.ContentType = contentType;
            SetWebRequest(request);
            WriteRequestData(request, data);
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                var message = ReadStringResponse(ex.Response);
                throw new Exception(message);
            }
            return func(response);
        }
        #endregion

        #region 通用WebService调用(Soap),参数Pars为string类型的参数名、参数值
        /// <summary>
        /// 通用WebService调用(Soap),参数Pars为string类型的参数名、参数值
        /// </summary>
        public static XmlDocument QuerySoapWebService(string URL, string MethodName, Hashtable Pars)
        {
            if (_xmlNamespaces.ContainsKey(URL))
            {
                return QuerySoapWebService(URL, MethodName, Pars, _xmlNamespaces[URL].ToString());
            }
            else
            {
                return QuerySoapWebService(URL, MethodName, Pars, GetNamespace(URL));
            }
        }

        private static XmlDocument QuerySoapWebService(string URL, string MethodName, Hashtable Pars, string XmlNs)
        {
            _xmlNamespaces[URL] = XmlNs;//加入缓存，提高效率
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "text/xml; charset=utf-8";
            request.Headers.Add("SOAPAction", "\"" + XmlNs + (XmlNs.EndsWith("/") ? "" : "/") + MethodName + "\"");
            SetWebRequest(request);
            byte[] data = EncodeParsToSoap(Pars, XmlNs, MethodName);
            WriteRequestData(request, data);
            XmlDocument doc = new XmlDocument(), doc2 = new XmlDocument();
            doc = ReadXmlResponse(request.GetResponse());

            XmlNamespaceManager mgr = new XmlNamespaceManager(doc.NameTable);
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            string RetXml = doc.SelectSingleNode("//soap:Body/*/*", mgr).InnerXml;
            doc2.LoadXml("<root>" + RetXml + "</root>");
            AddDelaration(doc2);
            return doc2;
        }
        #endregion

        #region 通用
        private static string GetNamespace(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + "?WSDL");
            SetWebRequest(request);
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sr.ReadToEnd());
            sr.Close();
            return doc.SelectSingleNode("//@targetNamespace").Value;
        }

        private static byte[] EncodeParsToSoap(Hashtable Pars, string XmlNs, string MethodName)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"></soap:Envelope>");
            AddDelaration(doc);
            //XmlElement soapBody = doc.createElement_x_x("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            XmlElement soapBody = doc.CreateElement("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            //XmlElement soapMethod = doc.createElement_x_x(MethodName);
            XmlElement soapMethod = doc.CreateElement(MethodName);
            soapMethod.SetAttribute("xmlns", XmlNs);
            foreach (string k in Pars.Keys)
            {
                //XmlElement soapPar = doc.createElement_x_x(k);
                XmlElement soapPar = doc.CreateElement(k);
                soapPar.InnerXml = ObjectToSoapXml(Pars[k]);
                soapMethod.AppendChild(soapPar);
            }
            soapBody.AppendChild(soapMethod);
            doc.DocumentElement.AppendChild(soapBody);
            return Encoding.UTF8.GetBytes(doc.OuterXml);
        }
        private static string ObjectToSoapXml(object o)
        {
            XmlSerializer mySerializer = new XmlSerializer(o.GetType());
            MemoryStream ms = new MemoryStream();
            mySerializer.Serialize(ms, o);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Encoding.UTF8.GetString(ms.ToArray()));
            if (doc.DocumentElement != null)
            {
                return doc.DocumentElement.InnerXml;
            }
            else
            {
                return o.ToString();
            }
        }

        /// <summary>
        /// 设置凭证与超时时间
        /// </summary>
        /// <param name="request"></param>
        private static void SetWebRequest(HttpWebRequest request)
        {
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 10000;
        }

        private static void WriteRequestData(HttpWebRequest request, byte[] data)
        {
            request.ContentLength = data.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();
        }

        private static byte[] EncodePars(Hashtable Pars, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetBytes(ParsToString(Pars));
        }

        private static string ParsToString(Hashtable Pars)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (var key in Pars.Keys)
            {
                if (i > 0)
                    sb.Append("&");
                sb.AppendFormat("{0}={1}", key, Pars[key]);
                i++;
                //sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
            }
            return sb.ToString();
        }

        private static string ReadStringResponse(WebResponse response, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding))
            {
                return sr.ReadToEnd();
            }
        }

        private static string ReadStringResponse(WebResponse response)
        {
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }

        private static string ReadXml(WebResponse response)
        {
            using (XmlTextReader Reader = new XmlTextReader(response.GetResponseStream()))
            {
                Reader.MoveToContent();
                var data = Reader.ReadInnerXml();
                var res = data.Replace("&lt;", "<").Replace("&gt;", ">");
                return res;
            }
        }

        private static XmlDocument ReadXmlResponse(WebResponse response)
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string retXml = reader.ReadToEnd();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(retXml);
                return doc;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private static void AddDelaration(XmlDocument doc)
        {
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.InsertBefore(decl, doc.DocumentElement);
        }
        #endregion
    }
}

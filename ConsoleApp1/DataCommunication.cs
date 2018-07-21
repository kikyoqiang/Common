using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Console3
{
    /// <summary>
    /// 清远数据对接HTTTP请求类，返回string
    /// </summary>
    public class DataCommunication
    {
        private string sentence = "";
        //private string strUrl = SystemConfig.GetConfig("Posturl");
        private string strUrl = @"http://127.0.0.1:8082";
        public DataCommunication(string postString)
        {
            sentence = postString;
        }
        /// <summary>
        /// postString的HTTP请求返回string
        /// </summary>
        /// <returns></returns>
        public string Post()
        {
            // Encoding encoding = Encoding.GetEncoding("gb2312");
            Encoding encoding = Encoding.UTF8;
            Stream postStream = null;
            Stream receiveStream = null;
            StreamReader sr = null;
            string url = strUrl;
            WebRequest request = null;
            WebResponse response = null;
            string content = string.Empty;
            try
            {
                // 准备请求,设置参数  
                if (!ToggleAllowUnsafeHeaderParsing(true))
                {
                    // Couldn't change flag. Log the fact, throw an exception or whatever.
                    LogHelper.Instance.WriteError("===ToggleAllowUnsafeHeaderParsing False===");
                }
                request = WebRequest.Create(url) as HttpWebRequest;

                request.Method = "POST";
                request.ContentType = "application/xml;charset=gb2312";
                byte[] data = encoding.GetBytes(sentence);
                request.ContentLength = data.Length;
                postStream = request.GetRequestStream();
                postStream.Write(data, 0, data.Length);
                postStream.Flush();
                postStream.Close();

                response = request.GetResponse() as HttpWebResponse;//发送请求并获取相应回应数据  
                                                                    ////直到request.GetResponse()程序才开始向目标网页发送Post请求  
                receiveStream = response.GetResponseStream();
                sr = new StreamReader(receiveStream, encoding);
                ////返回结果网页(html)代码  
                content = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError("清远数据对接HTTTP请求异常", ex);
                //MessageManager.ShowErrorMsg("清远数据对接HTTTP请求异常", "错误");
            }
            finally
            {
                if (postStream != null) { postStream.Close(); }
                if (receiveStream != null) { receiveStream.Close(); }
                if (sr != null) { sr.Close(); }
                if (request != null) { request.Abort(); }
                if (response != null) { response.Close(); }
            }
            return content;
        }
        private static bool ToggleAllowUnsafeHeaderParsing(bool enable)
        {
            //Get the assembly that contains the internal class
            Assembly assembly = Assembly.GetAssembly(typeof(SettingsSection));
            if (assembly != null)
            {
                //Use the assembly in order to get the internal type for the internal class
                Type settingsSectionType = assembly.GetType("System.Net.Configuration.SettingsSectionInternal");
                if (settingsSectionType != null)
                {
                    //Use the internal static property to get an instance of the internal settings class.
                    //If the static instance isn't created already invoking the property will create it for us.
                    object anInstance = settingsSectionType.InvokeMember("Section",
                    BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.NonPublic, null, null, new object[] { });
                    if (anInstance != null)
                    {
                        //Locate the private bool field that tells the framework if unsafe header parsing is allowed
                        FieldInfo aUseUnsafeHeaderParsing = settingsSectionType.GetField("useUnsafeHeaderParsing", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (aUseUnsafeHeaderParsing != null)
                        {
                            aUseUnsafeHeaderParsing.SetValue(anInstance, enable);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static string CreateXmlFromModel<T>(T model) where T : class
        {
            PropertyInfo[] Properties = model.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<DataTable>");
            foreach (PropertyInfo p in Properties)
            {
                sb.AppendFormat(@"<{0}>{1}</{2}>", p.Name, p.GetValue(model, null).ToSafeString(), p.Name).AppendLine();
            }
            sb.Append(@"</DataTable>");
            return sb.ToSafeString();
        }
    }
}

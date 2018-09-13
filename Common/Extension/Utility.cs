using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Services.Description;

namespace System
{
    public class Utility
    {
        #region 变量
        /// <summary>种子日期（自动排班用）</summary> 
        private static DateTime sourse = DateTime.Parse("2018-01-01 00:00:00");
        #endregion

        /// <summary> 检测WebService是否可用 </summary> 
        #region IsWebServiceAvaiable
        public static bool IsWebServiceAvaiable(string url)
        {
            bool bRet = false;
            HttpWebRequest myHttpWebRequest = null;
            try
            {
                System.GC.Collect();

                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                myHttpWebRequest.Timeout = 60000;
                myHttpWebRequest.KeepAlive = false;
                myHttpWebRequest.ServicePoint.ConnectionLimit = 200;
                using (HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse())
                {
                    bRet = true;
                    if (myHttpWebResponse != null)
                    {
                        myHttpWebResponse.Close();
                    }
                }
            }
            catch (WebException e)
            {
                Common.LogHelper.Instance.WriteError(String.Format("网络连接错误 : {0}", e.Message));
            }
            catch (Exception ex)
            {
                Common.LogHelper.Instance.WriteError(ex.Message + "|" + ex.StackTrace);
            }
            finally
            {
                if (myHttpWebRequest != null)
                {
                    myHttpWebRequest.Abort();
                    myHttpWebRequest = null;
                }
            }

            return bRet;
        }
        #endregion

        #region InvokeWebService
        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            try
            {
                string _namespace = "WebService";
                if (string.IsNullOrEmpty(classname))
                {
                    classname = GetClassName(url);
                }
                //获取服务描述语言(WSDL)
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url + "?WSDL");//【1】
                ServiceDescription sd = ServiceDescription.Read(stream);//【2】
                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();//【3】
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(_namespace);//【4】
                //生成客户端代理类代码
                CodeCompileUnit ccu = new CodeCompileUnit();//【5】
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                //CSharpCodeProvider csc = new CSharpCodeProvider();//【6】
                CodeDomProvider csc = CodeDomProvider.CreateProvider("CSharp");
                //ICodeCompiler icc = csc.CreateCompiler();//【7】

                //设定编译器的参数
                CompilerParameters cplist = new CompilerParameters();//【8】
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");
                //编译代理类
                CompilerResults cr = csc.CompileAssemblyFromDom(cplist, ccu);//【9】
                if (true == cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new StringBuilder();
                    foreach (CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }

                //生成代理实例,并调用方法
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(_namespace + "." + classname, true, true);
                object bj = Activator.CreateInstance(t);//【10】
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);//【11】
                return mi.Invoke(bj, args);
            }
            catch (System.Exception ex)
            {
                Common.LogHelper.Instance.WriteError(ex.Message + "|" + ex.StackTrace);
                //MessageManager.ShowErrorMsg(ex.Message.ToString(), "test");
                return null;
            }
        }
        #endregion

        #region GetClassName
        private static string GetClassName(string url)
        {
            //假如URL为"http://localhost/InvokeService/Service1.asmx"
            //最终的返回值为 Service1
            string[] parts = url.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }
        #endregion

        /// <summary> GetMD5FromFile </summary>
        #region GetMD5FromFile
        public static string GetMD5FromFile(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] md5byte = md5.ComputeHash(fs);
            int i, j;
            string md5Str = string.Empty;
            foreach (byte b in md5byte)
            {
                i = Convert.ToInt32(b);
                j = i >> 4;
                md5Str += Convert.ToString(j, 16);

                j = ((i << 4) & 0x00ff) >> 4;

                md5Str += Convert.ToString(j, 16);
            }

            fs.Close();
            fs.Dispose();

            return md5Str;
        }
        #endregion

        /// <summary> GetMD5FromFileStream </summary>
        #region GetMD5FromFileStream
        public static string GetMD5FromFileStream(byte[] buffer)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] md5byte = md5.ComputeHash(buffer);
            int i, j;
            string md5Str = string.Empty;
            foreach (byte b in md5byte)
            {
                i = Convert.ToInt32(b);
                j = i >> 4;
                md5Str += Convert.ToString(j, 16);

                j = ((i << 4) & 0x00ff) >> 4;

                md5Str += Convert.ToString(j, 16);
            }

            return md5Str;
        }
        #endregion

        /// <summary> 根据时间 获取周几Int </summary>
        #region GetWeekInt
        public static int GetWeekInt(string dateTimeStr)
        {
            DateTime dateTime = DateTime.Now;
            if (DateTime.TryParse(dateTimeStr, out dateTime))
            {
                return GetWeekInt(dateTime);
            }
            return -1;
        }
        #endregion

        /// <summary> 根据时间 获取周几Int </summary>
        #region GetWeekInt
        public static int GetWeekInt(DateTime dateTime)
        {
            int week = -1;
            string weekStr = dateTime.DayOfWeek.ToString("d");
            week = Convert.ToInt32(weekStr);
            if (weekStr == "0")
                week = 7;
            return week;
        }
        #endregion

        /// <summary>获得目标日期 是单周 还是双周 根据种子日期</summary> 
        #region GetWeekByDate
        public static string GetWeekByDate(DateTime Target)
        {
            string result = string.Empty;
            TimeSpan span = Target.Subtract(sourse);
            if (span.Days % 14 < 7)
            {
                result = "单周";
            }
            if (span.Days % 14 >= 7)
            {
                result = "双周";
            }
            return result;
        }
        #endregion

        /// <summary>获得指定时间 是一年中的第几周</summary> 
        #region GetWeekIntOfYear
        public static int GetWeekIntOfYear(DateTime dateTime)
        {
            return new GregorianCalendar().GetWeekOfYear(dateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
        #endregion

        /// <summary>获得指定时间 单周还是双周</summary> 
        #region GetWeekStrOfYear
        public static string GetWeekStrOfYear(DateTime dateTime)
        {
            return GetWeekIntOfYear(dateTime) % 2 == 1 ? "单周" : "双周";
        }
        #endregion

        /// <summary> 计算年龄 </summary>
        #region 计算年龄
        public static int GetAgeByBirthDay(DateTime birthDay)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - birthDay.Year;
            if (now.Month < birthDay.Month || (now.Month == birthDay.Month && now.Day < birthDay.Day))
            {
                age--;
            }
            return age < 0 ? 0 : age;
        } 
        #endregion

        #region IsIP
        public static bool IsIP(string IP)
        {
            string regex = @"^((([1-9]?\d|1\d\d|2[0-4]\d|25[0-5])\.){3}([1-9]?\d|1\d\d|2[0-4]\d|25[0-5]))$";
            return System.Text.RegularExpressions.Regex.IsMatch(IP, regex);
        } 
        #endregion

        #region 读取文件
        /// <summary>
        /// 读取文件
        /// </summary>
        public static string ReadFile(string path, Encoding encoding = null)
        {
            string result = "";
            Encoding realEncoding = encoding ?? Encoding.UTF8;
            using (StreamReader reader = new StreamReader(path, realEncoding))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        #endregion
    }
}

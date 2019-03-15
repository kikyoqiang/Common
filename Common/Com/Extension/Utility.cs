using System;
using System.IO;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary> 公共工具类 </summary>
    public sealed class Utility
    {
        #region WebService 相关

        #region 检测WebService是否可用
        /// <summary> 检测WebService是否可用 </summary> 
        public static bool IsWebServiceAvaiable(string url)
        {
            bool bRet = false;
            Net.HttpWebRequest myHttpWebRequest = null;
            try
            {
                System.GC.Collect();

                myHttpWebRequest = (Net.HttpWebRequest)Net.WebRequest.Create(url);
                myHttpWebRequest.Timeout = 60000;
                myHttpWebRequest.KeepAlive = false;
                myHttpWebRequest.ServicePoint.ConnectionLimit = 200;
                using (Net.HttpWebResponse myHttpWebResponse = (Net.HttpWebResponse)myHttpWebRequest.GetResponse())
                {
                    bRet = true;
                    if (myHttpWebResponse != null)
                    {
                        myHttpWebResponse.Close();
                    }
                }
            }
            catch (Net.WebException e)
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

        #region 根据参数执行WebService
        /// <summary> 根据参数执行WebService </summary> 
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
                Net.WebClient wc = new Net.WebClient();
                Stream stream = wc.OpenRead(url + "?WSDL");//【1】
                Web.Services.Description.ServiceDescription sd = Web.Services.Description.ServiceDescription.Read(stream);//【2】
                Web.Services.Description.ServiceDescriptionImporter sdi = new Web.Services.Description.ServiceDescriptionImporter();//【3】
                sdi.AddServiceDescription(sd, "", "");
                CodeDom.CodeNamespace cn = new CodeDom.CodeNamespace(_namespace);//【4】
                //生成客户端代理类代码
                CodeDom.CodeCompileUnit ccu = new CodeDom.CodeCompileUnit();//【5】
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                //CSharpCodeProvider csc = new CSharpCodeProvider();//【6】
                CodeDom.Compiler.CodeDomProvider csc = CodeDom.Compiler.CodeDomProvider.CreateProvider("CSharp");
                //ICodeCompiler icc = csc.CreateCompiler();//【7】

                //设定编译器的参数
                CodeDom.Compiler.CompilerParameters cplist = new CodeDom.Compiler.CompilerParameters();//【8】
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");
                //编译代理类
                CodeDom.Compiler.CompilerResults cr = csc.CompileAssemblyFromDom(cplist, ccu);//【9】
                if (true == cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new StringBuilder();
                    foreach (CodeDom.Compiler.CompilerError ce in cr.Errors)
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

        #region 根据WebService获取类名
        /// <summary> 根据WebService获取类名 </summary> 
        private static string GetClassName(string url)
        {
            //假如URL为"http://localhost/InvokeService/Service1.asmx"
            //最终的返回值为 Service1
            string[] parts = url.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }
        #endregion

        #endregion

        #region _MD5 相关 

        /// <summary> 从路径获取文件的MD5 </summary>
        public static string GetMD5FromFile(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            Security.Cryptography.MD5CryptoServiceProvider md5 = new Security.Cryptography.MD5CryptoServiceProvider();
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

        /// <summary> 获取字节流的md5值 </summary>
        public static string GetMD5FromFileStream(byte[] buffer)
        {
            Security.Cryptography.MD5CryptoServiceProvider md5 = new Security.Cryptography.MD5CryptoServiceProvider();
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

        /// <summary> 32位MD5加密 </summary> 
        public static string Md5Hash(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        #endregion

        #region 日期 相关

        #region 根据时间 获取周几Int
        /// <summary> 根据时间 获取周几Int </summary>
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

        #region 根据时间 获取周几Int
        /// <summary> 根据时间 获取周几Int </summary>
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

        #region 根据时间 获取周几str 例：周一
        /// <summary> 根据时间 获取周几str 例：一 </summary>
        public static string GetWeekStrByInt(DateTime dateTime)
        {
            int week = GetWeekInt(dateTime);
            string str = "";
            switch (week)
            {
                case 1:
                    str = "一";
                    break;
                case 2:
                    str = "二";
                    break;
                case 3:
                    str = "三";
                    break;
                case 4:
                    str = "四";
                    break;
                case 5:
                    str = "五";
                    break;
                case 6:
                    str = "六";
                    break;
                case 7:
                    str = "日";
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        } 
        #endregion

        #region 获得目标日期 是单周 还是双周
        /// <summary>获得目标日期 是单周 还是双周 </summary> 
        public static string GetWeekByDate(DateTime Target)
        {
            string result = string.Empty;
            TimeSpan span = Target.Subtract(DateTime.Parse("2018-01-01 00:00:00"));
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

        #region 获得指定时间 是一年中的第几周
        /// <summary>获得指定时间 是一年中的第几周</summary> 
        public static int GetWeekIntOfYear(DateTime dateTime)
        {
            return new Globalization.GregorianCalendar().GetWeekOfYear(dateTime, Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
        #endregion

        #region 获得指定时间 单周还是双周
        /// <summary>获得指定时间 单周还是双周</summary> 
        public static string GetWeekStrOfYear(DateTime dateTime)
        {
            return GetWeekIntOfYear(dateTime) % 2 == 1 ? "单周" : "双周";
        }
        #endregion

        #region 计算年龄
        /// <summary> 计算年龄 </summary>
        public static int GetAgeByBirthDay(DateTime birthDay)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - birthDay.Year;
            if (now.Month < birthDay.Month || (now.Month == birthDay.Month && now.Day < birthDay.Day))
                age--;
            return age < 0 ? 0 : age;
        }
        #endregion

        #endregion

        #region   Stream  相关

        /// <summary> Stream 转换为 byte[] </summary>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        #endregion

        #region 文件 相关

        #region 读取文件
        /// <summary>  读取文件 </summary>
        public static string ReadFile(string path, Encoding encoding = null)
        {
            Encoding realEncoding = encoding ?? Encoding.UTF8;
            using (StreamReader reader = new StreamReader(path, realEncoding))
            {
                return reader.ReadToEnd();
            }
        }
        #endregion

        #region 获取文件大小 GB MB KB Byte
        /// <summary> 获取文件大小 GB MB KB Byte </summary>
        public static string GetFileSize(long size)
        {
            string FileSize = string.Empty;
            if (size > (1024 * 1024 * 1024))
                FileSize = ((double)size / (1024 * 1024 * 1024)).ToString(".##") + " GB";
            else if (size > (1024 * 1024))
                FileSize = ((double)size / (1024 * 1024)).ToString(".##") + " MB";
            else if (size > 1024)
                FileSize = ((double)size / 1024).ToString(".##") + " KB";
            else if (size == 0)
                FileSize = "0 Byte";
            else
                FileSize = ((double)size / 1).ToString(".##") + " Byte";

            return FileSize;
        }
        #endregion

        #region 递归文件夹下 所有文件数量
        /// <summary> 递归文件数量 </summary>
        public static int GetDirectoryCount(string dirp, ref int UpfileCount)
        {
            DirectoryInfo mydir = new DirectoryInfo(dirp);
            foreach (FileSystemInfo fsi in mydir.GetFileSystemInfos())
            {
                if (fsi is FileInfo)
                {
                    UpfileCount += 1;
                }
                else
                {
                    DirectoryInfo di = (DirectoryInfo)fsi;
                    string new_dir = di.FullName;
                    GetDirectoryCount(new_dir, ref UpfileCount);
                }
            }
            return UpfileCount;
        }
        #endregion

        #endregion

        #region   byte[] 相关

        #region byte[] 转换为 Base64
        /// <summary> byte[] 转换为 Base64 </summary>
        public static string BytesToToBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes).Replace("+", "%2B");
        }
        #endregion

        #region byte[] Zip压缩 为 Base64
        /// <summary> byte[] Zip压缩 为 Base64 </summary>
        public static string BytesToZipBase64(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream())
            using (var zip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress, true))
            {
                //压缩
                zip.Write(bytes, 0, bytes.Length);
                zip.Close();

                byte[] buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                string compressStr = Convert.ToBase64String(buffer);

                //base64编码
                byte[] encData_byte = new byte[compressStr.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(compressStr);
                return Convert.ToBase64String(encData_byte);
            }
        }
        #endregion

        #region Base64 Zip解压 为 byte[]
        /// <summary> Base64 Zip解压 为 byte[] </summary>
        public static byte[] ZipBase64ToBytes(string xml)
        {
            //base64解码
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(xml);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string decoded = new string(decoded_char);

            //解压缩
            byte[] compressBeforeByte = Convert.FromBase64String(decoded);
            byte[] buffer = new byte[0x1000];

            using (MemoryStream ms = new MemoryStream(compressBeforeByte))
            using (var zip = new IO.Compression.GZipStream(ms, IO.Compression.CompressionMode.Decompress, true))
            using (MemoryStream msreader = new MemoryStream())
            {
                int reader = 0;
                while ((reader = zip.Read(buffer, 0, buffer.Length)) > 0)
                {
                    msreader.Write(buffer, 0, reader);
                }
                msreader.Position = 0;
                buffer = msreader.ToArray();
            }

            byte[] compressAfterByte = buffer;
            return compressAfterByte;
        }
        #endregion

        #endregion

        #region 测试 相关

        #region 获取测试DataTable
        /// <summary> 获取测试DataTable </summary>
        public static System.Data.DataTable GetTableTest(params string[] columns)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            columns.ToList<string>().ForEach(a => dt.Columns.Add(a, typeof(string)));
            return dt;
        }
        /// <summary> 获取测试DataTable </summary>
        public static System.Data.DataTable GetTableTest(int rows, params string[] columns)
        {
            System.Data.DataTable dt = GetTableTest(columns);
            for (int i = 0; i < rows; i++)
            {
                System.Data.DataRow row = dt.NewRow();
                for (int j = 0; j < columns.Length; j++)
                {
                    row[columns[j]] = string.Format("{0}{1}", i, j);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
        #endregion

        #region 测试方法 耗时2.5s
        /// <summary> 测试方法 耗时2.5s </summary>
        public static void LongTimeMethod()
        {
            var list2 = new Collections.Generic.List<string>();
            for (int i = 0; i < 5000000; i++)
            {
                list2.Add(i.ToString());
            }
            list2 = null;
        }
        #endregion

        #region 获取程序集是 Debug 还是 Release 模式
        /// <summary> 获取程序集是 Debug 还是 Release 模式 </summary>
        public static string GetDebugMode(string assemblyName)
        {
            if (string.IsNullOrEmpty(assemblyName))
                throw new ArgumentNullException("assemblyName");

            var ass = Reflection.Assembly.LoadFile(assemblyName);
            var att = ass.GetCustomAttribute<Diagnostics.DebuggableAttribute>();
            string ret = att.IsJITTrackingEnabled ? "Debug" : "Release";
            return ret;
        }
        #endregion

        #region Stopwatch计时器
        /// <summary>
        /// 计时器开始
        /// </summary>
        /// <returns></returns>
        public static Diagnostics.Stopwatch TimerStart()
        {
            var watch = new Diagnostics.Stopwatch();
            watch.Reset();
            watch.Start();
            return watch;
        }
        /// <summary>
        /// 计时器结束
        /// </summary>
        /// <param name="watch"></param>
        /// <returns></returns>
        public static string TimerEnd(Diagnostics.Stopwatch watch)
        {
            watch.Stop();
            double costtime = watch.ElapsedMilliseconds;
            return costtime.ToString();
        }
        #endregion

        #endregion

        #region 系统 相关

        /// <summary> 获取系统内存大小 </summary>
        [System.Runtime.InteropServices.DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref Memory_Info meminfo);
        /// <summary> 内存的信息结构 </summary>
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct Memory_Info
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }

        #region 关闭进程
        /// <summary> 关闭进程 </summary>
        public static void StopProcess(string processName)
        {
            var processAll = Diagnostics.Process.GetProcesses();
            var processes = processAll.Where(e => e.ProcessName.Contains(processName) && !e.ProcessName.Contains("vshost"));
            foreach (var process in processes)
            {
                process.CloseMainWindow();

                if (!process.HasExited)
                    process.Kill();

                if (process != null)
                    process.Close();
            }
        }
        #endregion

        #endregion

        #region 判断 相关

        #region KeyPress是否是全角
        /// <summary> KeyPress是否是全角 </summary>
        public static bool IsFullAngle(System.Windows.Forms.KeyPressEventArgs e)
        {
            return ((e.KeyChar >= 65281 && e.KeyChar <= 65374) || e.KeyChar == 12288);
        }
        #endregion

        #region 是否存在任何一个为空
        /// <summary> string[] 是否存在任何一个为空 </summary>
        public static bool IsAnyNullOrEmpty(params string[] strings)
        {
            return strings == null || strings.Any(a => a.IsNullOrEmpty());
        }
        #endregion 

        #region IsContainsNum
        /// <summary> 字符串是否包含数字 </summary>
        public static bool IsContainsNum(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] >= 48 && text[i] <= 57)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 是否是IP
        /// <summary> 是否是IP </summary>
        public static bool IsIP(string IP)
        {
            string regex = @"^((([1-9]?\d|1\d\d|2[0-4]\d|25[0-5])\.){3}([1-9]?\d|1\d\d|2[0-4]\d|25[0-5]))$";
            return System.Text.RegularExpressions.Regex.IsMatch(IP, regex);
        }
        #endregion

        #endregion

        #region 其它 相关

        #region 获得强随机数，较耗内存
        /// <summary> 获得强随机数，较耗内存 </summary>
        public static int GetRandom()
        {
            byte[] randomBytes = new byte[8];
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            var result = BitConverter.ToInt32(randomBytes, 0);
            result = System.Math.Abs(result);
            return result;
        }
        #endregion

        #region 10进制转换2进制
        /// <summary>
        /// 10进制转换2进制
        /// </summary>
        /// <param name="str">返回的2进制字符串</param>
        /// <param name="num">需要转换的10进制数</param>
        /// <returns></returns>
        public static string ConvertToBinary(out string str, int num)
        {
            str = string.Empty;
            if (num == 0)
                return str;
            ConvertToBinary(out str, num / 2);
            return str += num % 2;
        }
        #endregion

        #region 深度拷贝
        /// <summary>
        /// 深度拷贝
        /// <para>source 必须标注 Serializable</para>
        /// </summary>
        public static T DeepCopy<T>(T source) where T : class
        {
            var stream = new System.IO.MemoryStream();
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            formatter.Serialize(stream, source);
            stream.Position = 0;
            return formatter.Deserialize(stream) as T;
        } 
        #endregion

        #endregion

        #region 线程 相关
        /// <summary> 将方法排入队列以便执行。此方法在有线程池线程变得可用时执行 </summary>
        public static void QueueUserWorkItem(Action work, Action complete = null)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(obj =>
            {
                work();
                if (complete != null)
                    complete();
            });
        } 
        #endregion
    }
}

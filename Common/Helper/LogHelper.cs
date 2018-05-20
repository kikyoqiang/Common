using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public class LogHelper
    {
        /// <summary> 在构造函数时会设置为当前程序的目录,后边带\符号 </summary>
        private static string LogDirectory = @".\ErrorLog\";
        private static string errorFileName = @"ErrorLog.log";
        private static string infoFileName = @"InfoLog.log";
        private static string debugFileName = @"DebugLog.log";
        private static readonly object locker = new object();
        private static LogHelper instance;

        private LogHelper()
        {
            LogDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\ErrorLog\\";
        }

        private static void CreateLogFile(string logType)
        {
            lock (locker)
            {
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }

                switch (logType)
                {
                    case "Error":
                        errorFileName = "ErrorLog_" + DateTime.Now.ToString("yyyy_MM_dd") + ".log";
                        string errFilePath = LogDirectory + errorFileName;
                        if (!System.IO.File.Exists(errFilePath))
                        {
                            FileStream fs = System.IO.File.Create(errFilePath);
                            fs.Close();
                        }
                        break;
                    case "Info":
                        infoFileName = "InfoLog_" + DateTime.Now.ToString("yyyy_MM_dd") + ".log";
                        string infoFilePath = LogDirectory + infoFileName;
                        if (!System.IO.File.Exists(infoFilePath))
                        {
                            FileStream fs = System.IO.File.Create(infoFilePath);
                            fs.Close();
                        }
                        break;
                    case "Debug":
                        debugFileName = "DebugLog_" + DateTime.Now.ToString("yyyy_MM_dd") + ".log";
                        string debugFilePath = LogDirectory + debugFileName;
                        if (!System.IO.File.Exists(debugFilePath))
                        {
                            FileStream fs = System.IO.File.Create(debugFilePath);
                            fs.Close();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public static LogHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new LogHelper();
                        }
                    }
                }
                return instance;
            }
        }

        private void Log(string logType, string message)
        {
            try
            {
                CreateLogFile(logType); //不要将此过程放入锁中，会导致死锁
                lock (locker)
                {
                    string method_name = "";
                    string name_space = "";

                    StackTrace st = new StackTrace(true);
                    StackFrame[] StackFrameList = st.GetFrames();
                    if (StackFrameList.Length >= 3)
                    {
                        method_name = st.GetFrame(2).GetMethod().Name.ToString();
                        name_space = st.GetFrame(2).GetMethod().DeclaringType.FullName.ToString();
                    }
                    else if (StackFrameList.Length == 2)
                    {
                        method_name = st.GetFrame(1).GetMethod().Name.ToString();
                        name_space = "【FrameLength2】" + st.GetFrame(1).GetMethod().DeclaringType.FullName.ToString();
                    }
                    else if (StackFrameList.Length == 1)
                    {
                        method_name = st.GetFrame(0).GetMethod().Name.ToString();
                        name_space = "【FrameLength1】" + st.GetFrame(0).GetMethod().DeclaringType.FullName.ToString();
                    }
                    else
                    {
                        name_space = "【FrameLength0】";
                    }

                    string timeStr = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string funcName = name_space + " : " + method_name;
                    string filePath = string.Empty;

                    switch (logType)
                    {
                        case "Error":
                            filePath = LogDirectory + errorFileName;
                            break;
                        case "Info":
                            filePath = LogDirectory + infoFileName;
                            break;
                        case "Debug":
                            filePath = LogDirectory + debugFileName;
                            break;
                        default:
                            break;
                    }

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        System.IO.StreamWriter objWStream = new System.IO.StreamWriter(filePath, true, System.Text.Encoding.Default);
                        objWStream.WriteLine(timeStr + " " + funcName + ":" + message);
                        objWStream.Close();
                        objWStream = null;
                    }
                    else
                    {
                        Console.WriteLine("Invalid log file path!!!!");
                    }
                }
            }
            catch (System.Exception ex)
            {
                string str = string.Concat("LogError:", ex.Message);
                Console.WriteLine(str);
            }
        }

        public void WriteError(string message)
        {
            Log("Error", string.Format(" {0} ", message));
        }

        public void WriteError(string message, Exception excep)
        {
            string exMsg = excep.Message;
            string tempStr = " " + message + "   |  Exception:" + exMsg + "\r\n StackTrace:" + excep.StackTrace;
            Log("Error", tempStr);
        }

        public void WriteInfo(string message)
        {
            Log("Info", string.Concat(" ", message, " "));
        }

        public void WriteInfo(string message, Exception excep)
        {
            string exMsg = excep.Message;
            string tempStr = " " + message + "  |  Exception:" + exMsg;
            Log("Info", tempStr);
        }

        public void WriteDebug(string message, bool LogThis = true)
        {
            if (LogThis)
            {
                //个别DebugLog可以由传入的参数控制
                Log("Debug", message);
            }
        }
    }
}

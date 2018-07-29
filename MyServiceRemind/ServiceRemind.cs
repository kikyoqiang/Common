using Cjwdev.WindowsApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace MyServiceRemind
{

    public partial class ServiceRemind : ServiceBase
    {
        #region Ready
        string filePath = string.Empty;
        string startPath = string.Empty;
        #endregion

        #region ServiceRemind
        public ServiceRemind()
        {
            InitializeComponent();
            filePath = AppDomain.CurrentDomain.BaseDirectory + "MyService.Log";
        } 
        #endregion

        #region OnStart
        protected override void OnStart(string[] args)
        {
            ThreadPool.QueueUserWorkItem(obj =>
            {
                try
                {
                    Write($"{DateTime.Now}，服务启动！");

                    ConfigurationManager.RefreshSection("appSettings");
                    string startName = ConfigurationManager.AppSettings["StartName"];
                    int sleepTime = int.Parse(ConfigurationManager.AppSettings["SleepTime"]);
                    int startHour = int.Parse(ConfigurationManager.AppSettings["StartHour"]);
                    int endHour = int.Parse(ConfigurationManager.AppSettings["EndHour"]);
                    startPath = AppDomain.CurrentDomain.BaseDirectory + startName;

                    while (true)
                    {
                        Thread.Sleep(sleepTime * 1000);
                        if (DateTime.Now.Hour >= startHour && DateTime.Now.Hour <= endHour)
                        {
                            ShowForm(startPath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Write($"{DateTime.Now}，服务异常！ \r\n {ex.ToString()}");
                }
            });
        }
        #endregion

        #region OnStop
        protected override void OnStop()
        {
            Write($"{DateTime.Now}，服务停止！");
        }
        #endregion

        #region Read
        private string Read(string timePath)
        {
            string str = string.Empty;
            try
            {
                using (StreamReader read = new StreamReader(timePath, Encoding.UTF8))
                {
                    str = read.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Write("读异常 " + ex.ToString());
            }
            return str;
        }
        #endregion

        #region Write
        private void Write(string msg)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine(msg);
            }
        }
        #endregion

        #region ShowForm
        private void ShowForm(string filePath)
        {
            try
            {
                string appStartPath = filePath;
                IntPtr userTokenHandle = IntPtr.Zero;
                ApiDefinitions.WTSQueryUserToken(ApiDefinitions.WTSGetActiveConsoleSessionId(), ref userTokenHandle);

                ApiDefinitions.PROCESS_INFORMATION procInfo = new ApiDefinitions.PROCESS_INFORMATION();
                ApiDefinitions.STARTUPINFO startInfo = new ApiDefinitions.STARTUPINFO();
                startInfo.cb = (uint)Marshal.SizeOf(startInfo);

                ApiDefinitions.CreateProcessAsUser(
                    userTokenHandle,
                    appStartPath,
                    "",
                    IntPtr.Zero,
                    IntPtr.Zero,
                    false,
                    0,
                    IntPtr.Zero,
                    null,
                    ref startInfo,
                    out procInfo);

                if (userTokenHandle != IntPtr.Zero)
                    ApiDefinitions.CloseHandle(userTokenHandle);

                int _currentAquariusProcessId = (int)procInfo.dwProcessId;
            }
            catch (Exception ex)
            {
                Write("启动程序异常 " + ex.ToString());
            }
        }
        #endregion
    }
}


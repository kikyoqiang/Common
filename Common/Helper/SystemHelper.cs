using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Common
{
    public class SystemHelper
    {
        #region 获取当前系统用户
        // <summary>
        // 获取当前系统用户
        // </summary>
        // <returns>用户名称</returns>
        public static string GetUserName()
        {
            string userName = string.Empty;
            try
            {
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    userName = mo["UserName"].ToString();
                }
                moc = null;
                mc = null;
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError("获取当前系统用户 异常", ex);
            }
            return userName;
        }
        #endregion

        #region 获取MAC

        /// <summary>
        /// 获取MAC
        /// </summary>
        /// <returns>MAC</returns>
        public static string GetMacAddress()
        {
            string mac = string.Empty;
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError("获取MAC 异常", ex);
            }
            return mac;
        }
        #endregion

        #region 获取内网IP

        /// <summary>
        /// 获取内网IP
        /// </summary>
        /// <returns>内网IP</returns>
        public static string GetClientLocalIPAddress()
        {
            string ip = string.Empty;
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                bool findFlag = false;
                bool IPEnabled = false;
                foreach (ManagementObject mo in moc)
                {
                    IPEnabled = (bool)mo["IPEnabled"];
                    if (!IPEnabled)
                        continue;
                    string[] iplist = (string[])mo["IPAddress"];
                    for (int i = 0; i < iplist.Length; i++)
                    {
                        if (Utility.IsIP(iplist[i]))
                        {
                            ip = iplist[i];
                            findFlag = true;
                            break;
                        }
                    }
                    if (findFlag)
                        break;
                }
                moc = null;
                mc = null;
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError("获取内网IP 异常", ex);
            }
            return ip;
        }
        #endregion

        #region 获取外网IP

        /// <summary>
        /// 获取外网IP
        /// </summary>
        /// <returns>外网IP</returns>
        public static string getClientInternetIPAddress()
        {
            string internetAddress = "";
            WebClient webClient = null;
            try
            {
                using (webClient = new WebClient())
                {
                    internetAddress = webClient.DownloadString("http://www.coridc.com/ip");     //从外部网页获得IP地址
                    string Match = "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}";
                    if (!System.Text.RegularExpressions.Regex.IsMatch(internetAddress, Match))        //判断IP是否合法
                    {
                        internetAddress = webClient.DownloadString("http://fw.qq.com/ipaddress");       //从腾讯提供的API中获得IP地址
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError("获取外网IP 异常", ex);
            }
            finally
            {
                if (webClient != null)
                    webClient.Dispose();
            }
            return internetAddress;
        }
        #endregion

        #region 获取硬盘ID

        /// <summary>
        /// 获取硬盘ID
        /// </summary>
        /// <returns>硬盘ID</returns>
        public static string GetDiskID()
        {
            string hdInfo = "";
            try
            {
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
                hdInfo = disk.Properties["VolumeSerialNumber"].Value.ToString();
                disk = null;
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError("获取硬盘ID 异常", ex);
            }
            return hdInfo.Trim();
        }
        #endregion

        #region 获取CPU ID

        /// <summary>
        /// 获取CPU ID
        /// </summary>
        /// <returns>CPU ID</returns>
        public static string GetCpuID()
        {
            string cpuInfo = "";        //cpu序列号 //获取CPU序列号代码
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError("获取CPU ID 异常", ex);
            }
            return cpuInfo;
        }
        #endregion

        #region 获取系统名称

        /// <summary>
        /// 获取系统名称
        /// </summary>
        /// <returns>系统名称</returns>
        public static string GetSystemType()
        {
            string st = "";
            try
            {
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    st = mo["SystemType"].ToString();
                }
                moc = null;
                mc = null;
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError("获取系统名称 异常", ex);
            }
            return st;
        }
        #endregion

        #region 获取系统内存

        /// <summary>
        /// 获取系统内存
        /// </summary>
        /// <returns>系统内存</returns>
        public static string GetTotalPhysicalMemory()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    st = mo["TotalPhysicalMemory"].ToString();
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {

            }
        }
        #endregion

        /// <summary>
        /// 获取所有默认网关不为空的IP和MAC
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetIPAndMacList()
        {
            Dictionary<string, string> ipList = new Dictionary<string, string>();
            //获取所有网卡信息
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                //判断是否为以太网卡
                //Wireless80211         无线网卡;    Ppp     宽带连接;Ethernet              以太网卡
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    //获取以太网卡网络接口信息
                    IPInterfaceProperties ip = adapter.GetIPProperties();
                    //获取单播地址集
                    UnicastIPAddressInformationCollection ipCollection = ip.UnicastAddresses;
                    foreach (UnicastIPAddressInformation ipadd in ipCollection)
                    {
                        //InterNetwork    IPV4地址      InterNetworkV6        IPV6地址
                        //Max            MAX 位址
                        if (ipadd.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            string tempIP = ipadd.Address.ToString();
                            if (ip.GatewayAddresses.Count > 0)
                            {
                                string tempGateway = ip.GatewayAddresses[0].Address.ToString();
                                if (!string.IsNullOrEmpty(tempGateway) && tempGateway != "0.0.0.0")
                                {
                                    string tempMac = adapter.GetPhysicalAddress().ToString();
                                    ipList.Add(tempMac, tempIP);
                                }
                            }
                        }
                    }
                }
                else if (adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    //获取以太网卡网络接口信息
                    IPInterfaceProperties ip = adapter.GetIPProperties();
                    //获取单播地址集
                    UnicastIPAddressInformationCollection ipCollection = ip.UnicastAddresses;
                    foreach (UnicastIPAddressInformation ipadd in ipCollection)
                    {
                        //InterNetwork    IPV4地址      InterNetworkV6        IPV6地址
                        //Max            MAX 位址
                        if (ipadd.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            string tempIP = ipadd.Address.ToString();
                            if (ip.GatewayAddresses.Count > 0)
                            {
                                string tempGateway = ip.GatewayAddresses[0].Address.ToString();
                                if (!string.IsNullOrEmpty(tempGateway) && tempGateway != "0.0.0.0")
                                {
                                    string tempMac = adapter.GetPhysicalAddress().ToString();
                                    ipList.Add(tempMac, tempIP);
                                }
                            }
                        }

                    }
                }
            }
            
            return ipList;
        }
    }
}

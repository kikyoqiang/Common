using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Services.Description;

namespace Common
{
    public class WebServiceClientHelper
    {
        #region ready
        private string _serviceURL = string.Empty;
        private string _serviceNameSpace = string.Empty;
        private string _serviceClassName = string.Empty;
        private Type _webServiceType = null;
        private object _webServiceObject = null;
        private bool _webServiceConnectedFlag = false;
        #endregion

        #region WebServiceClientHelper
        public WebServiceClientHelper(string url, string spacename, string classname)
        {
            try
            {
                _serviceURL = url;
                _serviceClassName = classname;
                _serviceNameSpace = spacename;

                if (Utility.IsAnyNullOrEmpty(spacename, classname))
                {
                    Common.LogHelper.Instance.WriteError("调用目标地址或命名空间为空.");
                    _webServiceConnectedFlag = false;
                    return;
                }

                string _namespace = _serviceNameSpace;
                if (string.IsNullOrEmpty(classname))
                {
                    classname = GetClassName(_serviceURL);
                }

                //获取服务描述语言(WSDL)
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(_serviceURL + "?WSDL");
                ServiceDescription sd = ServiceDescription.Read(stream);
                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(_namespace);
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                CodeDomProvider csc = CodeDomProvider.CreateProvider("CSharp");

                //设定编译器的参数
                CompilerParameters cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                //编译代理类
                CompilerResults cr = csc.CompileAssemblyFromDom(cplist, ccu);
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
                _webServiceType = assembly.GetType(_namespace + "." + classname, true, true);
                _webServiceObject = Activator.CreateInstance(_webServiceType);
                _webServiceConnectedFlag = true;
            }
            catch (Exception)
            {
                _webServiceConnectedFlag = false;
                throw;
            }
        }
        #endregion

        #region GetClassName
        private string GetClassName(string url)
        {
            //假如URL为"http://localhost/InvokeService/Service1.asmx"
            //最终的返回值为 Service1
            string[] parts = url.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        } 
        #endregion

        #region 调用webservice接口
        /// <summary>
        /// 调用webservice接口
        /// </summary>
        /// <param name="methodname">接口名称</param>
        /// <param name="args">参数列表 (虽采用Object 会产生装箱操作 降低性能，但为了支持多种类型参数故使用object)</param>
        /// <returns>返回结果</returns>
        public object InvokeWebServiceMethod(string methodname, object[] args)
        {
            try
            {
                Common.LogHelper.Instance.WriteInfo("调用接口" + methodname + ", 参数列表:");
                if (args != null && args.Length > 0)
                {
                    Common.LogHelper.Instance.WriteInfo("===============Args Begin==================");
                    for (int i = 0; i < args.Length; i++)
                    {
                        Common.LogHelper.Instance.WriteInfo("[Args" + i + "]:" + args[i].ToSafeStr());
                    }
                    Common.LogHelper.Instance.WriteInfo("===============Args End====================");
                }

                if (!_webServiceConnectedFlag || _webServiceObject == null || _webServiceType == null)
                    return null;

                System.Reflection.MethodInfo mi = _webServiceType.GetMethod(methodname);//【11】
                return mi.Invoke(_webServiceObject, args);
            }
            catch (Exception ex)
            {
                Common.LogHelper.Instance.WriteError("调用接口" + methodname + "失败, 参数列表:");
                if (args != null && args.Length > 0)
                {
                    Common.LogHelper.Instance.WriteError("===============Args Begin==================");
                    for (int i = 0; i < args.Length; i++)
                    {
                        Common.LogHelper.Instance.WriteError("[Args" + i + "]:" + args[i].ToSafeStr());
                    }
                    Common.LogHelper.Instance.WriteError("===============Args End====================");
                }
                Common.LogHelper.Instance.WriteError(ex.Message + "\r\n StackTrace:\r\n" + ex.StackTrace);

                return null;
            }
        } 
        #endregion
    }
}

using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Web
{
    /// <summary>
    /// WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(Description = "求和")]
        public string Add(string a, string b)
        {
            int sum = a.ToInt() + b.ToInt();
            return sum.ToSafeString();
        }
        [WebMethod(Description = "求积")]
        public string Sum(string a, string b)
        {
            int sum = a.ToInt() * b.ToInt();
            return sum.ToSafeString();
        }
        [WebMethod(Description = "A01")]
        public void A01(string a)
        {
            string result = string.Format("{0} {1}", DateTime.Now.ToDateStrHH(), a);
            Context.Response.ContentType = "application/json";//text/plain
            //Context.Response.ContentType = "text/plain";
            JavaScriptSerializer java = new JavaScriptSerializer();
            result = java.Serialize(new { d = result });
            Context.Response.Write(result);
        }
        [WebMethod(Description = "A02")]
        public string A02(string a)
        {
            string s = string.Format("{0} {1}", DateTime.Now.ToDateStrHH(), a);
            return s;
        }
    }
}

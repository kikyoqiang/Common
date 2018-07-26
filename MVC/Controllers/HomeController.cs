using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public void Index()
        {
            string result = "";
            try
            {
                Stream stream = HttpContext.Request.InputStream;
                StreamReader reader = new StreamReader(stream);
                string xmlString = reader.ReadToEnd();
                var set = XmlHelper.GetDataSet(xmlString);
                var dt = set.GetDataTable("DocumentElement");
                string methord = dt.GetStringValue(0, "MethodName");
                result = ReadFile(methord);
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            HttpContext.Response.Write(result);
            HttpContext.Response.End();
        }
        private static string ReadFile(string s)
        {
            string path = string.Format(@"E:\0Project\0FaBu\Test3\1\{0}.txt", s);
            return FileHelper.ReadFile(path, Encoding.UTF8);
        }
    }
}
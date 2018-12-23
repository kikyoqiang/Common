using Common;
using System;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
            return View();
            //PostMethodName();
        }

        public ActionResult Add(string i1, string i2)
        {
            return Content((int.Parse(i1) + int.Parse(i2)).ToString());
        }




        #region PostMethodName
        private void PostMethodName()
        {
            string result = "";
            try
            {
                using (Stream stream = HttpContext.Request.InputStream)
                using (StreamReader reader = new StreamReader(stream))
                {
                    string xmlString = reader.ReadToEnd();
                    var set = XmlHelper.GetDataSet(xmlString);
                    var dt = set.GetDataTable("DocumentElement");
                    string methord = dt.GetStringValue(0, "MethodName");
                    result = ReadFile(methord);
                }
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
        #endregion
    }
}
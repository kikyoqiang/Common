using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
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
                StreamReader reader2 = new StreamReader(stream);
                string str = reader2.ReadToEnd();
                DataSet set = new DataSet();
                using (System.IO.StringReader reader = new System.IO.StringReader(str))
                {
                    set.ReadXml(reader, XmlReadMode.InferTypedSchema);
                }
                var a = set.Tables["DocumentElement"];

                string methord = a.Rows[0]["MethodName"].ToString();

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
            string result = "";
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
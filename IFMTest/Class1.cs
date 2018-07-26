using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace IFMTest
{
    class Class1
    {
        static void Main11()
        {
            int i = 0;
            while (true)
            {
                if (i++ == 6)
                {
                    Console.WriteLine("666");
                    break;
                }
            }
            Console.WriteLine("777777");
            //Random random = new Random();
            //while (true)
            //{
            //    Thread.Sleep(random.Next(1, 3) * 1000);
            //    Console.WriteLine("1");
            //    if (DateTime.Now.Second % 2 == 0)
            //    {
            //        Console.WriteLine("goto Over");
            //        goto Over;
            //    }

            //    Console.WriteLine("2");
            //    Console.WriteLine("3");
            //    Console.WriteLine("4");

            //    Over:
            //    Console.WriteLine("5");
            //}

            Console.ReadKey();
        }
        private static string ReadFile(int type)
        {
            string temp = string.Empty;
            string path = @"E:\临时\1\新建文本文档" + type + ".txt";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.UTF8))
            {
                temp = sr.ReadToEnd();
            }
            return temp;
        }
        private DataSet GetDataSet(string xmlString)
        {
            DataSet set = new DataSet();
            using (StringReader reader = new StringReader(xmlString))
            {
                set.ReadXml(reader, XmlReadMode.InferTypedSchema);
            }
            return set;
        }
        private static string MZ_GetMaxID(string definition, int sepNum)
        {
            List<string> list = new List<string>();
            list.Add(@"<Definition>" + definition + @"</Definition>");
            string postString = getPostXML("MZ_GetMaxID", list);
            return postString;
        }

        private static string getPostXML(string methodName, List<string> paramsString = null)
        {
            string post = string.Empty;
            StringBuilder sbXML = new StringBuilder();
            try
            {
                string AccessKey = @"d59b8f66-578e-41ba-adda-67cffb110da4";
                string data_org_id = @"44180101";
                string data_sys_id = @"0046";
                sbXML.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\" ?>");
                sbXML.AppendLine(@"<DocumentElement>");
                sbXML.Append(@"<AccessKey>").Append(AccessKey).Append(@"</AccessKey>").AppendLine();
                sbXML.Append(@"<MethodName>").Append(methodName).Append(@"</MethodName>").AppendLine();
                sbXML.AppendLine(@"<DataTable>");
                sbXML.Append(@"<data_org_id>").Append(data_org_id).Append(@"</data_org_id>").AppendLine();
                sbXML.Append(@"<data_sys_id>").Append(data_sys_id).Append(@"</data_sys_id>").AppendLine();
                if (paramsString != null && paramsString.Count > 0)
                {
                    foreach (string str in paramsString)
                    {
                        sbXML.AppendLine(str); //例如str为 <PatientCaseNo>2688426</PatientCaseNo>
                    }
                }
                sbXML.AppendLine(@"</DataTable>");
                sbXML.AppendLine(@"</DocumentElement>");
                post = sbXML.ToString();
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError("     getPostXML异常    ", ex);
            }
            return post;
        }
    }
}

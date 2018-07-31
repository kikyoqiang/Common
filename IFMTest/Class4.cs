using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IFMTest
{
    class Class4
    {
        static void Main()
        {
            SqlServerHelper.Instance.Init("172.16.2.88", "xytxForProduct_清远市人民医院", "sa", "Win2008");
            //SqlServerHelper.Instance.Init(".", "Test", "sa", "123456");
            DataTable dt = SqlServerHelper.Instance.ExecuteQuery("SELECT * FROM dbo.tPatients");


            Console.ReadKey();
        }
    }
}

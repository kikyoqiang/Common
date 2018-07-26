using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonDB;
using System.Transactions;

namespace IFMTest
{
    class Class2
    {
        static void Main()
        {
            var scope = new TransactionScope();

            string sql = @"update dbo.DialyseRecordSub3 set ItemCount='1000' where ID='DC066607-C095-452E-9A64-74A5A5A75813'";
            string[] strs = new string[] { "172.16.2.88", "xytxForProduct_清远市人民医院", "sa", "Win2008" };
            DataManageSqlServer.Instance().Init(strs[0], strs[1], strs[2], strs[3]);
            DataManageSqlServer.Instance().ExecuteSql(sql);

            string sql2 = @"update dbo.DialyseRecordSub3 set ItemCount='9998' where ID='DC066607-C095-452E-9A64-74A5A5A75813'";
            DataManageSqlServer.Instance().Init(strs[0], strs[1], strs[2], strs[3]);
            DataManageSqlServer.Instance().ExecuteSql(sql2);

            scope.Complete();
            scope.Dispose();
           //Console.ReadKey();
        }
    }
}

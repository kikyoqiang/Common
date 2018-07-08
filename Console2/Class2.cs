using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console2
{
    class Class2
    {
        static void Main()
        {
            SortedSet<int> set = new SortedSet<int>();
            RedBlackTree<int, int> red = new RedBlackTree<int, int>();
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                red.Put(random.Next(1, 15), random.Next(1, 15));
            }
            #region DataTable
            //DataTable dt = new DataTable();
            //dt.Columns.Add("Id", typeof(int));
            //dt.Columns.Add("Name", typeof(string));
            //dt.Columns.Add("Bithday", typeof(DateTime));
            //dt.Columns.Add("Count", typeof(int));
            //var columnId = dt.Columns[0];
            //dt.Columns["Count"].Expression = "avg(Id)";

            //dt.DefaultView.ToTable(true, "Id");
            //for (int i = 0; i < 5; i++)
            //{
            //    var row = dt.NewRow();
            //    row["Id"] = i + 1;
            //    dt.Rows.Add(row);
            //} 
            #endregion

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IFMTest
{
    class Program
    {
        /// <summary> 体重值的集合 </summary>
        private static List<double> lsWeight = new List<double>();
        /// <summary> 是否自动保存体重 </summary>
        private static string strOuto = "1";
        /// <summary> 体重稳定计算后的值 </summary>
        private static double StableWeight = 0;

        private static string lblWeightValue = "";

        private static Timer threadTimer;

        private static Timer threadTimer2;

        private static List<string> list2 = new List<string>();

        private static Random random = new Random();

        static void Main2(string[] args)
        {
            #region MyRegion
            //List<double> list = new List<double>();
            //list.Add(2.7);
            //list.Add(2.8);
            //list.Add(3.8);
            //list.Add(2.9);
            //list.Add(2.6);
            //list.Add(1.0);
            //list.Add(2.0);
            //list.Add(4.0);
            //list.Add(3.0);
            //list.Add(2.3);
            //list.Sort();
            //var a = list[list.Count / 2];
            //Console.ReadKey(); 
            #endregion

            threadTimer = new Timer(obj =>
            {
                lblWeightValue = (random.Next(1, 100) * 0.01 + 40).ToSafeString();
                list2.Add(lblWeightValue);
                if (list2.Count > 6)
                {
                    threadTimer.Dispose();
                }
                Console.WriteLine(lblWeightValue);
            }, null, Timeout.Infinite, 500);

            threadTimer.Change(0, 300);

            threadTimer2 = new Timer(obj =>
            {
                if (lblWeightValue.ToFloat() > 0)
                {
                    double StableWeight = DealWeightData();
                    if (StableWeight > 0)
                    {
                        Console.WriteLine("最终体重" + StableWeight);
                        foreach (var item in lsWeight)
                        {
                            Console.WriteLine(item);
                        }
                        Console.WriteLine("==========");
                        threadTimer2.Dispose();
                    }
                }
            }, null, Timeout.Infinite, 500);

            threadTimer2.Change(0, 300);

            Console.ReadKey();
        }
        private static double DealWeightData()
        {
            if (lsWeight.Count < 6)
            {
                string strweightstr = lblWeightValue;
                double strweight = 0;
                double.TryParse(strweightstr, out strweight);

                if (strweight > 30)
                {
                    if (lsWeight.Count < 1)
                        lsWeight.Add(strweight);
                    else if ((strweight - 1) > (lsWeight[lsWeight.Count - 1]))
                    {
                        lsWeight.Clear();
                        lsWeight.Add(strweight);
                    }
                    else if (Math.Abs(strweight - (lsWeight[lsWeight.Count - 1])) < 1)
                    {
                        lsWeight.Add(strweight);
                    }
                    //LogHelper.Instance.WriteInfo(lsWeight.Count.ToString());
                }

                return -1;
            }
            else
            {
                lsWeight.Sort();
                Console.WriteLine("体重list为6");
                return lsWeight[lsWeight.Count / 2];
            }
        }
    }
}

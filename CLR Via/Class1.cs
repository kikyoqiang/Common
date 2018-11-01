using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLR_Via
{
    class Class1
    {
        private const int c_numElements = 10000;
        static void Main22()
        {
            int[,] a2Dim = new int[c_numElements, c_numElements];

            int[][] ajagged = new int[c_numElements][];
            for (int x = 0; x < c_numElements; x++)
            {
                ajagged[x] = new int[c_numElements];
            }

            Safe2DimArrayAccess(a2Dim);

            SafeJaggedArrayAccess(ajagged);
        }
        private static int Safe2DimArrayAccess(int[,] a)
        {
            int sum = 0;
            for (int i = 0; i < c_numElements; i++)
            {
                for (int j = 0; j < c_numElements; j++)
                {
                    sum += a[i, j];
                }
            }
            return sum;
        }
        private static int SafeJaggedArrayAccess(int[][] a)
        {
            int sum = 0;
            for (int i = 0; i < c_numElements; i++)
            {
                for (int j = 0; j < c_numElements; j++)
                {
                    sum += a[i][j];
                }
            }
            return sum;
        }
        //private static unsafe int UnSafe2DimArrayAccess(int[,] a)
        //{
        //    int sum = 0;
        //    fixed (int* pi = a)
        //    {
        //        for (int x = 0; x < c_numElements; x++)
        //        {
        //            int baseOfDim = x * c_numElements;
        //            for (int y = 0; y < c_numElements; y++)
        //            {
        //                sum += pi[baseOfDim + y];
        //            }
        //        }
        //    }
        //    return sum;
        //}
    }
}

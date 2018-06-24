using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2
{
    public class QuickSort<T> where T : IComparable<T>
    {
        public static void Sort(T[] array)
        {
            //Sort(array, 0, array.Length - 1);
        }

        private static void Sort(T[] array, int lo, int hi)
        {
            if (lo >= hi) return;
            int index = Partition(array, lo, hi);
            Sort(array, lo, index - 1);
            Sort(array, index + 1, hi);
        }

        private static int Partition(T[] array, int lo, int hi)
        {
            int i = lo, j = hi + 1;
            while (true)
            {
                while (array[++i].CompareTo(array[lo]) < 0)
                {
                    if (i == hi) break;
                }
                while (array[--j].CompareTo(array[lo]) > 0)
                {
                    if (j == lo) break;
                }
                if (i >= j) break;
                Swap(array, i, j);
            }
            Swap(array, lo, j);
            return j;
        }

        private static void Swap(T[] array, int a, int b)
        {
            T temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }
    }
}

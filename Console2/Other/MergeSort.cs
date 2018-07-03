using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2
{
    public class MergeSort<T> where T : IComparable<T>
    {
        private static T[] aux;
        public static void Sort(T[] array)
        {
            aux = new T[array.Length];
            Sort(array, 0, array.Length - 1);
        }
        private static void Sort(T[] array, int lo, int hi)
        {
            if (lo >= hi) return;
            int mid = (hi - lo) / 2 + lo;
            Sort(array, lo, mid);
            Sort(array, mid + 1, hi);
            Merge(array, lo, mid, hi);
        }
        private static void Merge(T[] array, int lo, int mid, int hi)
        {
            int i = lo, j = mid + 1;
            for (int k = lo; k < hi; k++)
            {
                aux[k] = array[k];
            }
            for (int k = lo; k < hi; k++)
            {
                if (i > mid)
                {
                    array[k] = aux[j++];
                }
                else if (j > hi)
                {
                    array[k] = aux[i++];
                }
                else if (aux[i].CompareTo(aux[j]) < 0)
                {
                    array[k] = aux[i++];
                }
                else
                {
                    array[k] = aux[j++];
                }
            }
        }
    }
}

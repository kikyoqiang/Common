using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class BasicSort<T> where T : IComparable<T>
    {
        private static void Swap(T[] arry, int i, int min)
        {
            T temp = arry[i];
            arry[i] = arry[min];
            arry[min] = temp;
        }
        public static void SelectionSort(T[] arry)
        {
            int n = arry.Length;
            for (int i = 0; i < n; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arry[min].CompareTo(arry[j]) > 0)
                        min = j;
                }
                Swap(arry, i, min);
            }
        }
        public static void InsertionSort(T[] arry)
        {
            int n = arry.Length;
            for (int i = 1; i < n; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (arry[j].CompareTo(arry[j - 1]) < 0)
                        Swap(arry, j, j - 1);
                }
            }
        }
        public static void SellSort(T[] arry)
        {
            int n = arry.Length;
            int h = 1;
            while (h < n / 3) h = 3 * h + 1;
            while (h >= 1)
            {
                for (int i = 1; i < n; i++)
                {
                    for (int j = i; j >= h; j = j - h)
                    {
                        if (arry[j].CompareTo(arry[j - h]) < 0)
                            Swap(arry, j, j - h);
                    }
                }
                h = h / 3;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2
{
    public class Sort<T> where T : IComparable<T>
    {
        public static void SelectionSort(T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (array[min].CompareTo(array[j]) > 0)
                    {
                        min = j;
                    }
                }
                Swap(array, i, min);
            }
        }

        public static void InsertSort(T[] array)
        {
            int n = array.Length;
            for (int i = 1; i < n; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (array[j].CompareTo(array[j - 1]) < 0)
                    {
                        Swap(array, j, j - 1);
                    }
                    else
                        break;
                }
            }
        }

        public static void ShellSort(T[] array)
        {
            int n = array.Length;
            int h = 1;
            while (h < n / 3) h = h * 3 + 1;
            while (h >= 1)
            {
                for (int i = 1; i < n; i++)
                {
                    for (int j = i; j >= h; j = j - h)
                    {
                        if (array[j].CompareTo(array[j - h]) < 0)
                        {
                            Swap(array, j, j - h);
                        }
                        else
                            break;
                    }
                }
                h = h / 3;
            }
        }

        public static void Swap(T[] array, int i, int min)
        {
            T temp = array[i];
            array[i] = array[min];
            array[min] = temp;
        }
    }
}

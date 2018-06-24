using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 优先级队列和堆排序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private static T[] array;
        private static int N;

        /// <summary>
        /// 堆排序
        /// </summary>
        /// <param name="array1"></param>
        public static void Sort(T[] array1)
        {
            array = new T[array1.Length + 1];
            N = array.Length - 1;
            for (int i = 0; i < array1.Length; i++)
            {
                array[i + 1] = array1[i];
            }
            for (int k = N / 2; k >= 1; k--)
            {
                Sink(k);
            }
            while (N > 1)
            {
                Swap(array, 1, N--);
                Sink(1);
            }
            for (int i = 1; i < array.Length; i++)
            {
                array1[i - 1] = array[i];
            }
        }

        public static void Insert(T data)
        {
            array[++N] = data;
            Swim(N);
        }
        private static void Swim(int k)
        {
            while (k > 1 && array[k].CompareTo(array[k / 2]) > 0)
            {
                Swap(array, k, k / 2);
                k = k / 2;
            }
        }

        public static T DelMax()
        {
            T max = array[1];
            Swap(array, 1, N--);
            Sink(1);
            array[N + 1] = default(T);
            return max;
        }
        private static void Sink(int k)
        {
            while (2 * k < N)
            {
                int j = 2 * k;
                if (array[j].CompareTo(array[j + 1]) < 0)
                    j++;
                if (array[k].CompareTo(array[j]) > 0)
                    break;
                Swap(array, k, j);
                k = j;
            }
        }

        private static void Swap(T[] array, int a, int b)
        {
            T temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }
    }
}

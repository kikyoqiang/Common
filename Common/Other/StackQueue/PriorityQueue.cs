using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>优先级队列</summary>
    public class PriorityQueue2<T> where T : IComparable<T>
    {
        static T[] pq = null;
        static int N;
        public static void Insert(T s)
        {
            //将元素添加到数组末尾
            pq[++N] = s;
            //然后让该元素从下至上重建堆
            Swim(N);
        }
        private static void Swim(int k)
        {
            //如果元素比其父元素大，则交换
            while (k > 1 && pq[k].CompareTo(pq[k / 2]) > 0)
            {
                Swap(pq, k, k / 2);
                k = k / 2;
            }
        }

        public static T DelMax()
        {
            //根元素从1开始，0不存放值
            T max = pq[1];
            //将最后一个元素和根节点元素进行交换
            Swap(pq, 1, N--);
            //对根节点从上至下重新建堆
            Sink(1);
            //将最后一个元素置为空
            pq[N + 1] = default(T);
            return max;
        }
        private static void Sink(int k)
        {
            while (2 * k < N)
            {
                int j = 2 * k;
                //去左右子节点中，稍大的那个元素做比较
                if (pq[j].CompareTo(pq[j + 1]) < 0) j++;
                //如果父节点比这个较大的元素还大，表示满足要求，退出
                if (pq[k].CompareTo(pq[j]) > 0) break;
                //否则，与子节点进行交换
                Swap(pq, k, j);
                k = j;
            }
        }

        private static void Swap(T[] arry, int a, int b)
        {
            T temp = arry[a];
            arry[a] = arry[b];
            arry[b] = temp;
        }
    }
}

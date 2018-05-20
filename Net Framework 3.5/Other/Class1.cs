using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Net_Framework_3._5
{
    class Class1
    {
        static void Main22()
        {
            #region MyRegion
            //string s = DESEncryptionUtilities.MD5Encrypt("1");
            //string user = DESEncryptionUtilities.Encrypt3DES("sa", DESEncryptionUtilities.sKey);
            //string pwd = DESEncryptionUtilities.Encrypt3DES("123456", DESEncryptionUtilities.sKey);
            //if (s == "C4CA4238A0B923820DCC509A6F75849B")
            //{

            //}
            //Int32[] array = new Int32[] { 1, 3, 1, 4, 2, 4, 2, 3, 2, 4, 7, 6, 6, 7, 5,4 };
            //Console.WriteLine("Before SelectionSort:");
            //PrintArray(array);
            ////Sort2<Int32>.SelectionSort(array);
            //MergeSort<Int32>.Sort(array);
            //Console.WriteLine("After SelectionSort:");
            //PrintArray(array); 
            #endregion

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint point = new IPEndPoint(ip, 1000);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(point);
            socket.Listen(20);
            Console.WriteLine("启动监听{0}成功", socket.LocalEndPoint.ToString());
            Console.WriteLine("等待客户端连接");
            Socket acceptSock = socket.Accept();

            string recvStr = "";
            byte[] recvBytes = new byte[1024];
            int bytes;
            bytes = acceptSock.Receive(recvBytes, recvBytes.Length, 0); //从客户端接受消息
            recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);

            //给Client端返回信息
            Console.WriteLine("server get message:{0}", recvStr);    //把客户端传来的信息显示出来
            string sendStr = "ok!Client send message successful!";
            byte[] bs = Encoding.ASCII.GetBytes(sendStr);
            acceptSock.Send(bs, bs.Length, 0);  //返回信息给客户端
            acceptSock.Close();
            socket.Close();
            Console.ReadKey();
        }
        static void PrintArray<T>(T[] arry)
        {
            foreach (var item in arry)
            {
                Console.WriteLine(item);
            }
        }
    }
    public class Stack<T>
    {
        class Node
        {
            public T Item { get; set; }
            public Node Next { get; set; }
        }
        private Node first = null;
        private int number = 0;
        public void Push(T data)
        {
            Node oldFirst = first;
            first = new Node();
            first.Item = data;
            first.Next = oldFirst;
            number++;
        }
        public T Pop()
        {
            T temp = first.Item;
            first = first.Next;
            number--;
            return temp;
        }
    }
    public class Stack2<T>
    {
        T[] items;
        int number = 0;
        public Stack2(int capacity)
        {
            items = new T[capacity];
        }
        public void Push(T data)
        {
            if (number == items.Length) Resize(2 * items.Length);
            items[number++] = data;
        }
        public T Pop()
        {
            T temp = items[--number];
            items[number] = default(T);
            if (number > 0 && number == items.Length / 4) Resize(items.Length / 2);
            return temp;
        }
        private void Resize(int capacity)
        {
            T[] temp = new T[capacity];
            for (int i = 0; i < items.Length; i++)
            {
                temp[i] = items[i];
            }
            items = temp;
        }
    }
    public class Queue<T>
    {
        class Node
        {
            public T item { get; set; }
            public Node Next { get; set; }
        }
        private bool IsEmpty() { return number == 0; }
        private Node first = null;
        private Node last = null;
        private int number = 0;
        public void Enqueue(T data)
        {
            Node oldLast = last;
            last = new Node();
            last.item = data;
            if (IsEmpty())
                first = last;
            else
                oldLast.Next = last;
            number++;
        }
        public T Dequeue()
        {
            T temp = first.item;
            first = first.Next;
            number--;
            if (IsEmpty())
                last = null;
            return temp;
        }
    }
    public class Queue2<T>
    {
        T[] items;
        int head;
        int tail;
        public Queue2(int capacity)
        {
            items = new T[capacity];
        }
        public void Enqueue(T data)
        {
            if ((head - tail + 1) == items.Length)
                Resize(items.Length * 2);
            items[tail++] = data;
        }
        public T Dequque()
        {
            T temp = items[--head];
            items[head] = default(T);
            if (head > 0 && (tail - head + 1) == items.Length / 4)
                Resize(items.Length / 2);
            return temp;
        }
        private void Resize(int capacity)
        {
            T[] temp = new T[capacity];
            int index = 0;
            for (int i = head; i < tail; i++)
            {
                temp[++index] = items[i];
            }
            items = temp;
        }
    }
    public class Sort2<T> where T : IComparable<T>
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
    public class MergeSort<T> where T : IComparable<T>
    {
        private static T[] aux; // 用于排序的辅助数组
        public static void Sort(T[] array)
        {
            aux = new T[array.Length]; // 仅分配一次
            Sort(array, 0, array.Length - 1);
        }
        private static void Sort(T[] array, int lo, int hi)
        {
            if (lo >= hi) return; //如果下标大于上标，则返回
            int mid = lo + (hi - lo) / 2;//平分数组
            Sort(array, lo, mid);//循环对左侧元素排序
            Sort(array, mid + 1, hi);//循环对右侧元素排序
            Merge(array, lo, mid, hi);//对左右排好的序列进行合并
        }
        private static void Merge(T[] array, int lo, int mid, int hi)
        {
            int i = lo, j = mid + 1;
            //把元素拷贝到辅助数组中
            for (int k = lo; k <= hi; k++)
            {
                aux[k] = array[k];
            }
            //然后按照规则将数据从辅助数组中拷贝回原始的array中
            for (int k = lo; k <= hi; k++)
            {
                //如果左边元素没了， 直接将右边的剩余元素都合并到到原数组中
                if (i > mid)
                {
                    array[k] = aux[j++];
                }//如果右边元素没有了，直接将所有左边剩余元素都合并到原数组中
                else if (j > hi)
                {
                    array[k] = aux[i++];
                }//如果左边右边小，则将左边的元素拷贝到原数组中
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
    public class QuickSort<T> where T : IComparable<T>
    {
        public static void Sort(T[] array)
        {
            Sort(array, 0, array.Length - 1);
        }

        private static void Sort(T[] array, int lo, int hi)
        {
            //如果子序列为1，则直接返回
            if (lo >= hi) return;
            //划分，划分完成之后，分为左右序列，左边所有元素小于array[index]，右边所有元素大于array[index]
            int index = Partition(array, lo, hi);

            //对左右子序列进行排序完成之后，整个序列就有序了
            //对左边序列进行递归排序
            Sort(array, lo, index - 1);
            //对右边序列进行递归排序
            Sort(array, index + 1, hi);
        }
        /// <summary>
        /// 快速排序中的划分过程
        /// </summary>
        /// <param name="array">待划分的数组</param>
        /// <param name="lo">最左侧位置</param>
        /// <param name="hi">最右侧位置</param>
        /// <returns>中间元素位置</returns>
        private static int Partition(T[] array, int lo, int hi)
        {
            int i = lo, j = hi + 1;
            while (true)
            {
                //从左至右扫描，如果碰到比基准元素array[lo]小，则该元素已经位于正确的分区，i自增，继续比较i+1；
                //否则，退出循环，准备交换
                while (array[++i].CompareTo(array[lo]) < 0)
                {
                    //如果扫描到了最右端，退出循环
                    if (i == hi) break;
                }

                //从右自左扫描，如果碰到比基准元素array[lo]大，则该元素已经位于正确的分区，j自减，继续比较j-1
                //否则，退出循环，准备交换
                while (array[--j].CompareTo(array[lo]) > 0)
                {
                    //如果扫描到了最左端，退出循环
                    if (j == lo) break;
                }

                //如果相遇，退出循环
                if (i >= j) break;

                //交换左a[i],a[j]右两个元素，交换完后他们都位于正确的分区
                Swap(array, i, j);
            }
            //经过相遇后，最后一次a[i]和a[j]的交换
            //a[j]比a[lo]小，a[i]比a[lo]大，所以将基准元素与a[j]交换
            Swap(array, lo, j);
            //返回扫描相遇的位置点
            return j;
        }
        private static void Swap(T[] arry, int i, int min)
        {
            T temp = arry[i];
            arry[i] = arry[min];
            arry[min] = temp;
        }
    }
}

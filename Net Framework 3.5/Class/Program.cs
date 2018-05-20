using System;
namespace Net_Framework_3._5
{
    class Program
    {
        static void Main22()
        {
            string s = DESEncryptionUtilities.MD5Encrypt("1");
            if (s == "C4CA4238A0B923820DCC509A6F75849B")
            {

            }
            //Int32[] array1 = new Int32[] { 1, 3, 1, 4, 2, 4, 2, 3, 2, 4, 7, 6, 6, 7, 5, 5, 7, 7 };
            //Console.WriteLine("Before InsertionSort:");
            //PrintArray(array1);
            //Sort<Int32>.InsertionSort(array1);
            //Console.WriteLine("After InsertionSort:");
            //PrintArray(array1);
            Console.ReadKey();
        }
        static void Reverse(int[] arry, int start, int end)
        {
            while (end > start)
            {
                int temp = arry[start];
                arry[start] = arry[end];
                arry[end] = temp;
                start++;
                end--;
            }
        }
        static void PrintArray(Int32[] PrintArray)
        {
            foreach (var item in PrintArray)
            {
                Console.WriteLine(item);
            }
        }
    }
    public class MyStack<T>
    {
        class Node
        {
            public T item { get; set; }
            public Node Next { get; set; }
        }
        private Node first = null;
        private int number = 0;
        public void Push(T data)
        {
            Node oldItem = first;
            first = new Node();
            first.item = data;
            first.Next = oldItem;
            number++;
        }
        public T Pop()
        {
            T result = first.item;
            first = first.Next;
            number--;
            return result;
        }
    }
    public class MyStack2<T>
    {
        T[] items;
        int number = 0;
        public MyStack2(int capacity)
        {
            items = new T[capacity];
        }
        private void Resize(int length)
        {
            T[] temp = new T[length];
            for (int i = 0; i < items.Length; i++)
            {
                temp[i] = items[i];
            }
            items = temp;
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
    }
    public class MyQueue<T>
    {
        class Node
        {
            public T Item { get; set; }
            public Node Next { get; set; }
        }
        private Node first = null;
        private Node last = null;
        private int number = 0;
        private bool IsEmpty() { return number == 0; }
        public void Enqueue(T data)
        {
            Node oldlast = last;
            last = new Node();
            last.Item = data;
            if (IsEmpty())
                first = last;
            else
                oldlast.Next = last;
            number++;
        }
        public T Dequeue()
        {
            T temp = first.Item;
            first = first.Next;
            number--;
            if (IsEmpty())
                last = null;
            return temp;
        }
    }
    public class MyQueue2<T>
    {
        T[] items;
        private int head = 0;
        private int tail = 1;
        public MyQueue2(int capacity)
        {
            items = new T[capacity];
        }
        public void Enqueue(T data)
        {
            if ((head - tail + 1) == items.Length) Resize(2 * items.Length);
            items[tail++] = data;
        }
        public T Dequeue()
        {
            T temp = items[--head];
            items[head] = default(T);
            if (head > 0 && (tail - head + 1) == items.Length / 4) Resize(items.Length / 2);
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
    #region 排序类
    public class Sort<T> where T : IComparable<T>
    {
        #region 元素交换
        private static void Swap(T[] arry, int i, int min)
        {
            T temp = arry[i];
            arry[i] = arry[min];
            arry[min] = temp;
        }
        #endregion
        #region 选择排序
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
        #endregion
        #region 插入排序
        public static void InsertionSort(T[] arry)
        {
            int n = arry.Length;
            for (int i = 1; i < n; i++)
            {
                int min = i;
                for (int j = i; j > 0; j--)
                {
                    if (arry[j].CompareTo(arry[j - 1]) < 0)
                        Swap(arry, j, j - 1);
                    else
                        break;
                }
            }
        }
        #endregion
    }
    #endregion
}

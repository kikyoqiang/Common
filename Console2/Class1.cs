using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2
{
    class Class1
    {
        static void Main()
        {
            Int32[] array1 = new int[] { 5, 1, 9, 4, 3, 0, 2 };//new Int32[] { 1, 3, 1, 4, 2, 4, 2, 3, 2, 4, 7, 6, 6, 7, 5, 5, 7, 7 };
            Console.WriteLine("Before InsertionSort:");
            PrintArray(array1);
            //Sort<Int32>.ShellSort(array1);
            //MergeSort<int>.Sort(array1);
            PriorityQueue<int>.Sort(array1);
            Console.WriteLine("After InsertionSort:");
            PrintArray(array1);
            Console.ReadKey();
        }
        static void PrintArray(int[] array)
        {
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }
    }
}

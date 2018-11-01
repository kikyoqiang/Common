using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CLR_Via
{
    class Program
    {
        static void Main222(string[] args)
        {
            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //string s1 = "visualStudio";
            //string s2 = "Windows";
            //Console.WriteLine(string.Compare(s1, s2, StringComparison.CurrentCulture));
            //string a = "A";
            //var b1 = a.Equals("a", StringComparison.OrdinalIgnoreCase);
            //var b2 = a.Equals("a");
            //a.ToUpperInvariant();
            //var a = Math.Sign('A');
            //string a = "a";
            //var a1 = string.Intern(a);
            //var a2 = string.IsInterned("B");

            //string s1 = "Hello";
            //string s2 = "Hello";

            //Console.WriteLine(string.ReferenceEquals(s1, s2));

            //s1 = string.Intern(s1);
            //s2 = string.Intern(s2);
            //Console.WriteLine(string.ReferenceEquals(s1, s2));

            //IFormattable a;
            //double d = 3.0261;
            //var d1 = string.Format("{0:f2}", d);
            //Console.WriteLine(d1);

            //DateTimeFormatInfo info = new DateTimeFormatInfo();
            //decimal price = 123.4M;
            //string s = price.ToString("C", CultureInfo.InvariantCulture);
            //Console.WriteLine(s);

            //string a = " 122";
            //var a1 = int.Parse(a, NumberStyles.AllowLeadingWhite);

            //int a2 = int.Parse("1A", NumberStyles.HexNumber, null);
            //Console.WriteLine(a2);

            //string s = "hi here !";
            //Encoding utf8 = Encoding.UTF8;
            //byte[] bytes = utf8.GetBytes(s);
            //Console.WriteLine("bytes {0} ", BitConverter.ToString(bytes));


            //string s1 = utf8.GetString(bytes);

            //Color c = Color.Green;

            //Console.WriteLine(c.ToString());
            //Console.WriteLine(c.ToString("G"));
            //Console.WriteLine(c.ToString("D"));
            //Console.WriteLine(c.ToString("X"));

            //Console.WriteLine(Enum.Format(typeof(Color), (byte)4, "G"));
            //Color c1 = (Color)Enum.Parse(typeof(Color), "3");
            //Console.WriteLine(c1);

            //Console.WriteLine(Enum.GetUnderlyingType(typeof(Color)));

            //Color[] colors = (Color[])Enum.GetValues(typeof(Color));
            //Color[] colors2 = GetEnumValues<Color>();
            //foreach (var item in colors2)
            //{
            //    Console.WriteLine("{0,5:D} \t {0:G}", item);
            //    Console.WriteLine("{0} \t {0:G}", item);
            //    Console.WriteLine();
            //    Console.WriteLine();
            //}

            //Color c = (Color)Enum.Parse(typeof(Color), "White", true);

            //Console.WriteLine(Enum.IsDefined(typeof(Color), 4));


            //string file = Assembly.GetEntryAssembly().Location;
            //FileAttributes attributes = File.GetAttributes(file);
            //Console.WriteLine("Is {0} hidden? {1}", file, (attributes & FileAttributes.Hidden) != 0);
            //File.SetAttributes(file, FileAttributes.Hidden | FileAttributes.ReadOnly);

            //Actions action = Actions.Read | Actions.Delete;
            //Console.WriteLine(action.ToString("F"));

            //Actions a = (Actions)Enum.Parse(typeof(Actions), "Query", true);
            //Console.WriteLine(a.ToString());

            //Enum.TryParse<Actions>("Query,Read", out a);
            //Console.WriteLine(a.ToString("F"));

            //a = (Actions)Enum.Parse(typeof(Actions), "28");
            //Console.WriteLine(a.ToString("F"));

            //var a = 6 << 1;
            //var b = 6 >> 1;
            //Console.WriteLine(a);
            //Console.WriteLine(b);

            //string[] names = { "1", "2", "3", "4" };

            //var kids = new[] { new { Name = "a" }, new { Name = "b" } };
            //foreach (var item in kids)
            //{
            //    Console.WriteLine(item.Name);
            //}

            //FileStream[,] fs2dim = new FileStream[5, 10];

            //object[,] o2dim = fs2dim;

            //Stream[,] s2dim = (Stream[,])o2dim;

            ////string[,] st2dim = (string[,])o2dim;

            //int[] idim = new int[5];

            ////object[] oldim = (object[])idim;

            //object[] obdim = new object[idim.Length];
            //Array.Copy(idim, obdim, idim.Length);

            //System.Buffer.BlockCopy();

            //Array a = new string[] { };

            //int[] lowerBounds = { 2005, 1 };
            //int[] lengths = { 5, 4 };
            //decimal[,] quarterlyRevenve = (decimal[,])Array.CreateInstance(typeof(decimal), lengths, lowerBounds);
            //Console.WriteLine("{0,4} {1,9} {2,9} {3,9} {4,9} ", "year", "Q1", "Q2", "Q3", "Q4");
            //int firstYear = quarterlyRevenve.GetLowerBound(0);
            //int lastYear = quarterlyRevenve.GetUpperBound(0);
            //int firstQuarter = quarterlyRevenve.GetLowerBound(1);
            //int lastQuarter = quarterlyRevenve.GetUpperBound(1);
            //for (int year = firstYear; year <= lastYear; year++)
            //{
            //    Console.Write(year + " ");
            //    for (int quarter = firstQuarter; quarter <= lastQuarter; quarter++)
            //    {
            //        Console.Write("{0,9:C} ", quarterlyRevenve[year, quarter]);
            //    }
            //    Console.WriteLine();
            //}

            //Array a;
            //a = Array.CreateInstance(typeof(string), new int[] { 1 }, new int[] { 2 });
            //Console.WriteLine(a.GetType());

            Console.ReadKey();
        }
    }
    public static class FileAttributesExtensionMethods
    {
        public static bool IsSet(this FileAttributes flags, FileAttributes flagToTest)
        {
            if (flagToTest == 0)
                throw new ArgumentOutOfRangeException("Value must not be 0");
            return (flags & flagToTest) == flagToTest;
        }
        public static bool IsClear(this FileAttributes flags, FileAttributes flagToTest)
        {
            return !IsSet(flags, flagToTest);
        }
        public static FileAttributes AnyFlagSet(this FileAttributes flags, FileAttributes flagToTest)
        {
            return flags | flagToTest;
        }
        public static FileAttributes Clear(this FileAttributes flags, FileAttributes flagToTest)
        {
            return flags & ~flagToTest;
        }
        public static void ForEach(this FileAttributes flags, Action<FileAttributes> processFlag)
        {
            if (processFlag == null)
                throw new ArgumentNullException("processFlag");
            for (int bit = 0; bit != 0; bit = bit << 1)
            {
                int temp = ((int)flags) & bit;
                if (temp != 0)
                    processFlag((FileAttributes)temp);
            }
            //for (int bit = 0; bit != 0; bit <<= 1)
            //{
            //    int temp = ((int)flags) & bit;
            //    if (temp != 0)
            //        processFlag((FileAttributes)temp);
            //}
        }
    }
    public enum Actions
    {
        None = 0,
        Read = 0x0001,
        Write = 0x0002,
        ReadWrite = Actions.Read | Actions.Write,
        Delete = 0x0004,
        Query = 0x0008,
        Sync = 0x0010
    }
    public enum Color
    {
        White = 1,
        Red = 2,
        Green = 4,
        Blue = 8,
        Orange = 16
    }
    public class A : Base
    {
        public override int CompareTo(object obj)
        {
            Console.WriteLine("A CompareTO");
            return base.CompareTo(obj);
        }
    }
    public class Base : IComparable
    {
        int IComparable.CompareTo(object obj)
        {
            Console.WriteLine("Base's IComparable.CompareTo");
            return CompareTo(obj);
        }
        public virtual int CompareTo(object obj)
        {
            Console.WriteLine("Base's virtual CompareTo");
            return 0;
        }
    }

    internal struct SomeValueType : IComparable
    {
        private int _x;
        public SomeValueType(int x)
        {
            _x = x;
        }
        public int CompareTo(SomeValueType other)
        {
            return (-_x - other._x);
        }
        int IComparable.CompareTo(object obj)
        {
            return CompareTo((SomeValueType)obj);
        }
    }
    public class SimpleType : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("public Dispose");
        }
        void IDisposable.Dispose()
        {
            Console.WriteLine("IDisposable Dispose");
        }
    }
}

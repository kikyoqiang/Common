using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console4
{
    class Program
    {
        static void Main11(string[] args)
        {
            ObservableCollection<string> list = new ObservableCollection<string>() { "1" };

            list.CollectionChanged += list_CollectionChanged;

            for (int i = 0; i < 1000; i++)
            {
                if (i % 3 == 1)
                {
                    list.RemoveAt(0);
                }
                else
                {
                    list.Add(i.ToString());
                }
            }

            Console.WriteLine("全部结束！！！");

            Console.Read();
        }
        static void list_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //为了不阻止主线程Add，事件用 “工作线程”处理
            Task.Factory.StartNew((o) =>
            {
                var obj = o as NotifyCollectionChangedEventArgs;

                switch (obj.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        Console.WriteLine("当前线程:{0}, 操作是:{1} 数据:{2}", Thread.CurrentThread.ManagedThreadId, obj.Action.ToString(), obj.NewItems[0]);
                        break;
                    case NotifyCollectionChangedAction.Move:
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        Console.WriteLine("当前线程:{0}, 操作是:{1} 数据:{2}", Thread.CurrentThread.ManagedThreadId, obj.Action.ToString(), obj.OldItems[0]);
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        break;
                    default:
                        break;
                }

                Thread.Sleep(1000);
            }, e);
        }
    }
}

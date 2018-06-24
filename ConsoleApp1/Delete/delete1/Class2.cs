using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
 

namespace ConsoleApp1
{
    class Class2
    {
        static void Main22(string[] args)
        {
            #region MyRegion
            //PurchaseRequest requestTelphone = new PurchaseRequest(4000.0, "Telphone");
            //PurchaseRequest requestSoftware = new PurchaseRequest(10000.0, "Visual Studio");
            //PurchaseRequest requestComputers = new PurchaseRequest(40000.0, "Computers");

            //Approver manager = new Manager("LearningHard");
            //Approver Vp = new VicePresident("Tony");
            //Approver Pre = new President("BossTom");

            //// 设置责任链
            //manager.NextApprover = Vp;
            //Vp.NextApprover = Pre;

            //// 处理请求
            //manager.ProcessRequest(requestTelphone);
            //manager.ProcessRequest(requestSoftware);
            //manager.ProcessRequest(requestComputers); 
            #endregion

            int port = 1000;
            string host = "127.0.0.1";
            //创建终结点EndPoint
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);   //把ip和端口转化为IPEndPoint的实例

            //创建Socket并连接到服务器
            Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);   //  创建Socket
            Console.WriteLine("Connecting...");
            c.Connect(ipe); //连接到服务器

            //向服务器发送信息
            string sendStr = "Hello,this is a socket test";
            byte[] bs = Encoding.ASCII.GetBytes(sendStr);   //把字符串编码为字节

            Console.WriteLine("Send message");
            c.Send(bs, bs.Length, 0); //发送信息

            //接受从服务器返回的信息
            string recvStr = "";
            byte[] recvBytes = new byte[1024];
            int bytes;
            bytes = c.Receive(recvBytes, recvBytes.Length, 0);    //从服务器端接受返回信息
            recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);
            Console.WriteLine("client get message:{0}", recvStr);    //回显服务器的返回信息

            Console.ReadLine();
            //一定记着用完Socket后要关闭
            c.Close();

            Console.ReadLine();
        }
    }
    public class PurchaseRequest
    {
        public double Amount { get; set; }
        public string ProductName { get; set; }
        public PurchaseRequest(double amount, string productName)
        {
            this.Amount = amount;
            this.ProductName = productName;
        }
    }
    public abstract class Approver
    {
        public Approver NextApprover { get; set; }
        public string Name { get; set; }
        public Approver(string name)
        {
            this.Name = name;
        }
        public abstract void ProcessRequest(PurchaseRequest request);
    }
    public class Manager : Approver
    {
        public Manager(string name) : base(name)
        {
        }

        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 10000.0)
            {
                Console.WriteLine("{0}-{1} approved the request of purshing {2}", this, Name, request.ProductName);
            }
            else if (NextApprover != null)
            {
                NextApprover.ProcessRequest(request);
            }
        }
    }
    public class VicePresident : Approver
    {
        public VicePresident(string name) : base(name)
        {
        }

        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 25000.0)
            {
                Console.WriteLine("{0}-{1} approved the request of purshing {2}", this, Name, request.ProductName);
            }
            else if (NextApprover != null)
            {
                NextApprover.ProcessRequest(request);
            }
        }
    }
    // ConcreteHandler，总经理
    public class President : Approver
    {
        public President(string name)
            : base(name)
        { }
        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 100000.0)
            {
                Console.WriteLine("{0}-{1} approved the request of purshing {2}", this, Name, request.ProductName);
            }
            else
            {
                Console.WriteLine("Request需要组织一个会议讨论");
            }
        }
    }
}

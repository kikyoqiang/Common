using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console4
{
    public abstract class TenXun
    {
        private List<IOBServer> list = new List<IOBServer>();
        public string Subject { get; set; }
        public string Info { get; set; }
        public TenXun(string subject, string info)
        {
            this.Subject = subject;
            this.Info = info;
        }
        public void AddObserver(IOBServer ob)
        {
            list.Add(ob);
        }
        public void Remove(IOBServer ob)
        {
            list.Add(ob);
        }
        public void Update()
        {
            foreach (var item in list)
            {
                item.ReceiveAndPrint(this);
            }
        }
    }
    public interface IOBServer
    {
        void ReceiveAndPrint(TenXun tenxun);
    }
    public class OB : IOBServer
    {
        public string Name { get; set; }
        public OB(string Name)
        {
            this.Name = Name;
        }
        public void ReceiveAndPrint(TenXun tenxun)
        {
            Console.WriteLine(string.Format("{0} {1} {2}", Name, tenxun.Subject, tenxun.Info));
        }
    }
    public class TenXun1 : TenXun
    {
        public TenXun1(string subject, string info) : base(subject, info)
        {
        }
    }
}

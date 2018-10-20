using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IFM_Form
{
    public partial class Form4 : Form
    {
        private object locker = new object();
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                int a = i;
                ThreadPool.QueueUserWorkItem(obj =>
                {
                    Thread.Sleep(random.Next(0, 10) * 1000);
                    LogHelper.Instance.WriteInfo(string.Format("当前线程ID:{0} 数字为:{1}", Thread.CurrentThread.ManagedThreadId, a));
                    SetNumber(a);
                });
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void SetNumber(int number)
        {
            BeginInvoke(new Action(() =>
            {
                lock (locker)
                {
                    button1.Text = (int.Parse(button1.Text) + number).ToSafeString();
                    //button1.Text = ( number).ToSafeString();
                    Application.DoEvents();
                }
            }));
        }
    }
}

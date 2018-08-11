using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IFM_Form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(obj =>
            {
                for (int i = 0; i < 100; i++)
                {
                    System.Threading.Thread.Sleep(2000);
                    Common.LogHelper.Instance.WriteInfo(i.ToSafeString());
                }
            });
        }
    }
}

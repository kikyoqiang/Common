using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionFun;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 10000; i++)
            //{
            //    label1.Text = i.ToString();
            //}
            object o = null;
            o.ToString();
        }
        private void UnhandledExceptionFun(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show(string.Format("出现错误 {0} \r\n {1}", ex.Message, ex.StackTrace));
            return;
        }
    }
}

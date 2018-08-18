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
        public event Func<object, EventArgs, Action> ClickLoading;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ClickLoading == null)
                return;
            var actions = ClickLoading.GetInvocationList();
            var action_0 = actions[0];
            ((Func<object, EventArgs, Action>)action_0).BeginInvoke(this, e, new AsyncCallback(r =>
            {
                Action callback = ((Func<object, EventArgs, Action>)ClickLoading.GetInvocationList()[0]).EndInvoke(r);

                this.BeginInvoke(new Action(() =>
                {
                    //panel1.Visible = false;
                    callback?.Invoke();
                }));
            }), null);
        }
    }
}

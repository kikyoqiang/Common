using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        public event Func<object, EventArgs, Action> ClickLoading;

        public delegate Task ClickTaskAsync(object sender, EventArgs e);
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            if (ClickLoading == null)
                return;
            Delegate[] actions = ClickLoading.GetInvocationList();
            List<Task> listTasks = new List<Task>();
            for (int i = 0; i < actions.Length; i++)
            {
                Func<object, EventArgs, Action> func = (Func<object, EventArgs, Action>)actions[i];

                var a = Task.Factory.StartNew(() =>
                {
                    Action callBack = func(sender, e);

                    this.BeginInvoke(new Action(() =>
                    {
                        callBack?.Invoke();
                    }));
                });
            }

            Task.Factory.StartNew(() =>
            {
                Task.WaitAll(listTasks.ToArray());
                this.BeginInvoke(new Action(() =>
                {
                    panel1.Visible = false;
                }));
            });
        }

        private void btn_BeginInvoke_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            if (ClickLoading == null)
                return;
            var actions = ClickLoading.GetInvocationList();
            var action_0 = actions[0];
            ((Func<object, EventArgs, Action>)action_0).BeginInvoke(this, e, new AsyncCallback(r =>
            {
                Action callback = ((Func<object, EventArgs, Action>)ClickLoading.GetInvocationList()[0]).EndInvoke(r);

                this.BeginInvoke(new Action(() =>
                {
                    panel1.Visible = false;
                    callback?.Invoke();
                }));
            }), null);
        }

        private void btn_Task_await_Click(object sender, EventArgs e)
        {

        }
        private async void ExecuteClickTaskAnsync(object sender, EventArgs e)
        {
            await ClickAnsyncLoading(sender, e);
        }
        private Task ClickAnsyncLoading(object sender, EventArgs e)
        {
            return new Task();
        }
    }
}

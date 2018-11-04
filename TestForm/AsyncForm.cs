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
    public partial class AsyncForm : Form
    {
        Label lable;
        Button button;
        public AsyncForm()
        {
            InitializeComponent();
            lable = new Label { Location = new Point(10, 20), Text = "Length" };
            button = new Button { Location = new Point(10, 50), Text = "Click" };

        }

    }
}

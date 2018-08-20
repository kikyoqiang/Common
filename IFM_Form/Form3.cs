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
    public partial class Form3 : Form
    {
        public event Func<object, EventArgs, Action> ClickLoading;
        public Form3()
        {
            InitializeComponent();
        }
    }
}

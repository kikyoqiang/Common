using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace RestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            int endNum = int.Parse(ConfigurationManager.AppSettings["EndNum"]);
            int showTime = int.Parse(ConfigurationManager.AppSettings["ShowTime"]);
            this.timer1.Interval = showTime;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            int jpgNum = new Random().Next(1, endNum);
            string path = string.Format(@"{0}\休息图片\{1}.jpg", baseDirectory, jpgNum);
            if (File.Exists(path))
                this.pictureBox1.BackgroundImage = Image.FromFile(path);
            this.TopMost = true;
          
            // 初始化画板 Bitmap image = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            // 获取背景层 Bitmap bg = (Bitmap)pictureBox1.BackgroundImage;
            // 初始化画布 Bitmap canvas = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            // 初始化图形面板 Graphics g = Graphics.FromImage(image); Graphics gb = Graphics.FromImage(canvas);
            // 绘图部分 Begin // ... ... // 绘图部分 End
            //gb.DrawImage(bg, 0, 0); // 先绘制背景层 gb.DrawImage(image, 0, 0); // 再绘制绘画层
            //pictureBox1.BackgroundImage = canvas; // 设置为背景层
            //pictureBox1.Refresh(); pictureBox1.CreateGraphics().DrawImage(canvas, 0, 0);


            //Bitmap image = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            //Bitmap bg = (Bitmap)pictureBox1.BackgroundImage;
            //Bitmap canvas = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            //Graphics g = Graphics.FromImage(image);
            //Graphics gb = Graphics.FromImage(canvas);
            //gb.DrawImage(bg, 0, 0);
            ////pictureBox1.BackgroundImage = canvas;
            //pictureBox1.Image = canvas;
            //pictureBox1.Refresh();
            //pictureBox1.CreateGraphics().DrawImage(canvas, 0, 0);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

namespace WindowsForms
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Task = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_BeginInvoke = new System.Windows.Forms.Button();
            this.btn_Task_await = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Task
            // 
            this.btn_Task.Location = new System.Drawing.Point(189, 132);
            this.btn_Task.Name = "btn_Task";
            this.btn_Task.Size = new System.Drawing.Size(75, 48);
            this.btn_Task.TabIndex = 0;
            this.btn_Task.Text = "Task";
            this.btn_Task.UseVisualStyleBackColor = true;
            this.btn_Task.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(189, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 100);
            this.panel1.TabIndex = 1;
            // 
            // btn_BeginInvoke
            // 
            this.btn_BeginInvoke.Location = new System.Drawing.Point(291, 132);
            this.btn_BeginInvoke.Name = "btn_BeginInvoke";
            this.btn_BeginInvoke.Size = new System.Drawing.Size(93, 48);
            this.btn_BeginInvoke.TabIndex = 2;
            this.btn_BeginInvoke.Text = "BeginInvoke";
            this.btn_BeginInvoke.UseVisualStyleBackColor = true;
            this.btn_BeginInvoke.Click += new System.EventHandler(this.btn_BeginInvoke_Click);
            // 
            // btn_Task_await
            // 
            this.btn_Task_await.Location = new System.Drawing.Point(403, 132);
            this.btn_Task_await.Name = "btn_Task_await";
            this.btn_Task_await.Size = new System.Drawing.Size(75, 48);
            this.btn_Task_await.TabIndex = 3;
            this.btn_Task_await.Text = "Task_await";
            this.btn_Task_await.UseVisualStyleBackColor = true;
            this.btn_Task_await.Click += new System.EventHandler(this.btn_Task_await_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("黑体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(86, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "哈哈";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 571);
            this.Controls.Add(this.btn_Task_await);
            this.Controls.Add(this.btn_BeginInvoke);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Task);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Task;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_BeginInvoke;
        private System.Windows.Forms.Button btn_Task_await;
        private System.Windows.Forms.Label label1;
    }
}


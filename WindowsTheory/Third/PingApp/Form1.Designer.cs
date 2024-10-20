namespace PingApp
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSyncPing = new System.Windows.Forms.Button();
            this.btnAsyncPing = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(46, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(231, 25);
            this.textBox1.TabIndex = 0;
            // 
            // btnSyncPing
            // 
            this.btnSyncPing.Location = new System.Drawing.Point(46, 130);
            this.btnSyncPing.Name = "btnSyncPing";
            this.btnSyncPing.Size = new System.Drawing.Size(75, 23);
            this.btnSyncPing.TabIndex = 1;
            this.btnSyncPing.Text = "同步";
            this.btnSyncPing.UseVisualStyleBackColor = true;
            this.btnSyncPing.Click += new System.EventHandler(this.btnSyncPing_Click);
            // 
            // btnAsyncPing
            // 
            this.btnAsyncPing.Location = new System.Drawing.Point(202, 130);
            this.btnAsyncPing.Name = "btnAsyncPing";
            this.btnAsyncPing.Size = new System.Drawing.Size(75, 23);
            this.btnAsyncPing.TabIndex = 2;
            this.btnAsyncPing.Text = "异步";
            this.btnAsyncPing.UseVisualStyleBackColor = true;
            this.btnAsyncPing.Click += new System.EventHandler(this.btnAsyncPing_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(349, 47);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(404, 338);
            this.textBox2.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btnAsyncPing);
            this.Controls.Add(this.btnSyncPing);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSyncPing;
        private System.Windows.Forms.Button btnAsyncPing;
        private System.Windows.Forms.TextBox textBox2;
    }
}


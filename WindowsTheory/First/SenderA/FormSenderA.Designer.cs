namespace SenderA
{
    partial class FormSenderA
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
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtTargetWindow = new System.Windows.Forms.TextBox();
            this.btnSend1 = new System.Windows.Forms.Button();
            this.btnSend2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(351, 47);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(100, 25);
            this.txtMessage.TabIndex = 0;
            // 
            // txtTargetWindow
            // 
            this.txtTargetWindow.Location = new System.Drawing.Point(106, 47);
            this.txtTargetWindow.Name = "txtTargetWindow";
            this.txtTargetWindow.Size = new System.Drawing.Size(100, 25);
            this.txtTargetWindow.TabIndex = 1;
            // 
            // btnSend1
            // 
            this.btnSend1.Location = new System.Drawing.Point(200, 129);
            this.btnSend1.Name = "btnSend1";
            this.btnSend1.Size = new System.Drawing.Size(106, 23);
            this.btnSend1.TabIndex = 2;
            this.btnSend1.Text = "触发消息1";
            this.btnSend1.UseVisualStyleBackColor = true;
            this.btnSend1.Click += new System.EventHandler(this.btnSend1_Click);
            // 
            // btnSend2
            // 
            this.btnSend2.Location = new System.Drawing.Point(200, 173);
            this.btnSend2.Name = "btnSend2";
            this.btnSend2.Size = new System.Drawing.Size(106, 23);
            this.btnSend2.TabIndex = 3;
            this.btnSend2.Text = "触发消息2";
            this.btnSend2.UseVisualStyleBackColor = true;
            this.btnSend2.Click += new System.EventHandler(this.btnSend2_Click);
            // 
            // FormSenderA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSend2);
            this.Controls.Add(this.btnSend1);
            this.Controls.Add(this.txtTargetWindow);
            this.Controls.Add(this.txtMessage);
            this.Name = "FormSenderA";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtTargetWindow;
        private System.Windows.Forms.Button btnSend1;
        private System.Windows.Forms.Button btnSend2;
    }
}


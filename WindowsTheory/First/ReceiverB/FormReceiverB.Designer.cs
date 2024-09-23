namespace ReceiverB
{
    partial class FormReceiverB
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
            this.lsvMsgList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lsvMsgList
            // 
            this.lsvMsgList.HideSelection = false;
            this.lsvMsgList.Location = new System.Drawing.Point(70, 22);
            this.lsvMsgList.Name = "lsvMsgList";
            this.lsvMsgList.Size = new System.Drawing.Size(661, 393);
            this.lsvMsgList.TabIndex = 0;
            this.lsvMsgList.UseCompatibleStateImageBehavior = false;
            this.lsvMsgList.View = System.Windows.Forms.View.List;
            // 
            // FormReceiverB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lsvMsgList);
            this.Name = "FormReceiverB";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvMsgList;
    }
}


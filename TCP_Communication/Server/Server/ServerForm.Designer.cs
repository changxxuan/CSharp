namespace Server
{
    partial class ServerForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.startServerButton = new System.Windows.Forms.Button();
            this.showMsgRichTextBox = new System.Windows.Forms.RichTextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.sendTextBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startServerButton
            // 
            this.startServerButton.Location = new System.Drawing.Point(140, 22);
            this.startServerButton.Name = "startServerButton";
            this.startServerButton.Size = new System.Drawing.Size(126, 38);
            this.startServerButton.TabIndex = 0;
            this.startServerButton.Text = "启动服务器";
            this.startServerButton.UseVisualStyleBackColor = true;
            this.startServerButton.Click += new System.EventHandler(this.startServerButton_Click);
            // 
            // showMsgRichTextBox
            // 
            this.showMsgRichTextBox.Location = new System.Drawing.Point(12, 91);
            this.showMsgRichTextBox.Name = "showMsgRichTextBox";
            this.showMsgRichTextBox.Size = new System.Drawing.Size(339, 157);
            this.showMsgRichTextBox.TabIndex = 1;
            this.showMsgRichTextBox.Text = "";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(374, 159);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(63, 31);
            this.clearButton.TabIndex = 0;
            this.clearButton.Text = "清空";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // sendTextBox
            // 
            this.sendTextBox.Location = new System.Drawing.Point(12, 297);
            this.sendTextBox.Multiline = true;
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.Size = new System.Drawing.Size(300, 69);
            this.sendTextBox.TabIndex = 2;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(335, 313);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(63, 31);
            this.sendButton.TabIndex = 0;
            this.sendButton.Text = "发送";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 405);
            this.Controls.Add(this.sendTextBox);
            this.Controls.Add(this.showMsgRichTextBox);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.startServerButton);
            this.Name = "ServerForm";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startServerButton;
        private System.Windows.Forms.RichTextBox showMsgRichTextBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.TextBox sendTextBox;
        private System.Windows.Forms.Button sendButton;
    }
}


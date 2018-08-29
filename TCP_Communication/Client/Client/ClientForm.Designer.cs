namespace Client
{
    partial class ClientForm
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
            this.codeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.showMsgRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dcodeTextBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.sendTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // codeTextBox
            // 
            this.codeTextBox.Location = new System.Drawing.Point(144, 46);
            this.codeTextBox.Multiline = true;
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(100, 21);
            this.codeTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "本地code:";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(316, 33);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(97, 34);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "连接服务器";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(450, 181);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(79, 31);
            this.clearButton.TabIndex = 2;
            this.clearButton.Text = "清空";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // showMsgRichTextBox
            // 
            this.showMsgRichTextBox.Location = new System.Drawing.Point(12, 86);
            this.showMsgRichTextBox.Name = "showMsgRichTextBox";
            this.showMsgRichTextBox.Size = new System.Drawing.Size(418, 236);
            this.showMsgRichTextBox.TabIndex = 3;
            this.showMsgRichTextBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 358);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "目的code:";
            // 
            // dcodeTextBox
            // 
            this.dcodeTextBox.Location = new System.Drawing.Point(77, 355);
            this.dcodeTextBox.Multiline = true;
            this.dcodeTextBox.Name = "dcodeTextBox";
            this.dcodeTextBox.Size = new System.Drawing.Size(83, 21);
            this.dcodeTextBox.TabIndex = 0;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(450, 349);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(79, 31);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "发送";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // sendTextBox
            // 
            this.sendTextBox.Location = new System.Drawing.Point(185, 340);
            this.sendTextBox.Multiline = true;
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.Size = new System.Drawing.Size(245, 62);
            this.sendTextBox.TabIndex = 0;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 426);
            this.Controls.Add(this.showMsgRichTextBox);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dcodeTextBox);
            this.Controls.Add(this.sendTextBox);
            this.Controls.Add(this.codeTextBox);
            this.Name = "ClientForm";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientForm_FormClosing);
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox codeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.RichTextBox showMsgRichTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dcodeTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox sendTextBox;
    }
}


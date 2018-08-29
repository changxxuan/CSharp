using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;


namespace Auto_COM
{
    public partial class MainForm : Form
    {
        private List<Button> buttons = new List<Button>();
        private List<Checker> checkers = new List<Checker>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                DrawButtonAndLabel();//绘制控件
                sendCommandButton.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //绘制控件（串口控件+设备控件）
        private void DrawButtonAndLabel()
        {
            buttons.Clear();
            checkers.Clear();
            int i = 0;
            foreach (string port in SerialPort.GetPortNames())
            {
                i++;
                Button button = new Button();
                button.Text = port;
                button.Size = new Size(70, 30);
                button.Font = new Font("宋体", 14F);
                int b = 35;
                button.Location = new Point(18, i * b);
                button.BackColor = Color.AliceBlue;

                buttons.Add(button);
                button.Visible = true;
                this.Controls.Add(button);
                button.Click += ButtonClick;
                groupBox1.SendToBack();

                Label label = new Label();
                label.Font = new Font("宋体", 10F);
                label.Location = new Point(120, i * 36);
                label.Width = 405;
                label.Height = 30;

                this.Controls.Add(label);
                groupBox1.SendToBack();

                Checker checker = new Checker();
                checker.Received += Checker_Received;
                checker.Sent += Checker_Sent;
                checker.Tag = label;
                button.Tag = checker;
                checkers.Add(checker);
                checker.Open(port, 9600);
                numLabel.Text = "总共" + i + "个串口";
            }
        }

        //串口按钮单击生成配件的label
        private void ButtonClick(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                //checkers
                Checker checker = (Checker)button.Tag;
                checker.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Checker_Received(object sender, CancelEventArgs e, string text)
        {
		    if(this.InvokeRequired)
		    {
			    this.Invoke(new Action<object,CancelEventArgs,string>(Checker_Received),sender,e,text);
			    return;
		    }
		    try
		    {
			    Checker checker = (Checker)sender;
			    Label label = (Label)checker.Tag;
			    if(text.ToUpper().Contains("ERR"))
			    {
				    e.Cancel = false;
				    return;
			    }
			    DateTime time = DateTime.Now;
			    string strTime = string.Format("[{0:yyyy-MM-dd HH:mm:ss}.{1}]", time, time.Millisecond);
			    if(text.Contains("Switcher"))
			    {
				    text = "【继电器切换板】" + "\r\n" +"【返回值】：" +text;
			    }
			    if(text.Contains("LED"))
			    {
				    text = "【通用LED测试板】" + "\r\n" +"【返回值】：" +text;
			    }
			    if(text.Contains("Elec"))
			    {
				    text = "【工装气动控制板】" + "\r\n" +"【返回值】：" +text;
			    }
			    if(text.Contains("DPpower"))
			    {
				    text = "【自制程控电源】" + "\r\n" +"【返回值】：" +text;
			    }
			    if(text.Contains("CVmeter"))
			    {
				    text = "【数控电压电流表】" + "\r\n" +"【返回值】：" +text;
			    }
			    if(text.Contains("OHMmeter"))
			    {
				    text = "【电阻测试仪】" + "\r\n" +"【返回值】：" +text;
			    }
			    if(text.Contains("Itech"))
			    {
				    text = "【IT6121B+】" + "\r\n" +"【返回值】：" +text;
			    }
			    if(text.Contains("5952B"))
			    {
				    text = "【TESCOM屏蔽箱】" + "\r\n" +"【返回值】：" +text;
			    }
			    if(text.Contains("E3631A"))
			    {
				    text = "【AgE3631A】" + "\r\n" +"【返回值】：" +text;
			    }
			    if(text.Contains("34970A"))
			    {
				    text = "【34970A】" + "\r\n" +"【返回值】：" +text;
			    }
			    if(text.Contains("model")|| text.Contains("*IDN"))
			    {
				    text = "未连接设备";
			    }
			    string sendStr = strTime + "[" + checker.SerialPort.PortName + "][接收]" + text + "\r\n";
			    string sendStr1 = "【" + checker.SerialPort.PortName + "】" + text;
			    label.Text = sendStr1;
		    }
		    catch(Exception ex)
		    {
			    MessageBox.Show(ex.Message);
		    }
	    }

        private void Checker_Sent(object sender, string text)
	    {
		    if(this.InvokeRequired)
		    {
			    this.BeginInvoke(new Action<object,string>(Checker_Sent), sender, text);
			    return;
		    }
		    Checker checker = (Checker)sender;
		    Label label = (Label)checker.Tag;
		    DateTime time = DateTime.Now;
		    string strTime = string.Format("[{0:yyyy-MM-dd HH:mm:ss}.{1}]", time, time.Millisecond);
            string note = strTime + "[" + checker.SerialPort.PortName + "][发送]" + text + "\r\n";
		    label.Text = note;
	    }

        private void sendCommandButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Checker checker in checkers)
                {
                    ((Label)checker.Tag).Text = string.Empty;
                    checker.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Checker checker in checkers)
            {
                checker.Close();
            }
        }

        private void sendCommandButton_Click()
        {

        }
    }
}
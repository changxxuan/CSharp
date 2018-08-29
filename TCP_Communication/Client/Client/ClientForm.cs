using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Diagnostics;

//客户端发送文本到服务器，服务器转发
//支持多个客户端，失败消息转发给客户端
namespace Client
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
        }

        TcpClient tcpClient;
        NetworkStream networkStream;
        Thread cThread;
        BinaryReader binaryReader;//System.IO
        BinaryWriter binaryWriter;

        private void ClientForm_Load(object sender, EventArgs e)
        {
            showMsgRichTextBox.ReadOnly = true;
            sendTextBox.Enabled = false;
            sendButton.Enabled = false;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                const int port = 60000;
                const string ip = "127.0.0.1";
                tcpClient = new TcpClient(ip, port);
                showMsgRichTextBox.Text = "连接服务器成功\r\n";
                //建立连接时绑定code
                networkStream = tcpClient.GetStream();
                byte[] code = Encoding.UTF8.GetBytes(codeTextBox.Text);
                networkStream.Write(code, 0, code.Length);

                connectButton.Enabled = false;
                sendButton.Enabled = true;
                sendTextBox.Enabled = true;
                sendTextBox.Focus();
                cThread = new Thread(new ThreadStart(Run));
                cThread.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show("连接服务器失败\r\n");
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = dcodeTextBox.Text + "/" + sendTextBox.Text;
                byte[] sendData = Encoding.UTF8.GetBytes(msg);
                networkStream.Write(sendData, 0 ,sendData.Length);
                showMsgRichTextBox.AppendText("send to " + dcodeTextBox.Text + ": " + sendTextBox.Text + "\r\n");
                sendTextBox.Text = "";
                sendTextBox.Focus();
            }
            catch
            {
                MessageBox.Show("客户端发送失败"); 
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            showMsgRichTextBox.Text = "";
        }

        private void Run()
        {
            while(true)
            {
                string msg = string.Empty;
                if(tcpClient.Connected)
                {
                    try
                    {
                        byte[] recvData = new byte[256];
                        int count = networkStream.Read(recvData, 0, recvData.Length);
                        msg = Encoding.UTF8.GetString(recvData, 0, count);
                        string[] codeAndData = msg.Split('/');
                        string code = codeAndData[0];
                        string msgBody = codeAndData[1];
                        UpdateMsg(code, msgBody);
                    }
                    catch
                    {
                        Debug.WriteLine("服务器退出");
                        break;
                    }
                }
            }
        }

        private void UpdateMsg(string code, string msg)
        {
            if(this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string, string>(UpdateMsg), code, msg);
                return;
            }
            showMsgRichTextBox.AppendText(code + ": " + msg + "\r\n");
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cThread.Abort();
            networkStream.Close();
            tcpClient.Close();
        }

    }
}
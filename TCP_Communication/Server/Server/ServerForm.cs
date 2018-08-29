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
using System.Net;
using System.Diagnostics;

namespace Server
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        TcpListener tcpListener;
        Thread listenerThread;

        List<OicqServicePoint> servicePoints = new List<OicqServicePoint>();

        private void ServerForm_Load(object sender, EventArgs e)
        {
            sendTextBox.Enabled = false;
            sendButton.Enabled = false;
            sendTextBox.Focus();
            showMsgRichTextBox.ReadOnly = true;
            startServerButton.Enabled = false;
            clearButton.Enabled = false;
            sendButton.Enabled = false;
            listenerThread = new Thread(new ThreadStart(ListenerStart));
            listenerThread.Start();
        }

        private void ListenerStart()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            const int port = 60000;
            tcpListener = new TcpListener(ip, port);
            tcpListener.Start();
            Debug.WriteLine("启动服务器成功，等待链接...");
            showMsgRichTextBox.Text = "启动服务器成功，等待链接...";
            while (true)
            {
                try
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    Debug.WriteLine("OicqService Start");
                    OicqServicePoint servicePoint = new OicqServicePoint();
                    servicePoint.TcpClient = tcpClient;

                    servicePoint.Transmited += servicePoint_Transmited;
                    servicePoint.Start();
                    servicePoints.Add(servicePoint);
                }
                catch (ThreadAbortException)
                {
                    tcpListener.Stop();
                    break;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    break;
                }
            }
        }

        private void servicePoint_Transmited(object sender, string dcode, string msg)
        {
            foreach(OicqServicePoint item in servicePoints)
            {
                if(item.code == dcode)
                {
                    try
                    {
                        OicqServicePoint oisp = (OicqServicePoint)sender;
                        item.SendMessage(oisp.code, msg);
                        return;
                    }
                    catch
                    {
                        //item.SendFailMessage("Send Fail !");
                    }
                }
            }
            OicqServicePoint failSp = (OicqServicePoint)sender;
            Debug.WriteLine(failSp.code + "...");
            failSp.SendFailMessage("send fail" + "/" + msg);
        }

        private void UpdateMsg(string dcode, string text)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string, string>(UpdateMsg), dcode, text);
                return;
            }
            showMsgRichTextBox.AppendText(dcode + ": " + text + "\r\n");
        }

        private void startServerButton_Click(object sender, EventArgs e)
        {

        }

        private void sendButton_Click(object sender, EventArgs e)
        {

        }

        private void clearButton_Click(object sender, EventArgs e)
        {

        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (OicqServicePoint item in servicePoints)
            {
                item.Stop();
            }
            listenerThread.Abort();
        }
    }
}
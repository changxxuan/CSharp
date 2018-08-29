using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace Server
{
    public class OicqServicePoint
    {
        public string code = string.Empty;
        private TcpClient tcpClient = null;
        private Thread thread = null;

        public TcpClient TcpClient
        {
            get
            {
                return tcpClient;
            }
            set
            {
                tcpClient = value;
            }
        }

        private NetworkStream networkStream;
        public event Action<object, string, string> Transmited;

        public void Start()
        {
            thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }
        public void Stop()
        {
            tcpClient.Close();
            networkStream.Close();
            thread.Abort();
        }

        private void Run()
        {
            networkStream = tcpClient.GetStream();
            byte[] recvCode = new byte[256];
            int count = networkStream.Read(recvCode, 0, recvCode.Length);
            string strCode = Encoding.UTF8.GetString(recvCode, 0, count);
            code = strCode;

            while(true)
            {
                try
                {
                    if(tcpClient.Connected)
                    {
                        TextMessage msg = ReceiveMessage();
                        Transmited(this, msg.Code, msg.MessageBody);
                    }
                }
                catch(ThreadAbortException)
                {
                    networkStream.Close();
                    break;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    break;
                }
            }
        }

        public void SendMessage(string code, string msg)
        {
            try
            {
                byte[] sendData = Encoding.UTF8.GetBytes(code + "/" + msg);
                networkStream.Write(sendData, 0, sendData.Length);
            }
            catch
            {
                Debug.WriteLine("客户端发送失败");
            }
        }

        public void SendFailMessage(string text)
        {
            byte[] sendFailData = Encoding.UTF8.GetBytes(text);
            networkStream.Write(sendFailData, 0, sendFailData.Length);
        }

        public TextMessage ReceiveMessage()
        {
            TextMessage textMsg = new TextMessage();
            string msg = string.Empty;
            byte[] recvData = new byte[256];
            int count = networkStream.Read(recvData, 0, recvData.Length);
            msg = Encoding.UTF8.GetString(recvData, 0, count);
            string[] codeAndData = msg.Split('/');
            textMsg.Code = codeAndData[0];
            textMsg.MessageBody = codeAndData[1];

            return textMsg;
        }
    }
}
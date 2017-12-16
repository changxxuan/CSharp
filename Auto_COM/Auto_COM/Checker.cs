using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;

namespace Auto_COM
{
    class Checker
    {
        public object Tag { get; set; }

        public SerialPort SerialPort
        {
            get
            {
                return serialPort;
            }
        }

        private SerialPort serialPort = new SerialPort();
        public event Action<object, string> Sent;
        public event Action<object, CancelEventArgs, string> Received;
        private static char[] command1 = new char[] { '*', 'I', 'D', 'N', '?', '\n' };
        private static char[] command2 = new char[] { 'm', 'o', 'd', 'e', 'l', '?', '\n' };
        private DelayCaller caller = new DelayCaller();
        private DelayTask task = null;

        public Checker()
        {
            serialPort.ErrorReceived += SerialPort_ErrorReceived;
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Debug.WriteLine(e.EventType.ToString());
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Debug.WriteLine(e.EventType.ToString());
            if (e.EventType == SerialData.Chars)
            {
                Thread.Sleep(100);
                SerialPort sp = (SerialPort)sender;
                try
                {
                    string text = sp.ReadExisting();
                    Debug.WriteLine("checker recv = " + text);
                    if (Received != null)
                    {
                        CancelEventArgs args = new CancelEventArgs();
                        args.Cancel = true;
                        Received(this, args, text);
                        if (task != null && args.Cancel)
                        {
                            task.IsRun = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return;
                }
            }
        }
        public void Open(string name, int baudRate)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            serialPort.BaudRate = baudRate;
            serialPort.PortName = name;
            serialPort.Open();
        }
        public void Close()
        {
            serialPort.Close();
            caller.Quit();
        }
        public void Start()
        {
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }
            if (caller.HasTask())
            {
                throw new Exception("延时任务未执行完成");
            }
            Debug.WriteLine("DsrHolding = " + serialPort.DsrHolding.ToString());
            Debug.WriteLine("ctsHolding = " + serialPort.CtsHolding.ToString());
            Debug.WriteLine("Handshake = " + serialPort.Handshake.ToString());
            Debug.WriteLine("CDHolding = " + serialPort.CDHolding.ToString());
            Debug.WriteLine("BreakState = " + serialPort.BreakState.ToString());
            if (serialPort.DsrHolding)
            {
                serialPort.DtrEnable = true;
            }
            serialPort.Write(command1, 0, command1.Count());
            if (Sent != null)
            {
                Sent(this, new string(command1));
            }
            task = caller.NewCall(500, SendModel);
        }
        private void SendNoDtsIdx()
        {
            serialPort.Write(command1, 0, command1.Count());
            if (Sent != null)
            {
                Sent(this, new string(command1));
            }
            task = caller.NewCall(500, SendModel);
        }
        private void SendModel()
        {
            serialPort.Write(command2, 0, command2.Count());
            if (Sent != null)
            {
                Sent(this, new string(command2));
            }
            task = caller.NewCall(500, Error);
        }
        private void Error()
        {
            if (Received != null)
            {
                CancelEventArgs args = new CancelEventArgs();
                Received(this, args, "接收数据超时");
            }
        }
    }
}

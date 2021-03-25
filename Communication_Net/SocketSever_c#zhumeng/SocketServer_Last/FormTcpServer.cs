using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketSever
{
    public partial class FormTcpServer : Form
    {
        //For scanner
        KeyenceScanner.KeyenceModuleLAN _scanner = new KeyenceScanner.KeyenceModuleLAN();
        private bool _scannerEnable;

        SocketManager _server;

        delegate void DelShowMsg(string str);
        DelShowMsg _showMsgDel;

        public FormTcpServer()
        {
            InitializeComponent();

            //Custom code
            _server = new SocketManager(10, 1024);
            _server.OnAccept += ServerOnAccept;
            _server.OnConnectionBreak += ServerOnConnectionBreak;
            _server.OnReceiveCompleted += ServerOnReceiveCompleted;
            _server.OnSendCompleted += ServerOnSendCompleted;

        }

        private void FrmTcpServer_Load(object sender, EventArgs e)
        {
            _showMsgDel = ShowMsg;
        }

        private void btnStartStopListen_Click(object sender, EventArgs e)
        {
            string str = btnStartStopListen.Text;
            if (str == "开始监听")
            {
                string serverIp = textBoxIP.Text.Trim();
                int serverPort = int.Parse(textBoxPort.Text.Trim());
                _server.Start(serverIp, serverPort);

                btnStartStopListen.Text = "停止监听";

                //开始监听后，初始化扫描仪
                _scannerEnable = bool.Parse(ConfigurationManager.AppSettings["Enable"].ToString());
                if (_scannerEnable)
                {
                    string scanIp = ConfigurationManager.AppSettings["ScannerIP"].ToString();
                    int scanPort = int.Parse(ConfigurationManager.AppSettings["ScannerPort"].ToString());
                    if (!_scanner.Init(scanIp, scanPort))
                    {
                        textBoxReceivedMsg.AppendText(string.Format("init scanner fail, IP:{0}; Port:{1}.\r\n", scanIp, scanPort));
                    }
                }
            }
            else
            {
                _server.Stop();
                btnStartStopListen.Text = "开始监听";
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string strEndPort = comboBoxClientList.Text;
            string sendStr = textBoxSendMsg.Text;
            IPAddress ipAddr = System.Net.IPAddress.Parse(strEndPort.Split(':')[0]);
            IPEndPoint endPort = new System.Net.IPEndPoint(ipAddr, int.Parse(strEndPort.Split(':')[1]));
            _server.Send(endPort, sendStr);
        }

        private void btnSendToAllClient_Click(object sender, EventArgs e)
        {
            string str = textBoxSendMsg.Text;
            _server.SendToAllClient(str);
        }

        private void FormTcpServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_scanner != null)
            {
                _scanner.Quit();
            }
            if (_server != null)
            {
                _server.OnAccept -= ServerOnAccept;
                _server.OnConnectionBreak -= ServerOnConnectionBreak;
                _server.OnReceiveCompleted -= ServerOnReceiveCompleted;
                _server.OnSendCompleted -= ServerOnSendCompleted;
                _server.Stop();
            }
        }

        #region "Events"

        private void ServerOnAccept(object sender, SocketAsyncEventArgs e)
        {
            string clientInfo = e.AcceptSocket.RemoteEndPoint.ToString();
            _showMsgDel(string.Format("有客户端接入！客户端信息：{0}", clientInfo));
            Invoke(new EventHandler(delegate { comboBoxClientList.Items.Add(clientInfo); }));
        }

        private void ServerOnSendCompleted(object sender, SocketAsyncEventArgs e)
        {
            string clientInfo = e.AcceptSocket.RemoteEndPoint.ToString();

            string @string = Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);
            //Console.WriteLine(Name + "发送：" + @string);
            _showMsgDel(string.Format("向客户端 {0} 发送：{1}", clientInfo, @string));
        }

        private void ServerOnReceiveCompleted(object sender, SocketAsyncEventArgs e)
        {
            string clientInfo = e.AcceptSocket.RemoteEndPoint.ToString();

            string strRecMsg = Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);
            if (e.Buffer[e.Offset] == 2 || e.Buffer[e.Offset + e.BytesTransferred - 1] == 3)
            {
                strRecMsg = strRecMsg.Substring(1, e.BytesTransferred - 2);
            }

            _showMsgDel(string.Format("从客户端 {0} 收到：{1}", clientInfo, strRecMsg));

            switch (strRecMsg)
            {
                case "INIT":

                    break;

                case "LON":
                    if (_scannerEnable)
                    {
                        string barcode = _scanner.Scan(3000);
                        _server.Send(e.AcceptSocket.RemoteEndPoint, barcode);
                    }
                    break;

                case "LOFF":



                    break;

                case "QUIT":



                    break;

                default:

                    break;
            }

        }

        private void ServerOnConnectionBreak(object sender, SocketAsyncEventArgs e)
        {
            string str = e.AcceptSocket.RemoteEndPoint.ToString();
            _showMsgDel(string.Format("有客户端断开！客户端信息：{0}", str));
            Invoke(new EventHandler(delegate { comboBoxClientList.Items.Remove(str); }));
        }

        #endregion

        #region "PrivateMethods"

        void ShowMsg(string str)
        {
            if (textBoxReceivedMsg.InvokeRequired)
            {
                textBoxReceivedMsg.Invoke(new Action<string>(ShowMsg), new object[] { str });
            }
            else
            {
                textBoxReceivedMsg.AppendText(str);
                textBoxReceivedMsg.AppendText("\r\n");
                textBoxReceivedMsg.ScrollToCaret();
            }
        }

        //The same function with method ShowMsg
        void ShowMsg2(string str)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(delegate { textBoxReceivedMsg.Text = str; }));
            }
            else
            {
                textBoxReceivedMsg.AppendText(str);
                textBoxReceivedMsg.AppendText("\r\n");
                textBoxReceivedMsg.ScrollToCaret();
            }
        }

        #endregion


    }
}

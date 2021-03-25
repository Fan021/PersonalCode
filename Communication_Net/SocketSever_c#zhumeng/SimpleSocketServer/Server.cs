using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace SocketSever
{
    public partial class Server : Form
    {

        private SocketServer _socketSever = null;

        public Server()
        {
            InitializeComponent();

            _socketSever = new SocketServer(textBoxIP.Text.Trim(), int.Parse(textBoxPort.Text.Trim()));
            _socketSever.MessageReceived += new SocketServer.MessageEvent(WatchMessage);

        }

        private void buttonStartListen_Click(object sender, EventArgs e)
        {
            try
            {
                _socketSever.StartListen();
                System.Threading.Thread.Sleep(20);

                string ip = ConfigurationManager.AppSettings["IP"].ToString();
                int port = int.Parse(ConfigurationManager.AppSettings["port"].ToString());
                _socketSever.InitScanner(ip, port);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            //_socketSever.StopListen();
        }

        private void WatchMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(delegate { labelMessage.Text = message; }));
            }

            Application.DoEvents();
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            _socketSever.Quit();
        }


    }
}

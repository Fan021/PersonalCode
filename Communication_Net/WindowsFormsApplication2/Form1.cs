using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Communication_Net;
namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private Serve_Net _serve;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            _serve = new Serve_Net();

            Thread receive = new Thread(new ThreadStart(ReceiveThread));
            receive.IsBackground = true;
            receive.Start();

        }

        private void Init_button1_Click(object sender, EventArgs e)
        {
            try
            {
                _serve.Init("10.190.21.126", 99); 
                Init_button1.BackColor = Color.YellowGreen;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void Send_button1_Click(object sender, EventArgs e)
        {
            try
            {
                _serve.Send("10.190.22.67", textBox1.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ReceiveThread()
        {
            var client = "";
            var msg = "";
            string time = "";
            string lastTime = "";
            string showMsg;
            while (true)
            {
                client = _serve._receiveMessage.client;
                msg = _serve._receiveMessage.message;
                time = _serve._receiveMessage.time;
                if (lastTime != time)
                {
                    showMsg = "[" + client + "] " + time + ":" + "\r\n" + msg + "\r\n"+"\r\n";
                    textBox2.Text += showMsg;
                    lastTime = time;
                }

            }
        }
    }
}

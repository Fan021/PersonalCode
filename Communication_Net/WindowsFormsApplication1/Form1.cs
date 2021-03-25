using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Communication_Net;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Client_Net _serve;
        private Thread GetData;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Close the functon of forbid using toolbox in different threads.
            Control.CheckForIllegalCrossThreadCalls = false;

            _serve = new Client_Net();
            GetData = new Thread(new ThreadStart(GetMessage));
        }
        private void Init_button1_Click(object sender, EventArgs e)
        {
            try
            {
                _serve.Init("10.190.21.126", 8080);
                Init_button1.BackColor = Color.YellowGreen;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Send_utton1_Click(object sender, EventArgs e)
        {
            try
            {
                _serve.Send(textBox1.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Start_button1_Click(object sender, EventArgs e)
        {
            try
            {
                _serve.ReceiveThreadStart();
                GetData.Start();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GetMessage()
        {
            while (true)
            {
                if (_serve.Result != "")
                {
                    textBox2.Text = _serve.Result;
                }
            }
        }

        private void Stop_button1_Click(object sender, EventArgs e)
        {
            try
            {
                _serve.ReceiveThreadStop();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

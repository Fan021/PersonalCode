using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vector;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Vector _vector;
        private bool _stop=true;
        private System.Threading.Thread _thread;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _vector = new Vector();
        }

        private void Init_button1_Click(object sender, EventArgs e)
        {
            _vector.Init(0);
        }

        private void Send_button1_Click(object sender, EventArgs e)
        {
            var b = textBox1.Text.Split(' ');

            byte[] a = new byte[64];
            for (int i = 0; i < 64; i++)
            {
                if (i < 8)
                {
                    var c = Convert.ToInt32(b[i], 16);
                    //var d = Convert.ToInt32(c,10);
                    a[i] = (byte)c;
                }
                else
                {
                    a[i] = (byte)i;
                }

            }
            _vector.WriteMsg(a);
        }

        private void Receive_button1_Click(object sender, EventArgs e)
        {
            var a = _vector.ReadMsg();
            var b = "";
            foreach (var item in a)
            {
                b += (item.ToString()+" ");
            }
            textBox2.Text = b;
        }

        private void Send_Cycle_button1_Click(object sender, EventArgs e)
        {
            _stop = true;
            _thread = new System.Threading.Thread(new System.Threading.ThreadStart(SendCycle));
            _thread.Start();
        }

        private void SendCycle()
        {
            while (_stop)
            {
                var b = textBox1.Text.Split(' ');

                byte[] a = new byte[64];
                for (int i = 0; i < 64; i++)
                {
                    if (i < 8)
                    {
                        var c = Convert.ToInt32(b[i], 16);
                        //var d = Convert.ToInt32(c,10);
                        a[i] = (byte)c;
                    }
                    else
                    {
                        a[i] = (byte)i;
                    }

                }
                _vector.WriteMsg(a);
                System.Threading.Thread.Sleep(30);
            }
        }

        private void Stop_button1_Click(object sender, EventArgs e)
        {
            _stop = false;
            _thread.Abort();
        }

        private void Close_button1_Click(object sender, EventArgs e)
        {
            _vector.Close();
        }
    }
}

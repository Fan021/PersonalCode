using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlHander;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Xml _xml;
        public Form1()
        {
            InitializeComponent();
        }

        private void Create_button1_Click(object sender, EventArgs e)
        {
            _xml.CreateXml();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _xml = new Xml();
        }

        private void Add_button1_Click(object sender, EventArgs e)
        {
            _xml.AddXml();
        }

        private void read_button2_Click(object sender, EventArgs e)
        {
            var result = _xml.ReadXml("Language");
        }

        private void Alert_button1_Click(object sender, EventArgs e)
        {
            _xml.AlertXml("Language");
        }
    }
}

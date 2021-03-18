using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvInfo;
using ZebraPrinterNet;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private ZebraPrintControl _zebra = new ZebraPrintControl();
        private string _rootPath;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _rootPath = Environment.CurrentDirectory + "\\Printer";
     
        }
        

        private void Print_button_Click(object sender, EventArgs e)
        {
            _zebra.Init("169.254.157.113",9100);
            _zebra.LoadFormatFile(_rootPath+ @"\10516931.txt");
            var csv = new Csv(_rootPath + "\\Daimler_VW_DCU_List.csv");
            var a14 = csv.GetArtilces();
            var a1 = csv.GetCustomerNr("10529868");
            var a6 = csv.GetProductDate();
            var a2 = csv.GetPrintInfo_Designation2("10529868");
            var a3 = csv.GetP17_50_LabelData_Description("10529868");
            var a4 = csv.GetPrintInfo_Qlevel("10529868");
            var a5 = csv.GetPrintInfo_Qlevel2("10529868");
            var a7 = csv.GetPrintInfo_SWN("10529868");
            var a8 = csv.GetP17_40_LABELDATA_SW_STATUS("10529868");
            var a9 = csv.GetPrintInfo_DlagID("10529868");
            var a10 = csv.GetPrintInfo_HWN("10529868");
            var a11 = csv.GetP17_35_LABELDATA_HW_STATUS("10529868");
            var a12 = a1.Replace(" ", "") + "/" + a6;
            var a13 = "/P10516932-01/SN0000000000000/" + a4 + a5;
            var a15 = csv.GetLK_ArticleIndex("10529868");
            string[] content = { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13 };
            _zebra.LoadPrintFile(_rootPath+ @"\DR10516931.txt",content);
            _zebra.PrintFile("DR10516931.txt");
        }
    }
}

using CefSharp;
using CefSharp.WinForms;
using CefSharp.Internals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chorme
{
    public partial class Form1 : Form
    {
        private CefSharp.WinForms.ChromiumWebBrowser _br;
        public Form1()
        {
            InitializeComponent();
            var deliver = new DemoClass();

            //the site of web
            var url = @"C:\Users\fan021\Desktop\temp\Chorme\Chorme\demo.html";
            _br = new CefSharp.WinForms.ChromiumWebBrowser(url);
            _br.Dock = DockStyle.Fill;

            //allow to register a web js instance
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            //register a instance in web, deliver the C#'s class to the instance
            _br.RegisterJsObject("form_syn", deliver);
            //_br.RegisterAsyncJsObject("form_async", deliver);
            this.Controls.Add(_br);
            //_br.IsBrowserInitializedChanged += this.PasstoJS;
         
            deliver.SendMessage = "Hello,I'm in C#";
           
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //close web
            CefSharp.Cef.Shutdown();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var message = "Hello,I am in C#,I am using js method";
            //C# use the method in JS,use the format like below, the message to be passed cannot include the symbol of '
            _br.ExecuteScriptAsync(string.Format("ShowMsg({0},'{1}')",0,message));
        }
    }

    //create a class used to transmit and receive message with js, used by js
    public class DemoClass
    {
        //define a propority to transmit data to js
        public string SendMessage { get; set; }
        //define a propority to receive data from js
        public string GetMessage { get; set; }
        //define a method to receive data from js 
        public string GetMessage_method(string msg)
        {
            MessageBox.Show(msg);
            return msg;
        }
        //define amethod to send data to js
        public string ReturnMessage_method()
        {
            return SendMessage;
        }
    }
}

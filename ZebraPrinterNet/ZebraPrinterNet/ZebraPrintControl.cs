using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Printer;

namespace ZebraPrinterNet
{
    public class ZebraPrintControl
    {
        private Printer.Printer pt;
        public void Init(string ip, int port)
        {
            try
            {
                pt = new Printer.Printer();
                pt.InitNetPrinter(port, ip);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadFormatFile(string path)
        {
            try
            {
                pt.LoadFormatFile(path);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void LoadPrintFile(string filename, string[] contentlist)
        {
            try
            {
                pt.LoadPrintFile(filename);
                for (int i = 1; i <= contentlist.Length; i++)
                {
                    var preField = "^FN" + i + "^FD";
                    var postField = "^FS";
                    var sContent = contentlist[i - 1];
                    pt.SetField("DR10516931.txt", preField, postField, sContent);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void PrintFile(string filename)
        {
            try
            {
                pt.PrintLabel(filename);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

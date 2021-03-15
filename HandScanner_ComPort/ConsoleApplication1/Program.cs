using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandScanner_ComPort;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var _scanner = new HandScanner(5, 115200);
            while (true)
            {
                if (_scanner.IsBarcodeReady())
                {
                    Console.WriteLine(_scanner.GetBarcode());
                    Console.ReadKey();
                }
            }

        }
    }
}

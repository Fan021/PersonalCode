using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            LogText.Log log = new LogText.Log();

            log.WriteInfo("This is the first line of log!");
            log.WriteError("This is the first error log...");
            log.WriteInfo("This is the second line of log!");
            log.WriteInfo("This is the thred line of log!");
            log.WriteInfo("This is the forth line of log!");
            log.WriteError("This is the second error log...");
            log.WriteInfo("This is the fifth line of log!");


            Console.WriteLine("Finished!");
        }
    }
}

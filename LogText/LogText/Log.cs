using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogText
{
    public class Log
    {
        private bool InitLog(string fullName)
        {
            try
            {
                if (!Directory.Exists(@".\Log"))
                {
                    Directory.CreateDirectory(@".\Log");
                }

                if (!File.Exists(fullName))
                {
                    File.Create(fullName).Close();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
         
        }

        public bool WriteInfo(string message)
        {
            try
            {
                var logFileName = GetDate() + ".txt";
                var logFulName = @".\Log\" + logFileName;
                if (!File.Exists(logFulName))
                {
                    InitLog(logFulName);
                }

                var logMessage = DateTime.Now.ToString() + " " + "Info:"+message;

                using (StreamWriter sw=File.AppendText(logFulName))
                {
                    sw.WriteLine(logMessage);
                    sw.Flush();
                    sw.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool WriteError(string message)
        {
            try
            {
                var logFileName = GetDate() + ".txt";
                var logFulName = @".\Log\" + logFileName;
                if (!File.Exists(logFulName))
                {
                    InitLog(logFulName);
                }

                var logMessage = DateTime.Now.ToString() + " " + "Error:"+message;

                using (StreamWriter sw = File.AppendText(logFulName))
                {
                    sw.WriteLine(logMessage);
                    sw.Flush();
                    sw.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        private string GetDate()
        {
            var nowYear = DateTime.Now.Year;
            var nowMonth = DateTime.Now.Month;
            var nowDay = DateTime.Now.Day;
            var nowDate = nowYear.ToString() + "-" + nowMonth.ToString() + "-" + nowDay.ToString() + "-log";
            return nowDate;
        }
    }
}

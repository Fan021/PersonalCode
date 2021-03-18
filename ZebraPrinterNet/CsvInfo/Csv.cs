using System;
using System.Collections.Generic;
using System.IO;

namespace CsvInfo
{
    public class Csv
    {
        private Dictionary<string, string[]> perLine = new Dictionary<string, string[]>();
        private List<string> articleList = new List<string>();
        public Csv(string path)
        {
            StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
            var csvLine = "";
            string[] perLineContent;
            while ((csvLine = sr.ReadLine()) != null)
            {
                perLineContent = csvLine.Split(';');
                perLine.Add(perLineContent[0], perLineContent);
                articleList.Add(perLineContent[0]);
            }
        }

        public List<string> GetArtilces()
        {
            List<string> arrList = new List<string>();
            for (int i = 1; i < articleList.Count; i++)
            {
                arrList.Add(articleList[i]);
            }
            return arrList;
        }
        public string GetCustomerNr(string article)
        {
            var targetLine = perLine[article];
            return targetLine[8];
        }

        public string GetPrintInfo_Designation2(string article)
        {
            var targetLine = perLine[article];
            return targetLine[10];
        }

        public string GetP17_50_LabelData_Description(string article)
        {
            var targetLine = perLine[article];
            return targetLine[36];
        }

        public string GetPrintInfo_Qlevel(string article)
        {
            var targetLine = perLine[article];
            return targetLine[11];
        }

        public string GetPrintInfo_Qlevel2(string article)
        {
            var targetLine = perLine[article];
            string targetNum = "";
            var targetInt = Convert.ToInt32(targetLine[12]);
            if (targetInt < 10)
            {
                targetNum = "0" + "0" + targetLine[12];
            }
            else if (targetInt < 100)
            {
                targetNum = "0" + targetLine[12];
            }
            else
            {
                targetNum = targetLine[12].ToString();
            }
            return targetNum;
        }

        public string GetProductDate()
        {
            var year = DateTime.Now.Year.ToString().Substring(2);
            var week = (DateTime.Now.DayOfYear - 4) / 7 + 1;
            string strWeek = "";
            if (week < 10)
            {
                strWeek = "0" + week;
            }
            else
            {
                strWeek = week.ToString();
            }
            var day = (int)DateTime.Now.DayOfWeek;
            var date = year.ToString() + strWeek + day;
            return date;
        }

        public string GetPrintInfo_SWN(string article)
        {
            string targetNum = "";
            var targetLine = perLine[article];
            targetNum = "SW:" + targetLine[13];
            return targetNum;
        }
        public string GetP17_40_LABELDATA_SW_STATUS(string article)
        {
            var targetLine = perLine[article];
            var targetNum = targetLine[34].Replace("_", "/");
            targetNum = "SW-Status:" + targetNum;
            return targetNum;
        }

        public string GetPrintInfo_DlagID(string article)
        {
            var targetLine = perLine[article];
            string targetNum = "";
            var targetInt = Convert.ToInt32(targetLine[15]);
            if (targetInt < 10)
            {
                targetNum = "0" + "0" + "0" + targetLine[15];
            }
            else if (targetInt < 100)
            {
                targetNum = "0" + "0" + targetLine[15];
            }
            else if (targetInt < 1000)
            {
                targetNum = "0" + targetLine[15];
            }
            else
            {
                targetNum = targetLine[15].ToString();
            }
            targetNum = "Diag.-ID:" + targetNum;
            return targetNum;
        }

        public string GetPrintInfo_HWN(string article)
        {
            string targetNum = "";
            var targetLine = perLine[article];
            targetNum = "HW:" + targetLine[16];
            return targetNum;
        }

        public string GetP17_35_LABELDATA_HW_STATUS(string article)
        {
            var targetLine = perLine[article];
            var targetNum = targetLine[33].Replace("_", "/");
            targetNum = "HW-Status:" + targetNum;
            return targetNum;
        }

        public string GetLK_ArticleIndex(string article)
        {
            var targetLine = perLine[article];
            var targetNum = "";
            var targetInt = Convert.ToInt32(targetLine[6]);
            if (targetInt < 10)
            {
                targetNum = "0" + targetLine[6];
            }
            else
            {
                targetNum = targetLine[6];
            }
            return targetNum;
        }
    }
}

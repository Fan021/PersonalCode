using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//1、引用命名空间
using System.Xml;
using System.IO;

namespace XmlHander
{
    public class Xml
    {
        private XmlDocument doc;
        public Xml()
        {
            //2、创建XML文档对象
            doc = new XmlDocument();
        }
        //通过代码来创建XML文档 
        public void CreateXml()
        {
            //3、创建第一个行描述信息，并且添加到doc文档中
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            //4、创建根节点
            XmlElement books = doc.CreateElement("Books");
            //将根节点添加到文档中
            doc.AppendChild(books);

            //5、给根节点Books创建子节点
            XmlElement book1 = doc.CreateElement("Book");
            //将book添加到根节点
            books.AppendChild(book1);
            //6、给Book1添加子节点
            XmlElement name1 = doc.CreateElement("Name");
            name1.InnerText = "三国演义";
            //7.给节点添加属性
            name1.SetAttribute("Language", "Chinese");
            name1.SetAttribute("Author", "Mr Luo");
            book1.AppendChild(name1);

            XmlElement price1 = doc.CreateElement("Price");
            price1.InnerText = "70";
            book1.AppendChild(price1);

            XmlElement des1 = doc.CreateElement("Des");
            des1.InnerText = "好看";
            book1.AppendChild(des1);

            XmlElement book2 = doc.CreateElement("Book");
            books.AppendChild(book2);


            XmlElement name2 = doc.CreateElement("Name");
            name2.InnerText = "西游记";
            book2.AppendChild(name2);

            XmlElement price2 = doc.CreateElement("Price");
            price2.InnerText = "80";
            book2.AppendChild(price2);

            XmlElement des2 = doc.CreateElement("Des");
            des2.InnerText = "还不错";
            book2.AppendChild(des2);

            doc.Save("Books.xml");
        }

        public void AddXml()
        {
            //追加XML文档
            XmlElement books;
            if (File.Exists("Books.xml"))
            {
                //如果文件存在 加载XML
                doc.Load("Books.xml");
                //获得文件的根节点
                books = doc.DocumentElement;
            }
            else
            {
                //如果文件不存在
                //创建第一行
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                //创建跟节点
                books = doc.CreateElement("Books");
                doc.AppendChild(books);
            }
            //8、给根节点Books创建子节点
            XmlElement book1 = doc.CreateElement("Book");
            //将book添加到根节点
            books.AppendChild(book1);


            //9、给Book1添加子节点
            XmlElement name1 = doc.CreateElement("Name");
            name1.InnerText = "c#开发大全";
            book1.AppendChild(name1);

            XmlElement price1 = doc.CreateElement("Price");
            price1.InnerText = "110";
            book1.AppendChild(price1);

            XmlElement des1 = doc.CreateElement("Des");
            des1.InnerText = "看不懂";
            book1.AppendChild(des1);

            doc.Save("Books.xml");

        }

        public List<string> ReadXml(string attribute)
        {
            List<string> arr = new List<string>();
            //加载要读取的XML
            if (File.Exists("Books.xml"))
            {
                doc.Load("Books.xml");
            }


            //获得根节点
            XmlElement books = doc.DocumentElement;

            //获得某一类特定的子节点
            XmlNodeList xnl = books.SelectNodes("/Books/Book/Name");

            if (attribute == "")
            {
                foreach (XmlNode item in xnl)
                {
                    arr.Add(item.InnerText);
                }
            }
            else
            {
                //读取带属性的XML文档
                foreach (XmlNode item in xnl)
                {
                    if (item.Attributes.Count != 0)
                    {
                        arr.Add(item.Attributes["Language"].Value);
                        arr.Add(item.Attributes["Author"].Value);
                    }
                }
            }

            return arr;
        }

        public void AlertXml(string attribute)
        {
            //改变属性的值
            if (File.Exists("Books.xml"))
            {
                doc.Load("Books.xml");
            }
            //获得根节点
            XmlElement books = doc.DocumentElement;

            //获得某个特定的带有属性的子节点
            XmlNodeList xnl = books.SelectNodes("/Books/Book/Name[@Language='Chinese']");
            //读取带属性的XML文档
            foreach (XmlNode item in xnl)
            {
                if (item.InnerText.Contains("三国演义"))
                {
                    item.Attributes[attribute].Value = "English";
                }
            }
            doc.Save("Books.xml");
        }

        public void DeleteXml()
        {
            doc.Load("Order.xml");
            XmlNode xn = doc.SelectSingleNode("/Order/Items");
            xn.RemoveAll();
            doc.Save("Order.xml");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XML测试
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //CreatXmlFile("Books.xml");
            CreatXmlFileEx("Books.xml");
            ReadXml("Books.xml");
            Console.ReadLine();

        }
        /// <summary>
        /// 创建xml文件
        /// </summary>
        /// <param name="file_name"></param>
        static void CreatXmlFile(string file_name)
        {
            //创建xml对象
            XmlDocument doc = new XmlDocument();
            //创建第一行描述信息,并添加到xml对象中
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);

            //创建根节点,并添加到xml对象中
            XmlElement books = doc.CreateElement("Books");
            doc.AppendChild(books);

            //给根节点创建第一个子节点
            XmlElement book1 = doc.CreateElement("Book");
            //将子节点添加到根节点
            books.AppendChild(book1);

            //给子节点1创建1个子节点
            XmlElement name1 = doc.CreateElement("Name");
            name1.InnerText = "水浒传";
            book1.AppendChild (name1);


            doc.Save(file_name);

        }
        /// <summary>
        /// 创建带有属性的XML文件
        /// </summary>
        /// <param name="file_name"></param>
        static void CreatXmlFileEx(string file_name)
        {
            //创建xml对象
            XmlDocument doc = new XmlDocument();
            //创建第一行描述信息,并添加到xml对象中
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);

            //创建根节点,并添加到xml对象中
            XmlElement books = doc.CreateElement("Books");
            doc.AppendChild(books);

            //给根节点创建第一个子节点
            XmlElement book1 = doc.CreateElement("Book");
            //将子节点添加到根节点
            books.AppendChild(book1);

            //给子节点1创建1个子节点
            XmlElement name1 = doc.CreateElement("Name");
            name1.InnerText = "水浒传";
            book1.AppendChild(name1);

            //创建子节点
            XmlElement orderItme = doc.CreateElement("order");
            //给子节点赋属性
            orderItme.SetAttribute("Name", "辣条");
            orderItme.SetAttribute("价格", "三元");
            books.AppendChild (orderItme);


            doc.Save(file_name);
        }

        /// <summary>
        ///给指定xml文件追加节点
        /// </summary>
        /// <param name="file_name"></param>
        static void AppendXmlNode(string file_name)
        {
            XmlDocument doc = new XmlDocument();//xml文件对象
            XmlElement books;//根节点
            if(File.Exists(file_name))//如果存在
            {
                doc.Load(file_name);//加载xml文件对象
                books = doc.DocumentElement;//获取根节点
            }
            else
            {
                //创建文件第一行，和根节点
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                books = doc.CreateElement("Books");
                doc.AppendChild(books);
            }
            //。。。。。。。。。。。。
            doc.Save(file_name);
        }
        /// <summary>
        /// 读取xml文件
        /// </summary>
        /// <param name="file_name"></param>
        static void ReadXml(string file_name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file_name);
            XmlElement books = doc.DocumentElement;

            XmlNodeList xnl = books.ChildNodes;  //得到根目录下所有节点
            foreach (XmlNode xn in xnl)
            {
                Console.WriteLine(xn.InnerText);
                if(xn.Attributes != null)
                {
                    Console.WriteLine(xn.Name);
                    foreach (XmlAttribute xmlAttribute in  xn.Attributes)
                    {
                        Console.WriteLine($"{xmlAttribute.Name}={xmlAttribute.InnerXml}");
                    }
                }
            }
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="file_name"></param>
        static void DeletNode(string file_name)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("file_name");
            //删除单个节点
            XmlNodeList xnl = doc.SelectSingleNode("Book").ChildNodes;
            xnl[1].ParentNode.RemoveChild(xnl[1]);
            //删除所有节点
            //XmlNode xn = doc.SelectSingleNode("Books");
            //xn.RemoveAll();
            doc.Save("file_name");
            Console.WriteLine("删除成功");
        }
    }
}

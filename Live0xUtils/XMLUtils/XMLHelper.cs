using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Live0xUtils.XMLUtils
{
    public class XMLHelper
    {
        public static List<T> XMLToList<T>(string xml, string desc)
        {
            List<T> lists = new List<T>();
            XDocument xDocument = XDocument.Parse(xml);
            var s = from info in xDocument.Descendants(desc).Elements("Row")
                    select info;

            foreach (XElement nodes in s)
            {
                T t = Activator.CreateInstance<T>();
                PropertyInfo[] pInfos = t.GetType().GetProperties();
                var info = from x in nodes.Elements()
                           select x;
                foreach (XElement node in info)
                {

                    string s11 = string.Join("@", info.Select(p => p.Name));
                    foreach (PropertyInfo p in pInfos)
                    {
                       
                        if (p.Name.ToUpper().Equals(node.Name.ToString().ToUpper()) && p.CanWrite) 
                        {
                            p.SetValue(t, node.Value, null);
                            break;
                        }
                    }
                }
                lists.Add(t);
            }
            return lists;
        }


        public string CreateXML<T>(T t,string RootName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            PropertyInfo[] propertyInfos = t.GetType().GetProperties();

            XmlNode decNode = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(decNode);

            XmlElement root = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(root);

            foreach (PropertyInfo p in propertyInfos)
            {
                object[] obj = p.GetCustomAttributes(true);
                if (obj.Length > 0)
                {
                    //EDescription eDescription = (EDescription)obj[0];
                    XmlNode xmlNode = xmlDoc.SelectSingleNode("//" + RootName);
                    if (xmlNode == null)
                    {
                        xmlNode = xmlDoc.CreateElement(RootName);
                        root.AppendChild(xmlNode);
                    }
                    XmlElement xmlElement = GetXmlElement(xmlDoc, p.Name, p.GetValue(t, null) == null ? "" : Convert.ToString(p.GetValue(t, null)));
                    xmlNode.AppendChild(xmlElement);
                }
            }
            return xmlDoc.OuterXml;
        }

        public XmlElement GetXmlElement(XmlDocument doc, string elementName, string value)
        {
            XmlElement element = doc.CreateElement(elementName);
            element.InnerText = value;
            return element;
        }

        public string GetXmlValue(string strXml, string strEle)
        {
            string strResult = "";
            Match match = Regex.Match(strXml, @"<" + strEle + @">(?<Value>[\s\S]*?)</" + strEle + ">");
            if (match.Success)
            {

                strResult = match.Groups["Value"].Value;
            }
            return strResult;
        }

        public T FillEntityByXml<T>(string strXml)
        {
            T t = Activator.CreateInstance<T>();
            try
            {
                PropertyInfo[] propertyInfos = t.GetType().GetProperties();
                foreach (PropertyInfo p in propertyInfos)
                {
                    p.SetValue(t, GetXmlValue(strXml, p.Name), null);
                }
            }
            catch
            {
                throw;
            }
            return t;
        }
    }
}

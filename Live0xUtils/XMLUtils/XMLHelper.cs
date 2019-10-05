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


        public static string CreateXML<T>(T t,string RootName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            PropertyInfo[] propertyInfos = t.GetType().GetProperties();

            XmlNode decNode = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(decNode);

            XmlElement root = xmlDoc.CreateElement(RootName);
            xmlDoc.AppendChild(root);

            foreach (PropertyInfo p in propertyInfos)
            {
               // object[] obj = p.GetCustomAttributes(true);
              //  if (obj.Length > 0)
                //{
                    //EDescription eDescription = (EDescription)obj[0];
                    XmlNode xmlNode = xmlDoc.SelectSingleNode("//" + RootName);
                    if (xmlNode == null)
                    {
                        xmlNode = xmlDoc.CreateElement(RootName);
                        root.AppendChild(xmlNode);
                    }
                    XmlElement xmlElement = GetXmlElement(xmlDoc, p.Name, p.GetValue(t, null) == null ? "" : Convert.ToString(p.GetValue(t, null)));
                    xmlNode.AppendChild(xmlElement);
               // }
            }
            return xmlDoc.OuterXml;
        }

        public static string CreateXMLEx<T>(T t, string RootName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            PropertyInfo[] propertyInfos = t.GetType().GetProperties();

            XmlNode decNode = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(decNode);

            XmlElement root = xmlDoc.CreateElement(RootName);
            xmlDoc.AppendChild(root);

            foreach (PropertyInfo p in propertyInfos)
            {
                if (p.PropertyType.Name.StartsWith("List"))
                {
                    string j = p.Name;
                }
                else
                {
                    string s = p.Name;
                }
                //XmlElement xmlElement = GetXmlElement(xmlDoc, p.Name, p.GetValue(t, null) == null ? "" : Convert.ToString(p.GetValue(t, null)));

                //XEleAttribute xEleAttribute = p.GetCustomAttributes(typeof(XEleAttribute), false).FirstOrDefault() as XEleAttribute;
                //if (xEleAttribute != null && string.IsNullOrEmpty(xEleAttribute.Ele))
                //{
                //    XmlNode xmlNode = null; = xmlDoc.SelectSingleNode("//" + RootName);
                //    if (xmlNode == null)
                //    {
                //        xmlNode = xmlDoc.CreateElement(RootName);
                //        root.AppendChild(xmlNode);
                //    }
                //    xmlNode.AppendChild(xmlElement);
                //}
                //else
                //{
                //    root.AppendChild(xmlElement);
                //}
            }
            return xmlDoc.OuterXml;
        }

        //public static string CreateXMLEx3(object obj, string RootName)
        //{
        //    XmlDocument xmlDoc = new XmlDocument();
        //    XmlNode decNode = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
        //    xmlDoc.AppendChild(decNode);
        //    CreateXMLEx4( obj, RootName, xmlDoc);
        //    return xmlDoc.OuterXml;
        //}

        //public static void CreateXMLEx4(object obj, string RootName, XmlDocument xmlDocument,XmlElement root)
        //{
        //    if (obj == null) return;
        //    Type tinfo = obj.GetType();
        //    PropertyInfo[] pInfos = tinfo.GetProperties();
        //    if (tinfo.IsGenericType)
        //    {
        //        System.Collections.ICollection list = obj as System.Collections.ICollection;
        //        if (list != null)
        //        {
        //            foreach (var item in list)
        //            {
        //                XEleAttribute xEleAttribute = tinfo.GetCustomAttributes(typeof(XEleAttribute), false).FirstOrDefault() as XEleAttribute;
        //                CreateXMLEx4(item, xEleAttribute.Ele, xmlDocument, root);
        //            }
        //        }
        //        return;
        //    }
        //}

        public static string CreateXMLEx0(object obj,string RootName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();

            XmlNode decNode = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(decNode);

            XmlElement root = xmlDoc.CreateElement(RootName);
            xmlDoc.AppendChild(root);
            CreateXMLEx1(obj,xmlDoc,root);
            return xmlDoc.OuterXml;
        }

        private static void CreateXMLEx1(object obj, XmlDocument xmlDoc, XmlElement root)
        {
            XmlElement rootElement = null;
            if (obj == null) return;
            Type tinfo = obj.GetType();
            PropertyInfo[] pInfos = tinfo.GetProperties();
            if (tinfo.IsGenericType)
            {
                System.Collections.ICollection list = obj as System.Collections.ICollection;
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        CreateXMLEx1(item, xmlDoc, root);
                    }
                }
                return;
            }
            XEleAttribute rootEleAttribute = tinfo.GetCustomAttributes(typeof(XEleAttribute), false).FirstOrDefault() as XEleAttribute;
            if (rootEleAttribute != null && rootEleAttribute.Ele != null)
            {
                rootElement = xmlDoc.CreateElement(rootEleAttribute.Ele);
            }

            foreach (PropertyInfo info in pInfos)
            {
                XEleAttribute xEleAttribute = info.GetCustomAttributes(typeof(XEleAttribute), false).FirstOrDefault() as XEleAttribute;
                if (info.PropertyType.Name.StartsWith("List"))
                {
                    object o = info.GetValue(obj, null);
                    if (xEleAttribute != null && !string.IsNullOrEmpty(xEleAttribute.Ele))
                    {
                        XmlNode xmlNode =rootElement != null ?  rootElement.SelectSingleNode("//" + xEleAttribute.Ele) : root.SelectSingleNode("//" + xEleAttribute.Ele);
                        if (xmlNode == null)
                        {
                            XmlElement xmlElement = xmlDoc.CreateElement(xEleAttribute.Ele);
                            CreateXMLEx1(o, xmlDoc, xmlElement);
                            if(rootElement != null )
                                rootElement.AppendChild(xmlElement) ;
                            else
                                root.AppendChild(xmlElement);
                        }
                    }
                    else
                    {
                        CreateXMLEx1(o, xmlDoc , rootElement != null ? rootElement : root);
                    }
                }
                else
                {

                    XmlElement xmlElement = GetXmlElement(xmlDoc, info.Name, info.GetValue(obj, null) == null ? "" : Convert.ToString(info.GetValue(obj, null)));
                    if (xEleAttribute != null && !string.IsNullOrEmpty(xEleAttribute.Ele))
                    {
                         XmlNode xmlNode = rootElement != null ? rootElement.SelectSingleNode("//" + xEleAttribute.Ele) : root.SelectSingleNode("//" + xEleAttribute.Ele);
                        if (xmlNode == null)
                        {
                            xmlNode = xmlDoc.CreateElement(xEleAttribute.Ele);
                            if (rootElement != null)
                                rootElement.AppendChild(xmlElement);
                            else
                                root.AppendChild(xmlElement);
                        }
                        xmlNode.AppendChild(xmlElement);
                    }
                    else
                    {
                        if (rootElement != null)
                            rootElement.AppendChild(xmlElement);
                        else
                            root.AppendChild(xmlElement);
                    }
                }
            }
            if (rootElement != null)
                root.AppendChild(rootElement);
        }

        public static XmlElement GetXmlElement(XmlDocument doc, string elementName, string value)
        {
            XmlElement element = doc.CreateElement(elementName);
            element.InnerText = value;
            return element;
        }

        public static string GetXmlValue(string strXml, string strEle)
        {
            string strResult = "";
            Match match = Regex.Match(strXml, @"<" + strEle + @">(?<Value>[\s\S]*?)</" + strEle + ">");
            if (match.Success)
            {

                strResult = match.Groups["Value"].Value;
            }
            return strResult;
        }

        public static T FillEntityByXml<T>(string strXml)
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

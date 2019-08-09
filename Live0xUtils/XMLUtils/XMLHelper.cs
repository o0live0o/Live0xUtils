using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
    }
}

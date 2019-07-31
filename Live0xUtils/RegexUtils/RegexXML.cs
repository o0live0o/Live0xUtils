using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Live0xUtils.RegexUtils
{
    public class RegexXML
    {
        public static string MatchProterty(string xml, string name)
        {
            string s = "";
            try
            {
                string reg = "" + name + "=\"(?<name>.*?)\"";
                Regex regex = new Regex(reg);
                Match match = regex.Match(xml);
                if (match.Success)
                {
                    s = match.Groups["name"].Value;
                }
            }
            catch (Exception ex)
            {
            }
            return s;
        }

        public static T XMLToModel<T>(string xml, string desc, string elem)
        {
            T t = Activator.CreateInstance<T>();
            try
            {
                XDocument xDocument = XDocument.Parse(xml);
                var s = from info in xDocument.Descendants(desc).Elements(elem).Nodes()
                        select info;
                PropertyInfo[] pInfos = t.GetType().GetProperties();
                foreach (XElement node in s)
                {
                    foreach (PropertyInfo p in pInfos)
                    {
                        try
                        {
                            if (p.Name.ToUpper().Equals(node.Name.ToString().ToUpper()))
                            {
                                //p.SetValue(t, node.Value, null);
                                //p.SetValue(t, Convert.ChangeType(node.Value,p.PropertyType), null);
                                if (!p.PropertyType.IsGenericType)
                                {
                                    p.SetValue(t, Convert.ChangeType(node.Value, p.PropertyType), null);
                                }
                                else
                                {
                                    Type genericTypeDefinition = p.PropertyType.GetGenericTypeDefinition();
                                    if (genericTypeDefinition == typeof(Nullable<>))
                                    {
                                        //p.SetValue(t, Convert.ChangeType(t, Nullable.GetUnderlyingType(p.PropertyType)), null);
                                        p.SetValue(t, Convert.ChangeType(t, p.PropertyType.GetGenericArguments()[0]), null);
                                    }
                                }
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }
            }
            catch (Exception ex) { throw; }
            return t;
        }

        public static List<T> XMLToList<T>(string xml, string desc,string ele)
        {
            List<T> lists = new List<T>();
            XDocument xDocument = XDocument.Parse(xml);
            var s = from info in xDocument.Descendants(desc).Elements(ele)
                    select info;

            foreach (XElement nodes in s)
            {
                T t = Activator.CreateInstance<T>();
                PropertyInfo[] pInfos = t.GetType().GetProperties();
                var info = from x in nodes.Elements()
                           select x;
                foreach (XElement node in info)
                {
                    foreach (PropertyInfo p in pInfos)
                    {
                        if (p.Name.ToUpper().Equals(node.Name.ToString().ToUpper()))
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

        public static string MatchField(string src, string field,bool defaultNull)
        {
            Match match = Regex.Match(src, "<" + field + @">(?<Value>[\s\S]*?)</" + field + ">");
            if (match.Success)
            {
                return match.Groups["Value"].Value;
            }
            return defaultNull ? null : "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Live0xUtils.RegexUtils
{
    public class MatchR
    {
        public static string MatchField(string src,string field)
        {
            Match match = Regex.Match(src, "<" + field + @">(?<Value>[\s\S]*?)</" + field + ">");
            if (match.Success)
            {
               return  match.Groups["Value"].Value;
            }
            return "";
        }
    }
}

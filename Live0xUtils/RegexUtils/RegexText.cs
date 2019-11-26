using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Live0xUtils.RegexUtils
{
    public class RegexText
    {
        public static bool HasVal(string src, string pattern)
        {
            Regex regex = new Regex(pattern);
            Match match = regex.Match(src);
            return match.Success;
        }

        public static string MatchVal(string src, string pattern,int des = 0)
        {
            Regex regex = new Regex(pattern);
            Match match = regex.Match(src);
            if (match.Success)
            {
                return match.Groups[des].Value;
            }
            return "";
        }

        public static string ReplaceChinese(string src)
        {
            return src.Replace(@"(\s[\u4E00 - \u9FA5] +) | ([\u4E00 - \u9FA5] +\s)",""); 
        }
    }
}

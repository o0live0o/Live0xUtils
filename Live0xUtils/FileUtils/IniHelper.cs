using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Live0xUtils.FileUtils
{
    public class IniHelper
    {
        //调用API函数
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 写如INI文件
        /// </summary>
        /// <param name="Section">主节点名称</param>
        /// <param name="Key">键名称</param>
        /// <param name="Value">键值</param>
        /// <param name="path">INI文件地址</param>
        public static void WriteIni(string Section, string Key, string Value,string filePath)
        {
            WritePrivateProfileString(Section, Key, Value, filePath);
        }


        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="Section">主节点名称</param>
        /// <param name="Key">键名称</param>
        /// <param name="path">INI文件地址</param>
        /// <returns>键值</returns>
        public static string ReadIni(string Section, string Key, string filePath)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, filePath);
            return temp.ToString();
        }
    }
}

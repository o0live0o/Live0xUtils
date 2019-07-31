using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Live0xUtils.ConfigUtils
{
    public class ReadAppConfig
    {
        public static string GetStringByTag(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? "";
        }
    }
}

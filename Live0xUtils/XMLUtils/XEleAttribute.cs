using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Live0xUtils.XMLUtils
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class XEleAttribute : Attribute
    {
        public string Ele;

        public XEleAttribute(string s)
        {
            this.Ele = s;
        }
    }
}

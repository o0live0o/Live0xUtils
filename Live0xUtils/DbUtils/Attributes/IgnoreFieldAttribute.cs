using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Live0xUtils.DbUtils.Attributes
{
    [AttributeUsage(AttributeTargets.All,AllowMultiple =true)]
    public class IgnoreFieldAttribute:Attribute
    {
    }
}

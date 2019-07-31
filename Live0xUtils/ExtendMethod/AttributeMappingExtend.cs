using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Live0xUtils.DbUtils.Attributes;

namespace Live0xUtils.ExtendMethod
{
    public static class AttributeMappingExtend
    {
        public static IEnumerable<PropertyInfo> FilterKey(this IEnumerable<PropertyInfo> properties)
        {
            return properties.Where(p=>!p.IsDefined(typeof(KeyFieldAttribute),true));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public static object SD_ChanageType(this object value, Type convertsionType)
        {
            //判断convertsionType类型是否为泛型，因为nullable是泛型类,
            if (convertsionType.IsGenericType &&
                //判断convertsionType是否为nullable泛型类
                convertsionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value.ToString().Length == 0)
                {
                    return null;
                }

                //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                NullableConverter nullableConverter = new NullableConverter(convertsionType);
                //将convertsionType转换为nullable对的基础基元类型
                convertsionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, convertsionType);
        }
    }
}

using Live0xUtils.DbUtils.Attributes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Live0xUtils.DbUtils
{
    public class GenerateParams<T>
    {
        public static SqlParameter[] Paras(T t)
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            return propertyInfos.Select(p => new SqlParameter($"@{p.Name}", p.GetValue(t, null) ?? DBNull.Value)).ToArray();
        }
    }
}

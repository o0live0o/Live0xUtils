using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Live0xUtils.DbUtils.SqlServer
{
    public abstract class FillHelper
    {
        public static T FillEnity<T>(T t, SqlDataReader dr)
        {
            PropertyInfo[] infos = t.GetType().GetProperties();
            foreach (var item in infos)
            {
                if (ExistField(dr, item.Name))
                {
                    try
                    {
                        if (!item.PropertyType.IsGenericType)
                        {
                            item.SetValue(t, dr[item.Name] == DBNull.Value ? null : Convert.ChangeType(dr[item.Name], item.PropertyType), null);
                        }
                        else
                        {
                            Type genericTypeDefinition = item.PropertyType.GetGenericTypeDefinition();
                            if (genericTypeDefinition == typeof(Nullable<>))
                            {
                                item.SetValue(t, dr[item.Name] == DBNull.Value ? null : Convert.ChangeType(dr[item.Name], item.PropertyType.GetGenericArguments()[0]), null);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return t;
        }

        private static bool ExistField(SqlDataReader dr, string field)
        {
            dr.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + field + "'";
            return (dr.GetSchemaTable().DefaultView.Count > 0);
        }
    }
}

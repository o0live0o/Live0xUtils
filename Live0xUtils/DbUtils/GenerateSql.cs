using Live0xUtils.ExtendMethod;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Live0xUtils.DbUtils
{
    public class GenerateSql<T>
    {
        public static string Insert(string key)
        {
            string tableName = typeof(T).Name;
            string filed = "";
            string val = "";
            filed = string.Join(",", typeof(T).GetProperties().Where(p => !p.Name.ToUpper().Equals(key.ToUpper())).Select(p => $"[{p.Name}]"));
            val = string.Join(",", typeof(T).GetProperties().Where(p => !p.Name.ToUpper().Equals(key.ToUpper())).Select(p => $"@{p.Name}"));
            string sql = $"INSERT INTO [{tableName}]({filed}) values ({val}) ";
            return sql;
        }

        public static string Insert()
        {
            string tableName = typeof(T).Name;
            string filed = "";
            string val = "";
            filed = string.Join(",", typeof(T).GetProperties().FilterKey().Select(p => $"[{p.Name}]"));
            val = string.Join(",", typeof(T).GetProperties().FilterKey().Select(p => $"@{p.Name}"));
            string sql = $"INSERT INTO [{tableName}]({filed}) values ({val}) ";
            return sql;
        }

        public  static string Update(string key)
        {
            string tableName = typeof(T).Name;
            string filed = "";
            filed = string.Join(",", typeof(T).GetProperties().Where(p => p.Name.ToUpper() != key.ToUpper()).Select(p => $"[{p.Name}] = @{p.Name}"));
            string sql = $"UPDATE [{tableName}] SET {filed} WHERE [{key}] = @{key} ";
            return sql;
        }

        public static string Query(string key)
        {
            string tableName = typeof(T).Name;
            string sql = $"SELECT * FROM  [{tableName}] WHERE [{key}] = @{key} ";
            return sql;
        }


    }
}

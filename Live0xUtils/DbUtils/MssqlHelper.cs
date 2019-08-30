using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Reflection;
using Live0xUtils.ExtendMethod;
using System.ComponentModel;

namespace Live0xUtils.DbUtils
{
    public class MssqlHelper
    {
        private static MssqlHelper mssqlHelper = null;
        private MssqlHelper() { }

        private static object lockObj = new object();

        private string DbName { get; set; }
        private string DbUser { get; set; }
        private string DbPwd { get; set; }
        private string DbServer { get; set; }
        private string _conStr { get; set; }


        public static MssqlHelper GetInstence()
        {
            lock (lockObj)
            {
                if (mssqlHelper == null)
                {
                    mssqlHelper = new MssqlHelper();
                }
            }
            return mssqlHelper;
        }

        /*
         * 初始化连接字符串
         */
        public void init(string dbName, string dbUser, string dbPwd, string dbServer)
        {
            this.DbName = dbName;
            this.DbUser = dbUser;
            this.DbPwd = dbPwd;
            this.DbServer = dbServer;
            _conStr = $"Server = {this.DbServer};Initial Catalog = {this.DbName};User Id = {this.DbUser};Password = {this.DbPwd};";
        }

        public bool TestConnect()
        {
            bool b = false;
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                try
                {
                    sqlConnection.Open();
                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        b = true;
                    }
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    LogUtils.ConsoleLog.Error("测试连接:" + ex.Message);
                }
                return b;
            }
        }

        public DataTable ExcuteDataTable(string sql, SqlParameter[] sqlParameters)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                DataTable dt = new DataTable();
                try
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
                    if (sqlParameters != null && sqlParameters.Length > 0)
                        sqlDataAdapter.SelectCommand.Parameters.AddRange(sqlParameters);
                    sqlDataAdapter.Fill(dt);
                }
                catch (Exception)
                {
                    throw;
                }
                return dt;
            }
        }

        public void ExcuteNonQuery(string sql, SqlParameter[] sqlParameters)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    if (sqlParameters != null && sqlParameters.Length > 0)
                        sqlCommand.Parameters.AddRange(sqlParameters);
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public T ExcuteEntity<T>(string sql, SqlParameter[] sqlParameters)
        {
            T t = Activator.CreateInstance<T>();
            PropertyInfo[] infos = t.GetType().GetProperties();
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.AddRange(sqlParameters);
                    SqlDataReader dr = sqlCommand.ExecuteReader();
                    if (dr.Read())
                    {
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
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return t;
            }
        }

        public List<T> ExcuteList<T>(string sql, SqlParameter[] sqlParameters)
        {
            List<T> list = new List<T>();
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    if(sqlParameters != null && sqlParameters.Length > 0)
                        sqlCommand.Parameters.AddRange(sqlParameters);
                    SqlDataReader dr = sqlCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        T t = Activator.CreateInstance<T>();
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
                                catch (Exception)
                                {
                                    throw;
                                }
                            }
                        }
                        list.Add(t);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return list;
            }
        }

        public void ExcuteProduce()
        {

        }

        private bool ExistField(SqlDataReader dr, string field)
        {
            dr.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + field + "'";
            return (dr.GetSchemaTable().DefaultView.Count > 0);
        }

        public bool Insert<T>(T t)
        {
            string tableName = typeof(T).Name;
            string filed = "";
            string val = "";
            filed = string.Join(",", typeof(T).GetProperties().FilterKey().Select(p => $"[{p.Name}]"));
            val = string.Join(",", typeof(T).GetProperties().FilterKey().Select(p => $"@{p.Name}"));
            string sql = $"INSERT INTO [{tableName}]({filed}) values ({val}) ";

            var parameters = typeof(T).GetProperties().FilterKey().Select(p => new SqlParameter($"@{p.Name}", p.GetValue(t, null) ?? DBNull.Value));

            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.Parameters.AddRange(parameters.ToArray());
                sqlConnection.Open();
                int i = sqlCommand.ExecuteNonQuery();
                return i == 1;
            }
        }

        public void CreatePara()
        {
        }

        public void CreateParas()
        {
        }

        private bool Exist<T>(T t, string key)
        {
            string tableName = typeof(T).Name;
            string sql = $"SELECT * FROM {tableName} WHERE {key} = @{key}";
            var parameters = typeof(T).GetProperties().Select(p => new SqlParameter($"@{p.Name}", p.GetValue(t, null) ?? DBNull.Value));
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.Parameters.AddRange(parameters.ToArray());
                sqlConnection.Open();
                SqlDataReader dr = sqlCommand.ExecuteReader();
                return dr.Read();
            }
        }

        private bool Update<T>(T t, string key)
        {
            string tableName = typeof(T).Name;
            string filed = "";
            filed = string.Join(",", typeof(T).GetProperties().FilterKey().Where(p => p.Name.ToUpper() != key.ToUpper()).Select(p => $"[{p.Name}] = @{p.Name}"));
            string sql = $"UPDATE [{tableName}] SET {filed} WHERE [{key}] = @{key} ";
            var parameters = typeof(T).GetProperties().Select(p => new SqlParameter($"@{p.Name}", p.GetValue(t, null) ?? DBNull.Value));
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.Parameters.AddRange(parameters.ToArray());
                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery() > 0;
            }
        }

        public bool InsertOrUpdate<T>(T t, string key)
        {
            string tableName = typeof(T).Name;
            if (Exist<T>(t, key))
            {
                return Update<T>(t, key);
            }
            else
            {
                return Insert<T>(t);
            }
        }

        public bool InsertOrUpdate<T>(T t, string[] keys)
        {
            string tableName = typeof(T).Name;
            if (Exist<T>(t, keys))
            {
                return Update<T>(t, keys);
            }
            else
            {
                return Insert<T>(t);
            }
        }

        public bool Update<T>(T t, string[] keys)
        {
            string tableName = typeof(T).Name;
            string filed = "";
            filed = string.Join(",", typeof(T).GetProperties().Where(p => !keys.Contains(p.Name)).Select(p => $"[{p.Name}] = @{p.Name}"));
            string sql = $"UPDATE [{tableName}] SET {filed} WHERE 1= 1";
            for (int i = 0; i < keys.Length; i++)
            {
                sql += $" AND [{keys[i]}]= @{keys[i]}";
            }

            var parameters = typeof(T).GetProperties().Select(p => new SqlParameter($"@{p.Name}", p.GetValue(t, null) ?? DBNull.Value));
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.Parameters.AddRange(parameters.ToArray());
                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery() > 0;
            }
        }

        public bool Update<T>(T t, string[] keyParams, string[] updateParams)
        {
            string tableName = typeof(T).Name;
            string filed = "";
            filed = string.Join(",", typeof(T).GetProperties().Where(p => updateParams.Contains(p.Name)).Select(p => $"[{p.Name}] = @{p.Name}"));
            string sql = $"UPDATE [{tableName}] SET {filed} WHERE 1= 1";
            for (int i = 0; i < keyParams.Length; i++)
            {
                sql += $" AND [{keyParams[i]}]= @{keyParams[i]}";
            }

            var parameters = typeof(T).GetProperties().Where(p => updateParams.Contains(p.Name) || keyParams.Contains(p.Name)).Select(p => new SqlParameter($"@{p.Name}", p.GetValue(t, null) ?? DBNull.Value));
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.Parameters.AddRange(parameters.ToArray());
                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery() > 0;
            }
        }

        public bool Exist<T>(T t, string[] keys)
        {
            string tableName = typeof(T).Name;
            string sql = $"SELECT * FROM {tableName} WHERE 1= 1 ";
            for (int i = 0; i < keys.Length; i++)
            {
                sql += $" AND [{keys[i]}] = @{keys[i]}";
            }
            var parameters = typeof(T).GetProperties().Select(p => new SqlParameter($"@{p.Name}", p.GetValue(t, null) ?? DBNull.Value));
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.Parameters.AddRange(parameters.ToArray());
                sqlConnection.Open();
                SqlDataReader dr = sqlCommand.ExecuteReader();
                return dr.Read();
            }
        }
    }
}

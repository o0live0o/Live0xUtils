using Live0xUtils.DbUtils.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Live0xUtils.DbUtils.Sqlite
{
    public class SqliteHelper
    {
        private static object lockObj = new object();
        SQLiteConnection _sqliteConnection = null;
        private string _conStr = "";
        private static SqliteHelper _sqliteHelper = new SqliteHelper();
 
        private SqliteHelper() {}

        static SqliteHelper() { }

        public static SqliteHelper GetInstance()
        {
            return _sqliteHelper;
        }

        public void init(string conn)
        {
            _conStr = $"data source = {conn}";
        }

        public Boolean OpenDb()
        {
            bool succ = false;
            try
            {
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(_conStr))
                {
                    sqliteConnection.Open();
                    if (sqliteConnection.State == ConnectionState.Open)
                    {
                        succ = true;
                    }
                    sqliteConnection.Close();
                    sqliteConnection.Dispose();
                    return succ;
                }
            }
            catch (Exception ex)
            {
                throw;
                //throw new Exception("打开数据库：" + _conStr + "的连接失败：" + ex.Message);
            }
            return succ;
        }

        public List<T> ExcuteEntitys<T>(string sql)
        {
            List<T> list = new List<T>();
            try
            {
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(_conStr))
                {
                    SQLiteCommand sqliteCommand = sqliteConnection.CreateCommand();
                    sqliteCommand.CommandText = sql;
                    sqliteConnection.Open();
                    Monitor.Enter(lockObj);
                    SQLiteDataReader dr = sqliteCommand.ExecuteReader();
                    Monitor.Exit(lockObj);
                    while (dr.Read())
                    {
                        T t = Activator.CreateInstance<T>();
                        foreach (var item in t.GetType().GetProperties())
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
                            }
                        }
                        list.Add(t);
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw;
            }
            return list;
        }

        public T ExcuteEntity<T>(string sql, Dictionary<string,string> dic)
        {
            T t = Activator.CreateInstance<T>();
            Monitor.Enter(lockObj);
            try
            {
                using (SQLiteConnection sqliteConnection = new SQLiteConnection(_conStr))
                {
                    string tableName = typeof(T).Name;
                    var parameters = dic.Select(p => new SQLiteParameter($"@{p.Key}", p.Value));
                    SQLiteCommand sqliteCommand = sqliteConnection.CreateCommand();
                    sqliteCommand.CommandText = sql;
                    sqliteCommand.Parameters.AddRange(parameters.ToArray());
                    sqliteConnection.Open();

                    SQLiteDataReader dr = sqliteCommand.ExecuteReader();
                    if (dr.Read())
                    {
                        foreach (var item in t.GetType().GetProperties())
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

                            }
                            
                        }
                    }
                    dr.Close();
                    sqliteCommand.Dispose();
                    sqliteConnection.Close();
                    sqliteConnection.Dispose();

                }
            }
            catch(Exception ex)
            {
                throw;
            }
            Monitor.Exit(lockObj);
            return t;
        }

        public bool InsertOrUpdate<T>(T t, string key)
        {
            bool succ = false;
            Monitor.Enter(lockObj);
            try
            {
                string tableName = typeof(T).Name;
                if (Exist<T>(t, key))
                {
                    succ = Update<T>(t, key);
                }
                else
                {
                    succ = Insert<T>(t, key);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Monitor.Exit(lockObj);
            return succ;
        }

        private bool Update<T>(T t, string key)
        {
            bool succ = false;
            string tableName = typeof(T).Name;
            string filed = "";

            PropertyInfo[] propertyInfos = t.GetType().GetProperties();
            List<string> keyList = new List<string>();
            if (!string.IsNullOrEmpty(key))
                keyList.Add(key);
            foreach (var keyItem in propertyInfos)
            {
                KeyFieldAttribute keyFieldAttribute = keyItem.GetCustomAttributes(typeof(KeyFieldAttribute), false).FirstOrDefault() as KeyFieldAttribute;
                if (keyFieldAttribute != null)
                {
                    keyList.Add(keyItem.Name);
                }
            }

            filed = string.Join(",", typeof(T).GetProperties().Where(p => !keyList.Contains(p.Name) && p.Name.ToUpper() != "ID").Select(p => $"[{p.Name}] = @{p.Name}"));
            string sql = $"UPDATE [{tableName}] SET {filed} WHERE 1 = 1 ";
            foreach (string keyStr in keyList)
            {
                sql += $" AND [{keyStr}] = @{keyStr} ";
            }
            var parameters = typeof(T).GetProperties().Select(p => new SQLiteParameter($"@{p.Name}", p.GetValue(t, null) ?? DBNull.Value));
            using (SQLiteConnection sqlConnection = new SQLiteConnection(_conStr))
            {
                sqlConnection.Open();
                using (SQLiteCommand sqlCommand = new SQLiteCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddRange(parameters.ToArray());
                   succ = sqlCommand.ExecuteNonQuery() > 0;
                }
                sqlConnection.Close();
                sqlConnection.Dispose();

            }
            return succ;
        }

        public bool Insert<T>(T t,string key)
        {
            string tableName = typeof(T).Name;
            string filed = "";
            string val = "";
            filed = string.Join(",", typeof(T).GetProperties().Select(p => $"[{p.Name}]"));
            val = string.Join(",", typeof(T).GetProperties().Select(p => $"@{p.Name}"));
            string sql = $"INSERT INTO [{tableName}]({filed}) values ({val}) ";
            var parameters = typeof(T).GetProperties().Select(p => new SQLiteParameter($"@{p.Name}", p.GetValue(t, null) ?? DBNull.Value));
            int i = 0;
            using (SQLiteConnection sqlConnection = new SQLiteConnection(_conStr))
            {

                sqlConnection.Open();
                using (SQLiteCommand sqlCommand = new SQLiteCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddRange(parameters.ToArray());
                    i  = sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();

            }
            return i == 1;

        }

        private bool Exist<T>(T t, string key)
        {
            string tableName = typeof(T).Name;
            PropertyInfo[] propertyInfos = t.GetType().GetProperties();
            List<string> keyList = new List<string>();
            if (!string.IsNullOrEmpty(key))
                keyList.Add(key);
            foreach (var keyItem in propertyInfos)
            {
                KeyFieldAttribute keyFieldAttribute = keyItem.GetCustomAttributes(typeof(KeyFieldAttribute), false).FirstOrDefault() as KeyFieldAttribute;
                if (keyFieldAttribute != null)
                {
                    keyList.Add(keyItem.Name);
                }
            }
            string sql = $"SELECT * FROM {tableName} WHERE 1 = 1";
            foreach (string keyStr in keyList)
            {
                sql += $" AND {keyStr} = @{keyStr} ";
            }
            bool succ = false;
            var parameters = typeof(T).GetProperties().Select(p => new SQLiteParameter($"@{p.Name}", p.GetValue(t, null) ?? DBNull.Value));
            using (SQLiteConnection sqlConnection = new SQLiteConnection(_conStr))
            {
                sqlConnection.Open();
                using (SQLiteCommand sqlCommand = new SQLiteCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddRange(parameters.ToArray());
                    SQLiteDataReader dr = sqlCommand.ExecuteReader();
                     succ = dr.Read();
                    dr.Close();
                    
                    sqlCommand.Dispose();
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }
            return succ;
        }

        private bool ExistField(SQLiteDataReader dr, string field)
        {
            DataTable dt = dr.GetSchemaTable();
            foreach (DataRow item in dt.Rows)
            {
                if (item[0].Equals(field))
                    return true;
            }
            return false;
        }

        public  void Query(string conn)
        {
            SQLiteConnection sqliteConnection = new SQLiteConnection(@"data source= D:\MCode\SqliteDB\yzslz.db");
            sqliteConnection.Open();
            //SQLiteCommand sqliteCommand =  sqliteConnection.CreateCommand();
            //sqliteCommand.CommandText = "SELECT * FROM RESULT_BRAKE";
            //SQLiteDataReader dr = sqliteCommand.ExecuteReader();
            SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter("SELECT * FROM RESULT_BRAKE WHERE JCLSH = @JCLSH", sqliteConnection);
            sQLiteDataAdapter.SelectCommand.Parameters.Add(new SQLiteParameter("@JCLSH", "T190518P91410230010-01"));
            DataTable dt = new DataTable();
            sQLiteDataAdapter.Fill(dt);
            int i = 0;

        }

        public object QueryObject(string commandText, Hashtable hashtable)
        {
            Monitor.Enter(lockObj);
            using (SQLiteConnection sqlConnection = new SQLiteConnection(_conStr))
            {
                try
                {
                    sqlConnection.Open();
                    using (SQLiteCommand sqlCommand = new SQLiteCommand(commandText, sqlConnection))
                    {        
                        object o = sqlCommand.ExecuteScalar();
                        sqlCommand.Parameters.Clear();
                        Monitor.Exit(lockObj);
                        return o;
                    }
                }
                catch (Exception)
                {
                    Monitor.Exit(lockObj);
                    throw;
                }
            }
        }
    }
}

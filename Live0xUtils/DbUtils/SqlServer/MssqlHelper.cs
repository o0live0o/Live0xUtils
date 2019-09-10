using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Live0xUtils.DbUtils.SqlServer
{
    public  class MssqlHelper 
    {
        private static MssqlHelper _mssqlHelper = null;

        private string _server { get; set; }

        private string _database { get; set; }

        private string _user { get; set; }

        private string _password { get; set; }

        private string _connectionStr { get; set; }

        private MssqlHelper() { }

        static MssqlHelper()
        {
            _mssqlHelper = new MssqlHelper();
        }

        public static MssqlHelper GetInstance() { return _mssqlHelper; }

        public bool Connect()
        {
            throw new NotImplementedException();
        }

        public int ExcuteNonQuery(string commandText, Hashtable hashtable)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionStr))
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    PrepareCommand(sqlCommand, sqlConnection, null, CommandType.Text, commandText, hashtable);
                    int i = sqlCommand.ExecuteNonQuery();
                    sqlCommand.Parameters.Clear();
                    return i;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exist<T>(T t, Hashtable hashtable, string[] keys, string tableName = "")
        {
            if (string.IsNullOrEmpty(tableName))
                tableName = typeof(T).Name;

            string commandText = $"SELECT * FROM {tableName} WHERE 1= 1 ";
            for (int i = 0; i < keys.Length; i++)
            {
                commandText += $" AND [{keys[i]}] = @{keys[i]}";
            }

            using (SqlConnection sqlConnection = new SqlConnection(_connectionStr))
            {
                SqlCommand sqlCommand = new SqlCommand();
                PrepareCommand(sqlCommand, sqlConnection, null, CommandType.Text, commandText, hashtable);
                SqlDataReader dr = sqlCommand.ExecuteReader();
                return dr.Read();
            }
        }

        public void Init(string server, string database, string user, string password)
        {
            this._server = server;
            this._database = database;
            this._user = user;
            this._password = password;
            this._connectionStr = $"Server = {this._server};Initial Catalog = {this._database};User Id = {this._user};Password = {this._password};";
        }

        public bool Insert<T>(T t, Hashtable hashtable, string[] ignoreFields,string tableName = "")
        {
            if(string.IsNullOrEmpty(tableName))
                tableName = typeof(T).Name;
            string filed = "";
            string val = "";
            filed = string.Join(",", typeof(T).GetProperties().Where(p => !ignoreFields.Contains(p.Name)).Select(p => $"[{p.Name}]"));
            val = string.Join(",", typeof(T).GetProperties().Where(p => !ignoreFields.Contains(p.Name)).Select(p => $"@{p.Name}"));
            string sql = $"INSERT INTO [{tableName}]({filed}) values ({val}) ";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionStr))
            {
                SqlCommand sqlCommand = new SqlCommand();
                PrepareCommand(sqlCommand, sqlConnection, null, CommandType.Text, sql, hashtable);
                int i = sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                return i == 1;
            }
        }

        public bool InsertOrUpdate<T>(T t, Hashtable hashtable, string[] keys, string[] ignoreFields, string tableName = "")
        {
            if (Exist<T>(t, hashtable, keys, tableName))
            {
                return Update<T>(t, hashtable, keys, ignoreFields, tableName);
            }
            else
            {
                return Insert<T>(t, hashtable, ignoreFields, tableName);
            }
        }

        public T Query<T>(string commandText, Hashtable hashtable)
        {
            T t = Activator.CreateInstance<T>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionStr))
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    PrepareCommand(sqlCommand, sqlConnection, null, CommandType.Text, commandText, hashtable);
                    SqlDataReader dr = sqlCommand.ExecuteReader();
                    sqlCommand.Parameters.Clear();
                    if (dr.Read())
                    {
                        FillHelper.FillEnity<T>(t, dr);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return t;
            }
        }

        public IEnumerable<T> QueryList<T>(string commandText, Hashtable hashtable)
        {
            List<T> list = new List<T>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionStr))
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    PrepareCommand(sqlCommand, sqlConnection, null, CommandType.Text, commandText, hashtable);
                    SqlDataReader dr = sqlCommand.ExecuteReader();
                    sqlCommand.Parameters.Clear();
                    while (dr.Read())
                    {
                        T t = Activator.CreateInstance<T>();
                        FillHelper.FillEnity<T>(t, dr);
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

        public object QueryObject(string commandText, Hashtable hashtable)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionStr))
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    PrepareCommand(sqlCommand, sqlConnection, null, CommandType.Text, commandText, hashtable);
                    object o = sqlCommand.ExecuteScalar();
                    sqlCommand.Parameters.Clear();
                    return o;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public DataTable QueryTable(string commandText, Hashtable hashtable)
        {
            throw new NotImplementedException();
        }

        public bool Update<T>(T t, Hashtable hashtable, string[] keys, string[] ignoreFields, string tableName = "")
        {
             if(string.IsNullOrEmpty(tableName))
                tableName = typeof(T).Name;
            string filed = "";
            filed = string.Join(",", typeof(T).GetProperties().Where(p => !keys.Contains(p.Name) && !ignoreFields.Contains(p.Name)).Select(p => $"[{p.Name}] = @{p.Name}"));

            string sql = $"UPDATE [{tableName}] SET {filed} WHERE 1 = 1 ";
            foreach (string item in keys)
            {
                sql += $" AND {item} = @{item} ";
            }
            using (SqlConnection sqlConnection = new SqlConnection(_connectionStr))
            {
                SqlCommand sqlCommand = new SqlCommand();
                PrepareCommand(sqlCommand, sqlConnection, null, CommandType.Text, sql, hashtable);
                return sqlCommand.ExecuteNonQuery() > 0;
            }
        }

        #region 存储过程
        public void SP_Excute(string commandText,Hashtable hashtable)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionStr))
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    PrepareCommand(sqlCommand, sqlConnection, null, CommandType.StoredProcedure, commandText, hashtable);
                    int i = sqlCommand.ExecuteNonQuery();
                    sqlCommand.Parameters.Clear();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion

        private void PrepareParameters(List<SqlParameter> sqlParameters, Hashtable hashtable)
        {
            if (hashtable == null)
                return;
            foreach (string key in hashtable.Keys)
            {
                if (hashtable[key] == null)
                    hashtable[key] = DBNull.Value;
                SqlParameter sqlParameter = new SqlParameter() { ParameterName = "@" + key, Value = hashtable[key] };
                sqlParameters.Add(sqlParameter);
            }
        }

        private  void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, Hashtable hashtable)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            // 给命令分配一个数据库连接.
            command.Connection = connection;

            // 设置命令文本(存储过程名或SQL语句)
            command.CommandText = commandText;

            // 分配事务
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // 设置命令类型.
            command.CommandType = commandType;

            // 分配命令参数
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            PrepareParameters(sqlParameters, hashtable);
            if(sqlParameters != null && sqlParameters.Count > 0)
              command.Parameters.AddRange(sqlParameters.ToArray());            
            return;
        }
    }
}

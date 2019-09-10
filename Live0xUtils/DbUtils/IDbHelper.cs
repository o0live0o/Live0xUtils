using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Live0xUtils.DbUtils
{
    interface IDbHelper
    {
        void Init(string server,string database,string user,string password);

        bool Connect();

        bool Insert<T>(T t,string[] ignoreFields,string tableName="");

        bool Update<T>(T t, string[] keys, string[] ignoreFields,string tableName= "");

        bool Exist<T>(T t, string[] keys, string tableName = "");

        bool InsertOrUpdate<T>(T t,string[] keys, string[] ignoreFields, string tableName = "");

        T Query<T>(string commandText, Hashtable hashtable);

        IEnumerable<T> QueryList<T>(string commandText, Hashtable hashtable);

        DataTable QueryTable(string commandText, Hashtable hashtable);

        object QueryObject(string commandText, Hashtable hashtable);

        int ExcuteNonQuery(string commandText, Hashtable hashtable);
    }
}

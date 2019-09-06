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
        void Init();

        bool Connect();

        int ExcuteNonQuery(string sql, Hashtable hashtable);

        bool Insert<T>(T t,string[] ignoreFields,string tableName="");

        bool Update<T>(T t, string[] keys, string[] ignoreFields,string tableName= "");

        bool Exist<T>(T t, string[] keys, string tableName = "");

        bool InsertOrUpdate<T>(T t,string[] keys, string[] ignoreFields, string tableName = "");

        DataTable ExcuteDataTable(string sql, Hashtable hashtable);

        IEnumerable<T> ExcuteList<T>(string sql, Hashtable hashtable);



    }
}

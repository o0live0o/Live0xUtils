using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Live0xUtils.DbUtils
{
    interface IDbHelperDeveloper
    {
        string DatabaseName { get; set; }

        string DatabaseUserName { get; set; }

        string DatabasePwdName { get; set; }

        string DatabaseServer { get; set; }

        bool Connect();

        int ExcuteNonQuery(string sql);

        int ExcuteNonQuery(string sql, Hashtable hashtable);

        T ExcuteEntity<T>(string sql);

        T ExcuteEntity<T>(string sql, Hashtable hashtable);

        T ExcuteEntity<T>(Func<T, bool> func);

        IEnumerable<T> ExcuteEntitys<T>(string sql);

        IEnumerable<T> ExcuteEntitys<T>(string sql, Hashtable hashtable);

        bool Exists<T>(T t);

        bool Exists<T>(T t,Hashtable hashtable);

        object ExcuteScalar(string sql);

        object ExcuteScalar(string sql, Hashtable hashtable);

        T ExcuteScalar<T>(string sql);

        T ExcuteScalar<T>(string sql, Hashtable hashtable);

        int Update<T>(T t);

        int Insert<T>(T t);

        int InsertOrUpdate<T>(T t);

        int ExcuteStoredProcedure(string proc, Hashtable hashtable);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Live0xUtils.LogUtils
{
   
    public class TxtLog
    {
        private static object lockObj = new object();
        public static void Append(string path,string log)
        {
            try
            {
                Monitor.Enter(lockObj);
                log = $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} {log}\r\n";
                File.AppendAllText(path, log);
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
        }

        public static void Append(string path, string fileName, string log)
        {
            try
            {
                Monitor.Enter(lockObj);
                string fullName = path + "\\" + fileName;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                if (!File.Exists(fullName))
                    File.Create(fullName);
                log = $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} {log}\r\n";
                File.AppendAllText(fullName, log);
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
        }



        public static void Append(string path, string log,LogType logType)
        {
            try
            {
                Monitor.Enter(lockObj);
                log = $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}_[{logType.ToString()}]_ {log}\r\n";
                File.AppendAllText(path, log);
            }
            finally
            {
                Monitor.Exit(lockObj);
            }
        }

    }
}

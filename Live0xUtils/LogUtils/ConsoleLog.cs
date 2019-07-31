using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Live0xUtils.LogUtils
{
    public class ConsoleLog
    {
        public static void Trace(string message) { log(LogType.Trace, message); }

        /// <inheritdoc />
        public static void Debug(string message) { log(LogType.Debug, message); }

        /// <inheritdoc />
        public static void Fatal(string message) { log(LogType.Fatal, message); }

        /// <inheritdoc />
        public static void Fatal(string message, Exception ex) { log(LogType.Fatal, message); }

        /// <inheritdoc />
        public static void Info(string message) { log(LogType.Info, message); }

        /// <inheritdoc />
        public static void Warn(string message) { log(LogType.Warn, message); }

        /// <inheritdoc />
        public static void Error(string message) { log(LogType.Error, message); }

        public static void log(LogType logType, string message)
        {
            switch (logType)
            {
                case LogType.Debug:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogType.Fatal:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case LogType.Trace:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogType.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}

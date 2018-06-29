using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Angus.ISoft.Boilerplate.Infrastructure.Log
{
    public class LoggerHelper
    {
        readonly static ILog debug = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        readonly static ILog info = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        readonly static ILog warn = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        readonly static ILog error = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        readonly static ILog fatal = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Error(string errorMsg, Exception ex = null)
        {
            if (ex != null) error.Error(errorMsg, ex);
            else error.Error(errorMsg);
        }

        public static void Info(string msg)
        {
            info.Info(msg);
        }

        public static void Debug(string msg)
        {
            debug.Debug(msg);
        }

        public static void Warn(string msg)
        {
            warn.Warn(msg);
        }

        public static void Fatal(string msg, Exception ex = null)
        {
            if (ex != null) fatal.Fatal(msg, ex);
            else fatal.Fatal(msg);
        }
    }
}

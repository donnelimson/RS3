using log4net;
using log4net.Config;
using Codebiz.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Domain.Common.Model.DTOs;

namespace Logging
{
    public static class Logger
    {
        private static readonly List<string> Loggers = new List<string>();

        public static void Configure(FileInfo configFile, string loggerName, params string[] otherLoggers)
        {
            XmlConfigurator.Configure(configFile);
            Logger.Loggers.Add(loggerName);
            Logger.Loggers.AddRange((IEnumerable<string>)otherLoggers);
        }

        public static void Configure(string loggerName, params string[] otherLoggers)
        {
            XmlConfigurator.Configure();
            Logger.Loggers.Add(loggerName);
            Logger.Loggers.AddRange((IEnumerable<string>)otherLoggers);
        }

        public static void Debug(string format, params object[] arguments)
        {
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).DebugFormat(format, arguments)));
        }

        public static void Debug(object message, LogEventTitles? logEventTitle = null)
        {
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = (int?)logEventTitle;
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).Debug(message)));
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = null;
        }

        public static void Debug(string message, LogEventTitles? logEventTitle = null)
        {
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = (int?)logEventTitle;
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).Debug((object)message)));
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = null;
        }

        public static void Info(string format, params object[] arguments)
        {
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).InfoFormat(format, arguments)));
        }

        public static void Info(object message, LogEventTitles? logEventTitle = null)
        {
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = (int?)logEventTitle;
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).Info(message)));
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = null;
        }

        public static void Info(string message, LogEventTitles? logEventTitle = null, string attachments="", string fileUploadAttachments="", string changes="")
        {
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = (int?)logEventTitle;
            log4net.LogicalThreadContext.Properties["attachments"] = attachments;
            log4net.LogicalThreadContext.Properties["changes"] = changes;
            log4net.LogicalThreadContext.Properties["fileUploadAttachments"] = fileUploadAttachments;
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).Info((object)message)));
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = null;
        }

        public static void Warn(string format, params object[] arguments)
        {
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).WarnFormat(format, arguments)));
        }

        public static void Warn(object message, LogEventTitles? logEventTitle = null)
        {
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = (int?)logEventTitle;
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).Warn(message)));
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = null;
        }

        public static void Warn(string message, LogEventTitles? logEventTitle = null)
        {
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = (int?)logEventTitle;
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).Warn((object)message)));
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = null;
        }

        public static void Error(string message, params object[] arguments)
        {
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).ErrorFormat(message, arguments)));
        }

        public static void Error(object message, LogEventTitles? logEventTitle = null)
        {
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = (int?)logEventTitle;
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).Error(message)));
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = null;
        }

        public static void Error(string message, Exception exception, LogEventTitles? logEventTitle = null)
        {
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = (int?)logEventTitle;
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).Error((object)message, exception)));
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = null;
        }

        public static void Fatal(string message, params object[] arguments)
        {
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).FatalFormat(message, arguments)));
        }

        public static void Fatal(object message, Exception exception, LogEventTitles? logEventTitle = null)
        {
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = (int?)logEventTitle;
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).Fatal(message, exception)));
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = null;
        }

        public static void Fatal(string message, Exception exception, LogEventTitles? logEventTitle = null)
        {
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = (int?)logEventTitle;
            Logger.Loggers.ForEach((Action<string>)(l => Logger.GetLogger(l).Fatal((object)message, exception)));
            log4net.LogicalThreadContext.Properties["eventlogtitle"] = null;
        }

        private static ILog GetLogger(string loggerName)
        {
            string typeInfo = Logger.GetTypeInfo();
            return LogManager.GetLogger(loggerName + "." + typeInfo);
        }

        private static string GetTypeInfo()
        {
            MethodBase method = new StackTrace().GetFrame(5).GetMethod();
            return method.DeclaringType.ToString() + "." + method.Name;
        }
    }
}

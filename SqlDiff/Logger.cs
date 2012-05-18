using System;
using System.Linq;
using log4net;
using log4net.Core;

namespace AdrianBanks.SqlDiff
{
    public interface ILogger
    {
        void Verbose(string message, params object[] args);
        void Debug(string message, params object[] args);
        void Info(string message, params object[] args);
    }

    public sealed class Logger : ILogger
    {
        private readonly ILog log;

        public Logger(Type type)
        {
            log = LogManager.GetLogger(type);
        }

        public Logger(string loggerName)
        {
            log = LogManager.GetLogger(loggerName);
        }

        public void Verbose(string message, params object[] args)
        {
            if (log.Logger.IsEnabledFor(Level.Verbose))
            {
                log.Logger.Log(typeof(ILog), Level.Verbose, Format(message, args), null);
            }
        }

        public void Debug(string message, params object[] args)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(Format(message, args));
            }
        }

        public void Info(string message, params object[] args)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(Format(message, args));
            }
        }

        private static string Format(string message, params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                return message;
            }
            		
            args = args.Select(a => a ?? "<null>").ToArray();
            return string.Format(message, args);
        }
    }
}

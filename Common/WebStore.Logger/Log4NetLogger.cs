using System;
using System.Reflection;
using System.Xml;
using log4net;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _Log;

        public Log4NetLogger(string CategoryName, XmlElement xml)
        {
            var logger_repository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            _Log = LogManager.GetLogger(logger_repository.Name, CategoryName);

            log4net.Config.XmlConfigurator.Configure(logger_repository, xml);
        }

        public bool IsEnabled(LogLevel LogLevel)
        {
            switch (LogLevel)
            {
                default: throw new ArgumentOutOfRangeException(nameof(LogLevel), LogLevel, null);

                case LogLevel.Trace:
                case LogLevel.Debug:
                    return _Log.IsDebugEnabled;

                case LogLevel.Information:
                    return _Log.IsInfoEnabled;

                case LogLevel.Warning:
                    return _Log.IsWarnEnabled;

                case LogLevel.Error:
                    return _Log.IsErrorEnabled;

                case LogLevel.Critical:
                    return _Log.IsFatalEnabled;

                case LogLevel.None:
                    return false;
            }
        }

        public void Log<TState>(LogLevel LogLevel, EventId Id, TState State, Exception exception, Func<TState, Exception, string> formatter)
        {
            if(!IsEnabled(LogLevel)) return;
            if(formatter is null) throw new ArgumentNullException(nameof(formatter));

            var msg = formatter(State, exception);

            if(string.IsNullOrEmpty(msg) && exception is null) return;

            switch (LogLevel)
            {
                default: throw new ArgumentOutOfRangeException(nameof(LogLevel), LogLevel, null);

                case LogLevel.Trace:
                case LogLevel.Debug:
                    _Log.Debug(msg);
                    break;
                case LogLevel.Information:
                    _Log.Info(msg);
                    break;
                case LogLevel.Warning:
                    _Log.Warn(msg);
                    break;
                case LogLevel.Error:
                    _Log.Error(msg ?? exception.ToString());
                    break;
                case LogLevel.Critical:
                    _Log.Fatal(msg ?? exception.ToString());
                    break;
                case LogLevel.None:
                    break;
            }
        }

        public IDisposable BeginScope<TState>(TState State) => null;
    }
}
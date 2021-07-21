using System;
using Common.Logging;
using Microsoft.Extensions.Logging;

namespace Wise.Framework.Presentation.Logging
{
    /// <summary>
    ///     <see cref="ILoggerFacade" />
    ///     this is default implementation of <see cref="ILoggerFacade" /> used for loging unhandled exception by prism
    ///     framework
    /// </summary>
    public class DefaultLoggerFacade : ILogger
    {
        /// <summary>
        ///     default logger
        /// </summary>
        private readonly ILog log;
        /// <summary>
        ///     ctor.
        /// </summary>
        public DefaultLoggerFacade(ILog log)
        {
            this.log = log;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return log.IsDebugEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return log.IsErrorEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return log.IsInfoEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return log.IsWarnEnabled;
                default:
                    throw new ArgumentException("category");
            }
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                     log.Debug(formatter.Invoke(state, exception));
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    log.Error(formatter.Invoke(state, exception));
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    log.Info(formatter.Invoke(state, exception));
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    log.Warn(formatter.Invoke(state, exception));
                    break;
                default:
                    throw new ArgumentException("category");
            }
        }
    }
}
using System;
using Common.Logging;
using Microsoft.Practices.Prism.Logging;

namespace Wise.Framework.Logging
{
    /// <summary>
    /// <see cref="ILoggerFacade"/>
    /// this is default implementation of <see cref="ILoggerFacade"/> used for loging unhandled exception by prism framework
    /// </summary>
    public class DefaultLoggerFacade : ILoggerFacade
    {

        /// <summary>
        /// default logger 
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// ctor.
        /// </summary>
        public DefaultLoggerFacade()
        {
            log = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// <see cref="ILoggerFacade.Log"/>
        /// used in case of handling every not handled exception
        /// </summary>
        public void Log(string message, Category category, Priority priority)
        {
            
            switch (category)
            {
                case Category.Debug:
                    log.Debug(message);
                    break;
                case Category.Exception:
                    log.Error(message);
                    break;
                case Category.Info:
                    log.Info(message);
                    break;
                case Category.Warn:
                    log.Warn(message);
                    break;
                default:
                    throw new ArgumentException("category");

            }
        }
    }
}

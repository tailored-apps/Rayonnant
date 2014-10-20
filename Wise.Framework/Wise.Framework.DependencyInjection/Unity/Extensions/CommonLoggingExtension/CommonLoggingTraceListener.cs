// File: CommonLoggingTraceListener.cs
// Project Name: ILogInject.Unity
// Project Home: https://github.com/trondr/ILogInject/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/ILogInject/blob/master/License.md
// Credits: http://blog.baltrinic.com/software-development/dotnet/log4net-integration-with-unity-ioc-container
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System.Diagnostics;
using Common.Logging;

namespace Wise.Framework.DependencyInjection.Unity.Extensions.CommonLoggingExtension
{
    public class CommonLoggingTraceListener : TraceListener
    {
        private readonly ILog _log;

        public CommonLoggingTraceListener()
        {
            _log = LogManager.GetLogger("System.Diagnostics.Redirection");
        }

        public CommonLoggingTraceListener(ILog log)
        {
            _log = log;
        }

        public override void Write(string message)
        {
            if (_log != null)
            {
                if (_log.IsDebugEnabled) _log.Debug(message);
            }
        }

        public override void WriteLine(string message)
        {
            if (_log != null)
            {
                if (_log.IsDebugEnabled) _log.Debug(message);
            }
        }
    }
}
// File: CommonLoggingLogBuildPlanPolicy.cs
// Project Name: ILogInject.Unity
// Project Home: https://github.com/trondr/ILogInject/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/ILogInject/blob/master/License.md
// Credits: http://blog.baltrinic.com/software-development/dotnet/log4net-integration-with-unity-ioc-container
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using Common.Logging;
using Unity.Builder;
using Unity.Policy;
using Unity.Resolution;
using Unity.Strategies;

namespace TailoredApps.Rayonnant.DependencyInjection.Unity.Extensions.CommonLoggingExtension
{
    public class CommonLoggingLogBuildPlanPolicy : IResolveDelegateFactory
    {
        public CommonLoggingLogBuildPlanPolicy(Type typeForLog)
        {
            LogType = typeForLog;
        }

        public Type LogType { get; private set; }

        public void BuildUp(ref BuilderContext context)
        {
            if (context.Existing == null)
            {
                ILog log = LogManager.GetLogger(LogType);
                context.Existing = log;
            }
        }

        public ResolveDelegate<BuilderContext> GetResolver(ref BuilderContext context)
        {
            throw new NotImplementedException();
        }
    }
}
// File: CommonLoggingLogBuildPlanPolicy.cs
// Project Name: ILogInject.Unity
// Project Home: https://github.com/trondr/ILogInject/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/ILogInject/blob/master/License.md
// Credits: http://blog.baltrinic.com/software-development/dotnet/log4net-integration-with-unity-ioc-container
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using Common.Logging;
using Microsoft.Practices.ObjectBuilder2;

namespace Wise.Framework.DependencyInjection.Unity.Extensions.CommonLoggingExtension
{
    public class CommonLoggingLogBuildPlanPolicy : IBuildPlanPolicy
    {
        public Type LogType { get; private set; }

        public CommonLoggingLogBuildPlanPolicy(Type typeForLog)
        {
            LogType = typeForLog;
        }

        public void BuildUp(IBuilderContext context)
        {
            if (context.Existing == null)
            {                
                ILog log = LogManager.GetLogger(LogType);                
                context.Existing = log;
            }
        }
    }
}
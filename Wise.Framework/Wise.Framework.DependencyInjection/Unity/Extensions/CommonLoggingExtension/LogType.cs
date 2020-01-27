// File: LogType.cs
// Project Name: ILogInject.Unity
// Project Home: https://github.com/trondr/ILogInject/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/ILogInject/blob/master/License.md
// Credits: http://blog.baltrinic.com/software-development/dotnet/log4net-integration-with-unity-ioc-container
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.ObjectBuilder;
using Unity.Builder;
using Wise.Framework.DependencyInjection.Unity.Extensions.BuildTrackingExtension;

namespace Wise.Framework.DependencyInjection.Unity.Extensions.CommonLoggingExtension
{
    public class LogType
    {
        public static Type Get(IBuilderContext context)
        {
            Type logType = null;
            IBuildTrackingPolicy buildTrackingPolicy = BuildTracking.GetPolicy(context);
            if ((buildTrackingPolicy != null) && buildTrackingPolicy.BuildKeys.Count >= 2)
            {
                logType = ((NamedTypeBuildKey) buildTrackingPolicy.BuildKeys.ElementAt(1)).Type;
            }
            else
            {
                var stackTrace = new StackTrace();
                //First two stack frames are in the log creation strategy, skip it
                for (int i = 2; i < stackTrace.FrameCount; i++)
                {
                    StackFrame stackFrame = stackTrace.GetFrame(i);
                    logType = stackFrame.GetMethod().DeclaringType;
                    if (logType != null && !logType.FullName.StartsWith("Microsoft.Practices"))
                    {
                        break;
                    }
                }
            }
            return logType;
        }
    }
}
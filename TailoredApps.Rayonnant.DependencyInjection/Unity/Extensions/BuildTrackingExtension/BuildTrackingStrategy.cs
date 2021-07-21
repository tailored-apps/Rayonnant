// File: BuildTrackingStrategy.cs
// Project Name: ILogInject.Unity
// Project Home: https://github.com/trondr/ILogInject/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/ILogInject/blob/master/License.md
// Credits: http://blog.baltrinic.com/software-development/dotnet/log4net-integration-with-unity-ioc-container
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using Common.Logging;
using Unity.Builder;
using Unity.Strategies;

namespace TailoredApps.Rayonnant.DependencyInjection.Unity.Extensions.BuildTrackingExtension
{
    //todo
    public class BuildTrackingStrategy : BuilderStrategy
    {
        public override void PreBuildUp(ref BuilderContext context)
        {
            
            //IBuildTrackingPolicy policy = BuildTracking.GetPolicy(context) ?? BuildTracking.SetPolicy(context);
            //policy.BuildKeys.Push(context.BuildKey);
        }

        public override void PostBuildUp(ref BuilderContext context)
        {
            //IBuildTrackingPolicy policy = BuildTracking.GetPolicy(context);
            //if ((policy != null) && (policy.BuildKeys.Count > 0))
            //{
            //    policy.BuildKeys.Pop();
            //}
        }
    }
}
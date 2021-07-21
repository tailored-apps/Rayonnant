﻿// File: CommonLoggingLogCreationStrategy.cs
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

namespace TailoredApps.Rayonnant.DependencyInjection.Unity.Extensions.CommonLoggingExtension
{
    //todo
    public class CommonLoggingLogCreationStrategy : BuilderStrategy
    {
        public bool IsPolicySet { get; private set; }

        public override void PreBuildUp(ref BuilderContext context)
        {
            //if (typeof(ILog) == context.BuildKey.Type)
            //{
            //    if (context.Policies.Get<IBuildPlanPolicy>(context.BuildKey) == null)
            //    {
            //        Type typeForLog = LogType.Get(context);
            //        IBuildPlanPolicy buildPlanPolicy = new CommonLoggingLogBuildPlanPolicy(typeForLog);
            //        context.Policies.Set(buildPlanPolicy, context.BuildKey);
            //        IsPolicySet = true;
            //    }
            //}
        }

        public override void PostBuildUp(ref BuilderContext context)
        {
        //    if (IsPolicySet)
        //    {
        //        context.Policies.Clear<IBuildPlanPolicy>(context.BuildKey);
        //        IsPolicySet = false;
        //    }
        }
    }
}
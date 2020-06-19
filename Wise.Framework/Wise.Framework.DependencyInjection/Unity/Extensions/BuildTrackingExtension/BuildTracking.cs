// File: BuildTracking.cs
// Project Name: ILogInject.Unity
// Project Home: https://github.com/trondr/ILogInject/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/ILogInject/blob/master/License.md
// Credits: http://blog.baltrinic.com/software-development/dotnet/log4net-integration-with-unity-ioc-container
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using Microsoft.Practices.ObjectBuilder;
using System;
using Unity;
using Unity.Builder;
using Unity.Extension;

namespace Wise.Framework.DependencyInjection.Unity.Extensions.BuildTrackingExtension
{
    //todo
    public class BuildTracking : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Context.Strategies.Add(new BuildTrackingStrategy() ,UnityBuildStage.TypeMapping);
        }

        internal static IBuildTrackingPolicy GetPolicy(IBuilderContext context)
        {
           return new BuildTrackingPolicy();
        }

        //public static IBuildTrackingPolicy GetPolicy(IBuilderContext context)
        //{
        //  //  return context.Policies.Get<IBuildTrackingPolicy>(context.BuildKey);
        //}

        //public static IBuildTrackingPolicy SetPolicy(IBuilderContext context)
        //{
        //    IBuildTrackingPolicy policy = new BuildTrackingPolicy();
        //    context.Policies.SetDefault(policy);
        //    return policy;
        //}
    }
}
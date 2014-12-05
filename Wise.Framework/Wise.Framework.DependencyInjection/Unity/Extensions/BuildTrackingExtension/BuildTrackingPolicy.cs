// File: BuildTrackingPolicy.cs
// Project Name: ILogInject.Unity
// Project Home: https://github.com/trondr/ILogInject/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/ILogInject/blob/master/License.md
// Credits: http://blog.baltrinic.com/software-development/dotnet/log4net-integration-with-unity-ioc-container
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System.Collections.Generic;

namespace Wise.Framework.DependencyInjection.Unity.Extensions.BuildTrackingExtension
{
    public class BuildTrackingPolicy : IBuildTrackingPolicy
    {
        public BuildTrackingPolicy()
        {
            BuildKeys = new Stack<object>();
        }

        public Stack<object> BuildKeys { get; private set; }
    }
}
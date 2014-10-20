// File: IBuildTrackingPolicy.cs
// Project Name: ILogInject.Unity
// Project Home: https://github.com/trondr/ILogInject/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/ILogInject/blob/master/License.md
// Credits: http://blog.baltrinic.com/software-development/dotnet/log4net-integration-with-unity-ioc-container
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;

namespace Wise.Framework.DependencyInjection.Unity.Extensions.BuildTrackingExtension
{
    public interface IBuildTrackingPolicy : IBuilderPolicy
    {
        Stack<object> BuildKeys { get; }
    }
}
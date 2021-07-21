using System;
using TailoredApps.Rayonnant.Interface.DependencyInjection;

namespace TailoredApps.Rayonnant.DependencyInjection
{
    internal class UnityContainerRegistration : IContainerRegistration
    {
        public Type RegisteredType { get; set; }

        public Type MappedToType { get; set; }

        public string Name { get; set; }

        public Type LifetimeManagerType { get; set; }
    }
}
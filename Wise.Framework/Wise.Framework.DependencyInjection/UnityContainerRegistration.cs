using System;
using Wise.Framework.Interface.DependencyInjection;

namespace Wise.Framework.DependencyInjection
{
    internal class UnityContainerRegistration : IContainerRegistration
    {
        public Type RegisteredType { get; set; }

        public Type MappedToType { get; set; }

        public string Name { get; set; }

        public Type LifetimeManagerType { get; set; }
    }
}
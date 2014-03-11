using System;

namespace Wise.Framework.Interface.DependencyInjection
{
    public interface IContainerRegistration
    {
        Type RegisteredType { get; }
        Type MappedToType { get; }

        string Name {get; }
        Type LifetimeManagerType { get; }
    }
}

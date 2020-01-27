using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Lifetime;

namespace Wise.Framework.Interface.DI
{
    public static class Extensions
    {
        public static IUnityContainer RegisterTypeIfMissing<S, D>(this IUnityContainer container, ITypeLifetimeManager lifetimeManager) where D : S
        {
            if (!container.IsRegistered<D>())
            {
                container.RegisterType<S, D>(lifetimeManager);
            }
            return container;
        }
    }
}

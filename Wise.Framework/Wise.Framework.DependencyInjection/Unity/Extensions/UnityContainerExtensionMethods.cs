using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder;
using Unity;
using Unity.Registration;
namespace Wise.Framework.DependencyInjection.Unity.Extensions
{
    public static class UnityContainerExtensionMethods
    {
        public static Wise.Framework.Interface.DependencyInjection.IContainerRegistration ToContainerRegistration(this IContainerRegistration cr)
        {
            return new UnityContainerRegistration
            {
            };
        }

        public static IEnumerable<Wise.Framework.Interface.DependencyInjection.IContainerRegistration> ToContainerRegistration(
            this IEnumerable<IContainerRegistration> cr)
        {
            IList<Wise.Framework.Interface.DependencyInjection.IContainerRegistration> containerRegistrations = new List<Wise.Framework.Interface.DependencyInjection.IContainerRegistration>();
            foreach (var registration  in cr)
            {
                containerRegistrations.Add(ToContainerRegistration(registration));
            }
            return containerRegistrations;
        }
    }
}
using System.Collections.Generic;
using Unity;
using Unity.Registration;
namespace TailoredApps.Rayonnant.DependencyInjection.Unity.Extensions
{
    public static class UnityContainerExtensionMethods
    {
        public static TailoredApps.Rayonnant.Interface.DependencyInjection.IContainerRegistration ToContainerRegistration(this IContainerRegistration cr)
        {
            return new UnityContainerRegistration
            {
            };
        }

        public static IEnumerable<TailoredApps.Rayonnant.Interface.DependencyInjection.IContainerRegistration> ToContainerRegistration(
            this IEnumerable<IContainerRegistration> cr)
        {
            IList<TailoredApps.Rayonnant.Interface.DependencyInjection.IContainerRegistration> containerRegistrations = new List<TailoredApps.Rayonnant.Interface.DependencyInjection.IContainerRegistration>();
            foreach (var registration  in cr)
            {
                containerRegistrations.Add(ToContainerRegistration(registration));
            }
            return containerRegistrations;
        }
    }
}
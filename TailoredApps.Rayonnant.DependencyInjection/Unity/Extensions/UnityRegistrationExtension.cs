using System;
using System.Linq;
using Unity;
using Unity.Builder;
using Unity.Extension;

namespace TailoredApps.Rayonnant.DependencyInjection.Unity.Extensions
{
    /// <summary>
    ///     Extension for checking registration of types in container
    /// </summary>
    /// todo
    public class UnityRegistrationExtension : UnityContainerExtension
    {
        /// <summary>
        ///     check the status of registration type in container
        /// </summary>
        /// <param name="container">container</param>
        /// <param name="type">type to check</param>
        /// <returns></returns>
        public static bool IsTypeRegistered(IUnityContainer container, Type type)
        {
            var extension = container.Configure<UnityRegistrationExtension>();
            if (extension == null)
            {
                return false;
            }
            return extension.Context.Container.Registrations.Any(x=>x.RegisteredType ==  type);
        }

        /// <summary>
        ///     Method used to initialization
        /// </summary>
        protected override void Initialize()
        {
        }
    }
}
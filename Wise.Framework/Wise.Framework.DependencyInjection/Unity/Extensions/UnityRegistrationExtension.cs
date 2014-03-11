using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace Wise.Framework.DependencyInjection.Unity.Extensions
{
    /// <summary>
    /// Extension for checking registration of types in container
    /// </summary>
    public class UnityRegistrationExtension :UnityContainerExtension
    {
        /// <summary>
        /// check the status of registration type in container
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
            var policy = extension.Context.Policies.Get<IBuildKeyMappingPolicy>(new NamedTypeBuildKey(type));
            return policy != null;
        }
        /// <summary>
        /// Method used to initialization
        /// </summary>
        protected override void Initialize()
        {
            
        }
    }
}

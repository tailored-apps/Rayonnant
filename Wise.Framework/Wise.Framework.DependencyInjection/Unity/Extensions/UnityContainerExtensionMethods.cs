using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Wise.Framework.Interface.DependencyInjection;

namespace Wise.Framework.DependencyInjection.Unity.Extensions
{
    public static class UnityContainerExtensionMethods
    {
        public static IContainerRegistration ToContainerRegistration(this ContainerRegistration cr)
        {
            return new UnityContainerRegistration()
            {
                LifetimeManagerType = cr.LifetimeManagerType,
                MappedToType = cr.MappedToType,
                Name = cr.Name,
                RegisteredType = cr.RegisteredType
            };
        }
        
        public static IEnumerable<IContainerRegistration> ToContainerRegistration(this IEnumerable<ContainerRegistration> cr)
        {
            IList<IContainerRegistration> containerRegistrations = new List<IContainerRegistration>();
            cr.ForEach(x=> containerRegistrations.Add(ToContainerRegistration(x)));
            return containerRegistrations;
        }
    }
}

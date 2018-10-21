using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Wise.Framework.DependencyInjection.Unity.Extensions;
using Wise.Framework.DependencyInjection.Unity.Extensions.BuildTrackingExtension;
using Wise.Framework.DependencyInjection.Unity.Extensions.CommonLoggingExtension;
using Wise.Framework.Interface.DependencyInjection;
using Wise.Framework.Interface.DependencyInjection.Enum;
using Microsoft.Practices.ServiceLocation;

namespace Wise.Framework.DependencyInjection.Unity
{
    /// <summary>
    ///     Adapter class for unity DI framework. <see cref="IContainer" />
    /// </summary>
    public class UnityContainerAdapter : IContainer
    {
        private IUnityContainer unityContainer;

        /// <summary>
        ///     default ctor
        /// </summary>
        public UnityContainerAdapter()
            : this(new UnityContainer())
        {
        }

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="unityContainer">unity container to adapt</param>
        public UnityContainerAdapter(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
            this.unityContainer.RegisterInstance(unityContainer, new ExternallyControlledLifetimeManager())
                .AddNewExtension<UnityRegistrationExtension>()
                .AddNewExtension<BuildTracking>()
                .AddNewExtension<CommonLoggingLogCreationExtension>()
                .RegisterInstance<IContainer>(this, new ExternallyControlledLifetimeManager());
        }

        /// <summary>
        ///     <see cref="IContainer.RegisterTypeIfMissing{T1,T2}" />
        /// </summary>
        public void RegisterTypeIfMissing<T1, T2>(LifetimeScope lifetimeScope) where T2 : T1
        {
            if (!IsTypeRegistered<T1>())
            {
                RegisterTypeWithoutCheck<T1, T2>(lifetimeScope);
            }
        }

        /// <summary>
        ///     <see cref="IContainer.IsTypeRegistered{T1}" />
        /// </summary>
        public bool IsTypeRegistered<T1>()
        {
            return UnityRegistrationExtension.IsTypeRegistered(unityContainer, typeof (T1));
        }

        public bool IsTypeRegistered(Type type)
        {
            return UnityRegistrationExtension.IsTypeRegistered(unityContainer, type);
        }

        /// <summary>
        ///     <see cref="IContainer.Resolve{T1}" />
        /// </summary>
        public T1 Resolve<T1>()
        {
            return unityContainer.Resolve<T1>();
        }

        /// <summary>
        ///     <see cref="IContainer.Resolve{T1}" />
        /// </summary>
        public T1 Resolve<T1>(string name)
        {
            return unityContainer.Resolve<T1>(name);
        }

        public object Resolve(Type d)
        {
            return unityContainer.Resolve(d);
        }

        public object Resolve(Type d, string name)
        {
            return unityContainer.Resolve(d, name);
        }

        /// <summary>
        ///     <see cref="IContainer.RegisterInstance{T1}" />
        /// </summary>
        public void RegisterInstance<T1>(T1 instance)
        {
            unityContainer.RegisterInstance(instance, new ContainerControlledLifetimeManager());
        }

        /// <summary>
        ///     <see cref="IContainer.RegisterInstance{T1}" />
        /// </summary>
        public void RegisterInstance<T1>(T1 instance, string name)
        {
            unityContainer.RegisterInstance(name, instance);
        }


        public IContainer AddExtension(object extension)
        {
            var ext = extension as UnityContainerExtension;
            if (ext != null)
            {
                unityContainer = unityContainer.AddExtension(ext);
                return this;
            }
            throw new ArgumentException("extension is not type of UnityContainerExtension");
        }


        public IContainer CreateChildContainer()
        {
            return new UnityContainerAdapter(unityContainer.CreateChildContainer());
        }


        public IContainer RemoveAllExtensions()
        {
            unityContainer.RemoveAllExtensions();
            return this;
        }


        public void Teardown(object o)
        {
            unityContainer.Teardown(o);
        }

        public void Dispose()
        {
            unityContainer.RemoveAllExtensions();
            unityContainer.Dispose();
        }

        public object Configure(Type configurationInterface)
        {
            return unityContainer.Configure(configurationInterface);
        }

        public IContainer RegisterInstance(Type t, string name, object instance, object lifetime)
        {
            var manager = lifetime as LifetimeManager;
            unityContainer.RegisterInstance(t, name, instance, manager);
            return this;
        }

        public IContainer RegisterType(Type from, Type to, string name, object lifetimeManager,
            object[] injectionMembers)
        {
            var members = injectionMembers as InjectionMember[];
            var manager = lifetimeManager as LifetimeManager;
            unityContainer.RegisterType(from, to, name, manager, members);
            return this;
        }

        public IEnumerable<IContainerRegistration> Registrations
        {
            get { return unityContainer.Registrations.ToContainerRegistration(); }
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return unityContainer.ResolveAll<T>();
        }

        public IEnumerable<object> ResolveAll(Type t)
        {
            return unityContainer.ResolveAll(t);
        }


        public void RegisterType<T1, T2>(LifetimeScope lifetimeScope) where T2 : T1
        {
            RegisterTypeWithoutCheck<T1, T2>(lifetimeScope);
        }


        public void RegisterType<T1, T2>(LifetimeScope lifetimeScope, string name) where T2 : T1
        {
            RegisterTypeWithoutCheck<T1, T2>(lifetimeScope, name);
        }


        public void RegisterType(Type from, Type to, string name)
        {
            unityContainer.RegisterType(from, to, name);
        }

        /// <summary>
        ///     method registers type in container
        /// </summary>
        /// <typeparam name="T1">in most cases interface</typeparam>
        /// <typeparam name="T2">in most cases class implementation for {T1}</typeparam>
        /// <param name="lifetimeScope">lifetime scope for type</param>
        private void RegisterTypeWithoutCheck<T1, T2>(LifetimeScope lifetimeScope, string name = null) where T2 : T1
        {
            if (lifetimeScope == LifetimeScope.Singleton)
            {
                unityContainer.RegisterType<T1, T2>(name, new ContainerControlledLifetimeManager());
            }
            else
            {
                unityContainer.RegisterType<T1, T2>(name);
            }
        }

    }
}
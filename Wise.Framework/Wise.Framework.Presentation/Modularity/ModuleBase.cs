using Microsoft.Practices.Prism.Modularity;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.Modularity;

namespace Wise.Framework.Presentation.Modularity
{
    /// <summary>
    /// Abstract Base Module class <see cref="IModule"/>
    /// </summary>
    public abstract class ModuleBase<T> : IModule
    {
        /// <summary>
        /// The Resource Manager
        /// </summary>
        protected IResourceManager ResourceManager { get; private set; }

        private readonly IMessanger messanger;
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="resourceManager">the resource manager class used to merging resources.</param>
        protected ModuleBase(IResourceManager resourceManager, IMessanger messanger)
        {
            this.ResourceManager = resourceManager;
            this.messanger = messanger;
            PublishSystemMessage(string.Format("Module {0} has been created", typeof (T)));
        }

        private void PublishSystemMessage(string message)
        {
            //messanger.Publish<SystemNotyficationMessage>(new SystemNotyficationMessage() { Message = message });
        }
        /// <summary>
        /// <see cref="IModule.Initialize"/>
        /// </summary>
        public void Initialize()
        {
            PublishSystemMessage(string.Format("Going to init {0}", typeof(T)));
            PublishSystemMessage(string.Format("Going to regiser resources for {0}", typeof(T)));
            this.RegisterResources();
            PublishSystemMessage(string.Format("Going to regiser services for {0}", typeof(T)));
            this.RegisterServices();
            PublishSystemMessage(string.Format("Going to regiser regions for {0}", typeof(T)));
            this.RegisterViewRegions();
            PublishSystemMessage(string.Format("Going to start services for {0}", typeof(T)));
            this.StartServices();
            PublishSystemMessage(string.Format("Going to run module: {0}", typeof(T)));
            this.Run();
        }

        /// <summary>
        /// After all we should run what we need
        /// </summary>
        protected virtual void Run()
        {
        }

        /// <summary>
        /// If some services need be started , lets start them
        /// </summary>
        protected virtual void StartServices()
        {
        }
        /// <summary>
        /// If some regions need to be registered with view lets register them
        /// </summary>
        protected virtual void RegisterViewRegions()
        {
        }
        /// <summary>
        /// If some services need to be register lets register them
        /// </summary>
        protected virtual void RegisterServices()
        {
        }

        /// <summary>
        /// If you have to register resource do it in this method.
        /// </summary>
        protected virtual void RegisterResources()
        {

        }
    }
}

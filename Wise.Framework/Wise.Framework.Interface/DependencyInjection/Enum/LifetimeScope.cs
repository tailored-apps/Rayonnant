namespace Wise.Framework.Interface.DependencyInjection.Enum
{
    /// <summary>
    ///     Enumeration used for describing lifetime cycle of object registered in Dependency Injection container.
    /// </summary>
    public enum LifetimeScope
    {
        /// <summary>
        ///     The Singleton object
        /// </summary>
        Singleton,

        /// <summary>
        ///     Factory , which will provide new instance of object where required.
        /// </summary>
        Factory
    }
}
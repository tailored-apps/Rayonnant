namespace Wise.Framework.Interface.Bootstrapping
{
    /// <summary>
    /// Interface for bootstrapping class runner, which is responsible for running bootstrap in right way.
    /// </summary>
    public  interface IBootstrapperRunner
    {
        /// <summary>
        /// Method for running bootstrap class which contains all definitions of classes required to initialize.
        /// </summary>
        /// <param name="bootstrapper">bootstrapping class object</param>
        void Run(IBootstrapper bootstrapper);
    }
}

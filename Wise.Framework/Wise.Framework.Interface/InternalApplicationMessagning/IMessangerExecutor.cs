using System.Collections.Generic;

namespace Wise.Framework.Interface.InternalApplicationMessagning
{
    /// <summary>
    ///     Messanger executor
    /// </summary>
    public interface IMessangerExecutor
    {
        /// <summary>
        ///     Fires subscribtion methods on messanger
        /// </summary>
        /// <param name="subscriptions">collection of <see cref="IMessangerSubscription" /> which will be executed after publish</param>
        /// <param name="obj">subscribtion method argument</param>
        /// <typeparam name="T">type of executing method argument</typeparam>
        void Execute<T>(IEnumerable<IMessangerSubscription> subscriptions, object obj);
    }
}
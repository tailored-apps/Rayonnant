using System;
using Wise.Framework.Interface.InternalApplicationMessagning.Enum;

namespace Wise.Framework.Interface.InternalApplicationMessagning
{
    /// <summary>
    ///     Interface used for presenting subscribtion methods attached to <see cref="IMessanger">Messanger</see>
    ///     Its used to setting on subscribtion method execution thread.
    /// </summary>
    public interface IMessangerSubscription : IDisposable
    {
        /// <summary>
        ///     Key its used to sending  messages to specyfic subscriber with same key,
        ///     when its set then event will be not called on broadcast.
        /// </summary>
        string Key { get; }

        /// <summary>
        ///     Gets information about processing thread.
        /// </summary>
        MessageProcessingThread MessageProcessingThread { get; }

        bool HasAction { get; }

        /// <summary>
        ///     Event related to this subscription
        /// </summary>
        event Action<object> MessageArrivalAction;

        /// <summary>
        ///     Method used for changing processing thread.
        /// </summary>
        /// <param name="messageProcessingThread">determines on wchich thread should be executed event</param>
        /// <returns>self</returns>
        IMessangerSubscription ExecuteOn(MessageProcessingThread messageProcessingThread);

        /// <summary>
        ///     Method used by <see cref="IMessangerExecutor" /> for fire'ing events attached to subscription
        /// </summary>
        /// <param name="obj">event method argument</param>
        void Execute(object obj);
    }
}
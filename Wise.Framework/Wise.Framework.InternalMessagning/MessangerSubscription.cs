using System;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.InternalApplicationMessagning.Enum;

namespace Wise.Framework.InternalMessagning
{
    /// <summary>
    ///     This is default implementation of <see cref="IMessangerSubscription" />. This class is responsible for creating
    ///     messanger subscription object which contains references to delegates and types on which subscription
    ///     delegate(event) will be fired.
    /// </summary>
    public class MessangerSubscription : IMessangerSubscription
    {
        private readonly string key;
        private Action<object> messageArrivalAction;
        private MessageProcessingThread processingThread;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="key">its used to specyfing key method</param>
        /// <param name="action">subscription on specyfic key</param>
        public MessangerSubscription(string key, Action<object> action)
        {
            MessageArrivalAction += action;
            this.key = key;
            processingThread = MessageProcessingThread.MessagePublishingThread;
        }

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="action">broadcasting subscription method</param>
        public MessangerSubscription(Action<object> action)
        {
            MessageArrivalAction += action;
        }

        public bool HasAction
        {
            get { return (messageArrivalAction != null); }
        }

        public MessageProcessingThread MessageProcessingThread
        {
            get { return processingThread; }
        }


        public IMessangerSubscription ExecuteOn(MessageProcessingThread messageProcessingThread)
        {
            processingThread = messageProcessingThread;
            return this;
        }

        public void Execute(object obj)
        {
            if (HasAction)
                messageArrivalAction(obj);
        }


        public string Key
        {
            get { return key; }
        }

        /// <summary>
        /// </summary>
        public event Action<object> MessageArrivalAction
        {
            add { messageArrivalAction += value; }
            // ReSharper disable once DelegateSubtraction
            remove { messageArrivalAction -= value; }
        }

        public void Dispose()
        {
            messageArrivalAction = null;
        }
    }
}
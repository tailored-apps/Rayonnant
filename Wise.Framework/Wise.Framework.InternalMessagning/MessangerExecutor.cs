using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Threading;
using Wise.Framework.Interface.InternalApplicationMessagning;
using Wise.Framework.Interface.InternalApplicationMessagning.Enum;

namespace Wise.Framework.InternalMessagning
{
    /// <summary>
    /// <see cref="IMessangerSubscription "/>
    /// This is a default implementation of interface.
    /// </summary>
    public class MessangerExecutor : IMessangerExecutor
    {
        /// <summary>
        /// Default implementation of <see cref="IMessangerExecutor.Execute{T}"/>
        /// method performs executing subscribed methods after publishing object to te messanger.
        /// Method is fired for colection of <see cref="IEnumerable{IMessangerSubscription} "/>
        /// and exeuctes all subscirptions separatlely in loop.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="obj"/> </typeparam>
        /// <param name="subscriptions">Collection of subscription methods </param>
        /// <param name="obj">argument passed to subscription methods</param>
        public void Execute<T>(IEnumerable<IMessangerSubscription> subscriptions, object obj)
        {
            lock (this)
            {
                foreach (IMessangerSubscription item in subscriptions)
                {

                        switch (item.MessageProcessingThread)
                        {
                            case MessageProcessingThread.Dispatcher:
                                ExecuteOnDispatcher(item, obj);
                                break;
                            case MessageProcessingThread.MessagePublishingThread:
                                ExecuteOnCallingTrherad(item, obj);
                                break;
                            case MessageProcessingThread.NewTask:
                                ExecuteOnNewThread(item, obj);
                                break;
                        }
                
                }
            }
        }

        /// <summary>
        /// Method used for invoking subsciption method on UI Dispatcher thread
        /// </summary>
        /// <param name="subscriptions">method subscription</param>
        /// <param name="obj">argument</param>
        private void ExecuteOnDispatcher(IMessangerSubscription subscriptions, object obj)
        {
            Dispatcher.CurrentDispatcher.Invoke(new Action(() => subscriptions.Execute(obj)));
        }

        /// <summary>
        /// Method used for invoking subscription method on Caller thread.
        /// </summary>
        /// <param name="subscriptions">method subscription</param>
        /// <param name="obj">argument</param>
        private void ExecuteOnCallingTrherad(IMessangerSubscription subscriptions, object obj)
        {
            subscriptions.Execute(obj);
        }

        /// <summary>
        /// Method used for invoking subsciption method on new task (thread)
        /// </summary>
        /// <param name="subscriptions">method subscription</param>
        /// <param name="obj">argument</param>
        private void ExecuteOnNewThread(IMessangerSubscription subscriptions, object obj)
        {
            Task.Factory.StartNew(() => subscriptions.Execute(obj));
        }
    }
}

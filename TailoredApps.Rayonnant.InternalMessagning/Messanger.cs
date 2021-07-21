using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TailoredApps.Rayonnant.Interface.InternalApplicationMessagning;

namespace TailoredApps.Rayonnant.InternalMessagning
{
    public class Messanger : IMessanger
    {
        private readonly IMessangerExecutor executor;

        private readonly IDictionary<Type, ICollection<IMessangerSubscription>> subscriptionsDictionary =
            new ConcurrentDictionary<Type, ICollection<IMessangerSubscription>>();

        public Messanger(IMessangerExecutor executor)
        {
            this.executor = executor;
        }

        public void Publish<TMessage>(TMessage obj)
        {
            ICollection<IMessangerSubscription> messangerSubscription;
            if (subscriptionsDictionary.TryGetValue(typeof (TMessage), out messangerSubscription))
            {
                messangerSubscription = messangerSubscription.Where(x => x.HasAction).ToList();
                subscriptionsDictionary[typeof (TMessage)] = messangerSubscription;
                executor.Execute<TMessage>(messangerSubscription, obj);
            }
        }

        public void Publish<TMessage>(string key, TMessage obj)
        {
            ICollection<IMessangerSubscription> messangerSubscription;
            if (subscriptionsDictionary.TryGetValue(typeof (TMessage), out messangerSubscription))
            {
                messangerSubscription = messangerSubscription.Where(x => x.HasAction).ToList();
                subscriptionsDictionary[typeof (TMessage)] = messangerSubscription;

                IEnumerable<IMessangerSubscription> elements =
                    messangerSubscription.Where(x => string.Equals(x.Key, key));
                executor.Execute<TMessage>(elements, obj);
            }
        }

        public IMessangerSubscription Subscribe<TMessage>(Action<TMessage> onMessageArrived) where TMessage : class
        {
            IMessangerSubscription subscription = new MessangerSubscription(ConvertToActionObject(onMessageArrived));
            CreateSubscription<TMessage>(subscription);
            return subscription;
        }

        public IMessangerSubscription Subscribe<TMessage>(string key, Action<TMessage> onMessageArrived)
        {
            IMessangerSubscription subscription = new MessangerSubscription(key, ConvertToActionObject(onMessageArrived));
            CreateSubscription<TMessage>(subscription);
            return subscription;
        }

        private void CreateSubscription<TMessage>(IMessangerSubscription subscription)
        {
            ICollection<IMessangerSubscription> messangerSubscription;
            if (subscriptionsDictionary.TryGetValue(typeof (TMessage), out messangerSubscription))
            {
                messangerSubscription.Add(subscription);
                subscriptionsDictionary[typeof (TMessage)] = messangerSubscription;
            }
            else
            {
                messangerSubscription = new Collection<IMessangerSubscription>();
                messangerSubscription.Add(subscription);
                subscriptionsDictionary.Add(typeof (TMessage), messangerSubscription);
            }
        }

        private Action<object> ConvertToActionObject<TMessage>(Action<TMessage> myActionT)
        {
            if (myActionT == null) return null;
            return o => myActionT((TMessage) o);
        }
    }
}
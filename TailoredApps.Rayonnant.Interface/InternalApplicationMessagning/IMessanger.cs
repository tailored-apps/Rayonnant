using System;

namespace TailoredApps.Rayonnant.Interface.InternalApplicationMessagning
{
    public interface IMessanger
    {
        /// <summary>
        ///     Method for publishing object through messenger system
        /// </summary>
        /// <typeparam name="T1">Type of published object</typeparam>
        /// <param name="obj">object to publish</param>
        void Publish<T1>(T1 obj);

        /// <summary>
        ///     Method for publishing object through messenger system with specific key.
        ///     which object will be visible only for subscribers registered with same key.
        /// </summary>
        /// <typeparam name="T1">Type of published object</typeparam>
        /// <param name="key">communication key</param>
        /// <param name="obj">object to publish</param>
        void Publish<T1>(string key, T1 obj);

        /// <summary>
        ///     Method for registering handling method of message
        /// </summary>
        /// <typeparam name="T">Type for subscribing type</typeparam>
        /// <param name="onMessageArrived"></param>
        /// <returns>messenger subscription object</returns>
        IMessangerSubscription Subscribe<T>(Action<T> onMessageArrived) where T : class;

        /// <summary>
        ///     Method for registering handling method of message with specific key
        /// </summary>
        /// <typeparam name="T">type of message</typeparam>
        /// <param name="key">unique key</param>
        /// <param name="onMessageArrived">method invoked after message arrival</param>
        /// <returns>subscription object</returns>
        IMessangerSubscription Subscribe<T>(string key, Action<T> onMessageArrived);
    }
}
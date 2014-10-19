namespace Wise.Framework.Interface.InternalApplicationMessagning.Enum
{
    /// <summary>
    ///     Presents thread which will take care on arrived message.
    /// </summary>
    public enum MessageProcessingThread
    {
        /// <summary>
        ///     UI Thread
        /// </summary>
        Dispatcher,

        /// <summary>
        ///     Thread which push message
        /// </summary>
        MessagePublishingThread,

        /// <summary>
        ///     creates new thread for each arrived message
        /// </summary>
        NewTask
    }
}
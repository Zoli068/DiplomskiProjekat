using Common.Utilities;

namespace Common.Message
{
    /// <summary>
    ///  Interface for classes that will handles the creation, procession and serialization/deserialization of messages
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMessageProcesser<T>
    {
        /// <summary>
        /// That method used to be able to send a new message
        /// </summary>
        /// <param name="code">Indicates the type of the message that we want to sent</param>
        /// <param name="messageDTO">MessageDTO from the UI layer, and the values we can in our way to use it</param>
        void InitateMessage(T code, IMessageDTO messageDTO);
    }
}

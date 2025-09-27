namespace Common.Message
{
    /// <summary>
    /// Describes the methods of a MessageHandler
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// Creates an instance of the message object from byte[]
        /// </summary>
        void ProcessBytes(byte[] data);

        /// <summary>
        /// Converts the IMessage object to a byte[]
        /// </summary>
        void SendMessage(IMessageData messageData);
    }
}

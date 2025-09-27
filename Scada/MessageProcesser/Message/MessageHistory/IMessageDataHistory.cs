using Common.Message;

namespace Master.Message
{
    /// <summary>
    /// Interface that describes the methods for a MessageData History class
    /// </summary>
    public interface IMessageDataHistory
    {
        /// <summary>
        /// Function which will add messageData to the history
        /// </summary>
        /// <param name="messageData">The data which will be saved</param>
        /// <param name="messageId">The identificator for the data</param>
        void AddMessageData(IMessageData messageData, ushort messageId);

        /// <summary>
        /// Extracting the messageData from history by identificator
        /// </summary>
        /// <param name="messageId">The unique identifiactor for the messageData</param>
        /// <returns>The messagedata from the history</returns>
        IMessageData GetMessageData(ushort messageId);
    }
}

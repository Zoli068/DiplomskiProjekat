using Common.Message;

namespace Common.Command
{
    /// <summary>
    /// Describes the response message data handler, when the client got a response, then we 
    /// have to pass the Response MessageData, and the original Request MessageData as parameter
    /// </summary>
    public interface IResponseMessageDataHandler
    {
        /// <summary>
        /// Method which will be called when the Client got a response, then we have to 
        /// pass the request MessageData, and the responseMessageData as parameter,than 
        /// the specified Command will be executed
        /// </summary>
        void ProcessMessageData(IMessageData request, IMessageData response);
    }
}

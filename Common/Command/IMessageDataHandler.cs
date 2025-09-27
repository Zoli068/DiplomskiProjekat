using Common.Message;

namespace Common.Command
{
    /// <summary>
    /// Describes the message data handler, when the server got a request, then we 
    /// have to pass as parameter, then the return value is the response message data
    /// </summary>
    public interface IMessageDataHandler
    {
        /// <summary>
        /// Method which will be called when the server got a request, then we have to 
        /// pass as parameter,than the specified Command will be executed then the 
        /// return value is the response message data
        /// </summary>
        IMessageData ProcessMessageData(IMessageData data);
    }
}

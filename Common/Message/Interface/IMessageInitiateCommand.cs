using Common.Utilities;

namespace Common.Message
{
    /// <summary>
    /// Interface that describes a class which will initiate new messages to be sent
    /// </summary>
    public interface IMessageInitiateCommand<T, T2> where T : IMessageDTO 
    {
        /// <summary>
        /// Method that have to be called to initate a new message to be sent
        /// </summary>
        /// <param name="messageDTO">The DTO message object, which we got from the UI layer</param>
        /// <returns>Returns the part of the message that will be sent</returns>
        T2 InitiateMessage(T messageDTO);
    }
}

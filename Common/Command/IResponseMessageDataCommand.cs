namespace Common.Command
{
    /// <summary>
    /// Interfaces which describes the method that need to be 
    /// implemented inside a ResponseCommand for MessageDatas
    /// </summary>
    public interface IResponseMessageDataCommand<T>
    {
        /// <summary>
        /// The method which have to be called to process a messageData response
        /// </summary>
        /// <param name="request">The original request messageData</param>
        /// <param name="response">The response messageData</param>
        void Execute(T request, T response);
    }
}

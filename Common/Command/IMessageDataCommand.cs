namespace Common.Command
{
    /// <summary>
    /// Interfaces which describes the method that need to be 
    /// implemented inside a messageDataCommand 
    /// </summary>
    /// <typeparam name="T">The MessageData clas</typeparam>
    public interface IMessageDataCommand<T>
    {
        /// <summary>
        /// The method which have to be called to process a messageData
        /// </summary>
        /// <param name="data">The MessageData class</param>
        /// <returns></returns>
        T Execute(T data);
    }
}

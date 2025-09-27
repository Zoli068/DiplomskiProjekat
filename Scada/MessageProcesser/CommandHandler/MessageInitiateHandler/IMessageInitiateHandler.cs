using Common.Utilities;

namespace Master.CommandHandler.MessageInitiateHandler
{
    /// <summary>
    /// Method that has to be implemented for a InitiateHandler
    /// </summary>
    /// <typeparam name="T">Types inside the message enumeration</typeparam>
    public interface IMessageInitiateHandler<T>
    {
        void InitiateMessage(T code, IMessageDTO modbusMessageDTO);
    }
}

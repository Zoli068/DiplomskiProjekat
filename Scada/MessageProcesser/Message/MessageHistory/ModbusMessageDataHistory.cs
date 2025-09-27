using Common.Message;
using System.Collections.Generic;

namespace Master.Message.MessageHistory
{
    /// <summary>
    /// Class for saving the modbus messages, and retriving them by identificator
    /// </summary>
    public class ModbusMessageDataHistory : IMessageDataHistory
    {
        private Dictionary<ushort, IMessageData> messageDataHistory = new Dictionary<ushort, IMessageData>();

        public void AddMessageData(IMessageData messageData, ushort messageId)
        {
            if (!messageDataHistory.ContainsKey(messageId))
            {
                messageDataHistory[messageId] = messageData;
            }
            else
            {
                throw new MessageDataIdentificatorCollisionException();
            }
        }

        public IMessageData GetMessageData(ushort messageId)
        {
            IMessageData messageData;
            if (messageDataHistory.TryGetValue(messageId, out messageData))
            {
                messageDataHistory.Remove(messageId);
            }
            else
            {
                throw new MessageDataNotFoundException();
            }

            return messageData;
        }
    }
}

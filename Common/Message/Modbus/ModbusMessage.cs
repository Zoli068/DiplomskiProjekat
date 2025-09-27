using System.Collections.Generic;

namespace Common.Message
{
    /// <summary>
    /// The implementation of a Modbus Message
    /// </summary>
    public class ModbusMessage : IMessage
    {
        private IMessageData messageData;
        private IMessageHeader messageHeader;

        public ModbusMessage()
        {
            messageHeader = new TCPModbusHeader();
            messageData = new ModbusPDU();
        }

        public ModbusMessage(IMessageData messageData, IMessageHeader messageHeader)
        {
            this.messageData = messageData;
            this.messageHeader = messageHeader;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            messageHeader.Deserialize(data, ref startIndex);
            messageData.Deserialize(data, ref startIndex);
        }

        public byte[] Serialize()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(messageHeader.Serialize());
            bytes.AddRange(messageData.Serialize());

            return bytes.ToArray();
        }

        public IMessageData MessageData
        {
            get
            {
                return messageData;
            }
            set
            {
                messageData = value;
            }
        }

        public IMessageHeader MessageHeader
        {
            get
            {
                return messageHeader;
            }
            set
            {
                messageHeader = value;
            }
        }


    }
}

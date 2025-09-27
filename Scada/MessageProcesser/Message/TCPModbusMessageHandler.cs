using Common.Command;
using Common.Message;
using Common.Serialization;
using Master.Message.MessageHistory;
using System;
using System.Collections.Generic;

namespace Master.Communication
{
    /// <summary>
    /// The class which converts the bytes to ModbusMessages,  and ModbusMessages back to bytes and send for the communication layer
    /// </summary>
    public class TCPModbusMessageHandler : IMessageHandler
    {
        private Action<byte[]> sendBytes;
        private ushort transactionIdentificator = 0;
        private ModbusMessageDataHistory messageDataHistory;
        private IResponseMessageDataHandler responseMessageDataHandler;

        public TCPModbusMessageHandler(Action<byte[]> sendBytes, IResponseMessageDataHandler responseMessageDataHandler)
        {
            this.sendBytes = sendBytes;
            messageDataHistory = new ModbusMessageDataHistory();
            this.responseMessageDataHandler = responseMessageDataHandler;
        }

        /// <summary>
        /// Creates a modbusMessage by the recived bytes
        /// </summary>
        /// <param name="data">The recived bytes</param>
        public void ProcessBytes(byte[] data)
        {
            ModbusMessage modbusMessage = null;

            try
            {
                modbusMessage = Serialization.CreateMessageObject<ModbusMessage>(data);

                if (data.Length - 7 == (modbusMessage.MessageHeader as TCPModbusHeader).Length)
                {
                    IMessageData response = messageDataHistory.GetMessageData(((TCPModbusHeader)modbusMessage.MessageHeader).TransactionID);
                    responseMessageDataHandler.ProcessMessageData(response, modbusMessage.MessageData);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Sending messageData to the server also creates a tcpHeader for the data
        /// </summary>
        /// <param name="messageData">The messageData which we want to send</param>
        public void SendMessage(IMessageData messageData)
        {
            try
            {
                List<byte> bytes = new List<byte>();

                byte[] messageDataSerialized = messageData.Serialize();
                TCPModbusHeader header = new TCPModbusHeader(transactionIdentificator, 0, (ushort)messageDataSerialized.Length, 255);

                bytes.AddRange(header.Serialize());
                bytes.AddRange(messageDataSerialized);

                messageDataHistory.AddMessageData(messageData, transactionIdentificator);
                sendBytes(bytes.ToArray());
                transactionIdentificator++;
            }
            catch { }
        }

    }
}
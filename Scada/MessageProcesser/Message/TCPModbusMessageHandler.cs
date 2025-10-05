using Common.Command;
using Common.Message;
using Common.Serialization;
using Common.Utilities;
using Master.CommandHandler;
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
        private ByteBuffer buffer;
        private Action<byte[]> sendBytes;
        private ushort transactionIdentificator = 0;
        private ModbusMessageDataHistory messageDataHistory;
        private IResponseHandler responsePDUHandler;

        public TCPModbusMessageHandler(Action<byte[]> sendBytes, IResponseHandler responsePDUHandler)
        {
            this.sendBytes = sendBytes;
            messageDataHistory = new ModbusMessageDataHistory();
            this.responsePDUHandler = responsePDUHandler;
            buffer= new ByteBuffer();
        }

        /// <summary>
        /// Creates a modbusMessage by the recived bytes
        /// </summary>
        /// <param name="data">The recived bytes</param>
        public void ProcessBytes(byte[] data)
        {
            buffer.Append(data);

            while(buffer.Length >= 7)
            {
                byte[] header = buffer.GetValues(0, 7);
            
                try
                {                
                    TCPModbusHeader TCPModbusHeader = Serialization.CreateMessageObject<TCPModbusHeader>(header);

                    if (buffer.Length - 7 >= TCPModbusHeader.Length)
                    {
                        byte[] message=buffer.GetValues(7, TCPModbusHeader.Length);

                        ModbusPDU modbusPDU= Serialization.CreateMessageObject<ModbusPDU>(message);

                        IMessageData response = messageDataHistory.GetMessageData(TCPModbusHeader.TransactionID);
                        responsePDUHandler.ProcessMessageData(response, modbusPDU);

                        buffer.RemoveBytes(0,TCPModbusHeader.Length + 7);
                    }
                }
                catch (Exception)
                {
                    return;
                }
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
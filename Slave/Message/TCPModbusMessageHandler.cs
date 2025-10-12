using Common.Command;
using Common.Message;
using Common.Serialization;
using Common.Utilities;
using System;
using System.Collections.Generic;

namespace Slave.Communication
{
    /// <summary>
    /// The class which converts the bytes to ModbusMessages,  and ModbusMessages back to bytes and send for the communication layer
    /// </summary>
    public class TCPModbusMessageHandler : IMessageHandler
    {
        private ByteBuffer buffer;
        private Action<byte[]> sendBytes;
        private IMessageDataHandler messageDataHandler;
        private TCPModbusMessage modbusMessage;

        public TCPModbusMessageHandler(Action<byte[]> sendBytes, IMessageDataHandler messageDataHandler)
        {
            this.sendBytes = sendBytes;
            this.messageDataHandler = messageDataHandler;
            buffer = new ByteBuffer();
        }

        public void ProcessBytes(byte[] data)
        {
            IMessageData messageDataToSend;

            buffer.Append(data);

            while (buffer.Length >= 7)
            {
                byte[] header = buffer.GetValues(0, 7);
                byte[] message;
                ModbusPDU modbusPDU=null;
                try
                {
                    TCPModbusHeader TCPModbusHeader = Serialization.CreateMessageObject<TCPModbusHeader>(header);

                    if (buffer.Length - 6 >= TCPModbusHeader.Length)
                    {
                        message = buffer.GetValues(7, TCPModbusHeader.Length -1);

                        modbusPDU = Serialization.CreateMessageObject<ModbusPDU>(message);

                        modbusMessage = new TCPModbusMessage(modbusPDU,TCPModbusHeader);

                        messageDataToSend = messageDataHandler.ProcessMessageData(modbusMessage.MessageData);
                        SendMessage(messageDataToSend);

                        modbusMessage = null;

                        buffer.RemoveBytes(0, TCPModbusHeader.Length + 6);
                    }
                }
                catch (Exception)
                {
                    buffer.RemoveBytes(0, 6 + (modbusMessage.MessageHeader as TCPModbusHeader).Length);

                    if (modbusPDU == null)
                    {
                        return;
                    }

                    byte errorFunctionCode = (byte)(((byte)modbusPDU.FunctionCode) + 0x80);

                    ModbusPDU modbusPDUToSend = new ModbusPDU();

                    modbusPDUToSend.FunctionCode = (FunctionCode)errorFunctionCode;
                    modbusPDUToSend.Data = new ModbusError(errorFunctionCode, ExceptionCode.SlaveDeviceFailure);

                    SendMessage(modbusPDU);
                }
            }
        }

        public void SendMessage(IMessageData messageData)
        {
            try
            {
                List<byte> dataToSend = new List<byte>();

                byte[] messsageDataSerialized = messageData.Serialize();

                TCPModbusHeader header = new TCPModbusHeader();
                header.ProtocolID = 0;
                header.TransactionID = (modbusMessage.MessageHeader as TCPModbusHeader).TransactionID;
                header.UnitID = (modbusMessage.MessageHeader as TCPModbusHeader).UnitID;
                header.Length = (ushort)(messsageDataSerialized.Length + 1);

                dataToSend.AddRange(header.Serialize());
                dataToSend.AddRange(messsageDataSerialized);

                sendBytes(dataToSend.ToArray());
            }
            catch { }
        }
    }
}
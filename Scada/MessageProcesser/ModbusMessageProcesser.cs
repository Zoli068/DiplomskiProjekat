using Common.Command;
using Common.Communication;
using Common.Message;
using Common.Utilities;
using Master.CommandHandler;
using Master.CommandHandler.MessageInitiateHandler;
using Master.Communication;
using System;

namespace Master.MessageProcesser
{
    /// <summary>
    /// Contains all the necessarily objects for processing the modbus messages
    /// </summary>
    public class ModbusMessageProcesser : IMessageProcesser<FunctionCode>
    {
        private IMessageHandler messageHandler;
        private IMessageInitiateHandler<FunctionCode> messageInitiateHandler;
        private IResponseHandler responsePDUHandler;


        public ModbusMessageProcesser(ICommunication communication, Action<FunctionCode, IMessageDTO> responseRecived)
        {
            responsePDUHandler = new ModbusPDUResponseHandler(responseRecived);
            

            messageHandler = new TCPModbusMessageHandler(communication.SendBytes, responsePDUHandler);
            communication.BytesRecived += messageHandler.ProcessBytes;
            messageInitiateHandler = new ModbusMessageInitiateHandler(messageHandler.SendMessage);
        }

        public void InitateMessage(FunctionCode code, IMessageDTO messageDTO)
        {
            messageInitiateHandler.InitiateMessage(code, messageDTO);
        }
    }
}
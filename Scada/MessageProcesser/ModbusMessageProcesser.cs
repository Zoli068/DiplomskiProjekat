using Common.Command;
using Common.Communication;
using Common.Message;
using Common.Utilities;
using Master.CommandHandler;
using Master.CommandHandler.MessageInitiateHandler;
using Master.Communication;

namespace Master.MessageProcesser
{
    /// <summary>
    /// Contains all the necessarily objects for processing the modbus messages
    /// </summary>
    public class ModbusMessageProcesser : IMessageProcesser<FunctionCode>
    {
        private IResponseMessageDataHandler responseMessageDataHandler;
        private IMessageHandler messageHandler;
        private IMessageInitiateHandler<FunctionCode> messageInitiateHandler;

        public ModbusMessageProcesser(ICommunication communication)
        {
            responseMessageDataHandler = new ModbusResponseMessageDataHandler();    //like if we would have a ModbusPointsDic in the slave then here we can pass that
            messageHandler = new TCPModbusMessageHandler(communication.SendBytes, responseMessageDataHandler);
            communication.BytesRecived += messageHandler.ProcessBytes;
            messageInitiateHandler = new ModbusMessageInitiateHandler(messageHandler.SendMessage);
        }

        public void InitateMessage(FunctionCode code, IMessageDTO messageDTO)
        {
            messageInitiateHandler.InitiateMessage(code, messageDTO);
        }

        //if we had a true GUI then here we would have an Event or something like that to be able to update the graphical interface
        //also in that case the modbusPoints Dict, we would pass as a parameter.
    }
}

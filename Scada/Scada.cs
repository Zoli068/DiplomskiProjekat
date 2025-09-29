using Common.Communication;
using Common.Message;
using Common.Utilities;
using Master.MessageProcesser;
using System;

namespace Master
{
    public class Scada
    {
        public Action<FunctionCode, IMessageDTO> initateMessage;

        public event Action<FunctionCode,IMessageDTO> ResponseRecived;
        private ICommunication communication;
        private IMessageProcesser<FunctionCode> messageProcesser;

        public Scada()
        {
            communication = new Communication.Communication();
            messageProcesser = new ModbusMessageProcesser(communication,RaiseResponseRecivedEvent);
            initateMessage = messageProcesser.InitateMessage;
        }

        internal void RaiseResponseRecivedEvent(FunctionCode functionCode, IMessageDTO messageDTO)
        {
            ResponseRecived?.Invoke(functionCode, messageDTO);
        }
    }
}
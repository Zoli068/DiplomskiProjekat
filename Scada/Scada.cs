using Common.Communication;
using Common.Message;
using Master.MessageProcesser;
using Master.UI;

namespace Master
{
    public class Scada
    {
        private ICommunication communication;
        private IMessageProcesser<FunctionCode> messageProcesser;
        private ConsoleUI consoleUI;

        //private UIHandler
        public Scada()
        {
            communication = new Communication.Communication();
            messageProcesser = new ModbusMessageProcesser(communication);
            consoleUI = new ConsoleUI(messageProcesser.InitateMessage);
        }

        public void Start()
        {
            consoleUI.Start();
        }
    }
}

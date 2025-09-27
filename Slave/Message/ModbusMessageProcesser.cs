using Common.Command;
using Common.Communication;
using Common.FIFOQueue;
using Common.FileRecord;
using Common.IPointsDataBase;
using Common.Message;
using Common.Utilities;
using Slave.CommandHandler;
using Slave.Communication;

namespace Slave.Message
{
    /// <summary>
    /// Contains all the necessarily objects for processing the modbus messages
    /// </summary>
    public class ModbusMessageProcesser : IMessageProcesser<FunctionCode>
    {
        IMessageHandler messageHandler;
        IMessageDataHandler messageDataHandler = null;

        public ModbusMessageProcesser(IPointsDataBase pointsDataBase,IFileRecord fileRecord,IFIFOQueue fifoQueue, ICommunication communication)
        {
            messageDataHandler = new ModbusMessageDataHandler(pointsDataBase, fileRecord,fifoQueue);
            messageHandler = new TCPModbusMessageHandler(communication.SendBytes, messageDataHandler);
            communication.BytesRecived += messageHandler.ProcessBytes;

        }

        public void InitateMessage(FunctionCode code, IMessageDTO messageDTO)
        {
            //for unsolicited updates
        }
    }
}

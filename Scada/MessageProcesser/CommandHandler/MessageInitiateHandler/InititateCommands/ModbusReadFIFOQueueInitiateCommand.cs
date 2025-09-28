using Common.Message;
using Common.Utilities;

namespace Master.MessageProcesser.MessageInitiateHandler
{
    public class ModbusReadFIFOQueueInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusPDUData>
    {
        public IModbusPDUData InitiateMessage(IMessageDTO messageDTO)
        {
           InitiateReadFIFOModbusDTO DTO = messageDTO as InitiateReadFIFOModbusDTO;

            ModbusReadFIFOQueueRequest req = new ModbusReadFIFOQueueRequest();

            req.PointerAddress = DTO.PointAddress;

            return req;
        }
    }
}
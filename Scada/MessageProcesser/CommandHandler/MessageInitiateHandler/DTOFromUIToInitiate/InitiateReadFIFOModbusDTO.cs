using Common.Utilities;

namespace Master.MessageProcesser.MessageInitiateHandler
{
    public class InitiateReadFIFOModbusDTO:IMessageDTO
    {
        public ushort PointAddress { get; set; }
    }
}
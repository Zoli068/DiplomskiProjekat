using Common.Utilities;

namespace Master.MessageProcesser.MessageInitiateHandler
{
    public class InitiateReadWriteMultipleRegistersModbusDTO : IMessageDTO
    {
        public ushort ReadStartingAddress { get; set; }

        public ushort QuantityToRead { get; set; }  

        public ushort WriteStartingAddress { get; set; }    

        public ushort QuantityToWrite { get; set; }

        public short[] WriteRegistersValue { get; set; }
    }
}
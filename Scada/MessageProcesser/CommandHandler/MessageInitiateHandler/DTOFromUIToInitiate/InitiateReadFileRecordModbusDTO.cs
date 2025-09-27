using Common.Utilities;

namespace Master.MessageProcesser.MessageInitiateHandler
{
    public class InitiateReadFileRecordModbusDTO:IMessageDTO
    {
        public byte[] ReferenceType { get;set; }

        public ushort[] FileNumber { get; set; }

        public ushort[] RecordNumber { get; set; }

        public ushort[] RecordLength { get; set;}
    }
}
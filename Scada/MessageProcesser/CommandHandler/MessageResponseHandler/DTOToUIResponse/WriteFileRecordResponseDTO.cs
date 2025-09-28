using Common.Utilities;

namespace Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse
{
    public class WriteFileRecordResponseDTO:IMessageDTO
    {
        public byte[] ReferenceType { get; set; }

        public ushort[] FileNumber { get; set; }

        public ushort[] RecordNumber { get; set; }

        public short[][] RecordData { get; set; }
    }
}

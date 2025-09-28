using Common.Utilities;

namespace Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse
{
    public class ReadFIFOQueueResponseDTO:IMessageDTO
    {
        public ushort Address {  get; set; }

        public short[] Values { get; set; }
    }
}

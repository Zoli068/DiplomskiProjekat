using Common.Utilities;

namespace Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse
{
    public class ReadCoilsResponseDTO:IMessageDTO
    {
        public ushort Address {  get; set; }

        public byte[] Values { get; set; }
    }
}
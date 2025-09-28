using Common.Utilities;

namespace Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse
{
    public class ReadWriteRegistersResponseDTO:IMessageDTO
    {
        public ushort ReadAddress {  get; set; }

        public ushort WriteAddress { get; set; }

        public short[] ReadedValues {  get; set; }

        public short[] WriteValues { get; set;}
    }
}
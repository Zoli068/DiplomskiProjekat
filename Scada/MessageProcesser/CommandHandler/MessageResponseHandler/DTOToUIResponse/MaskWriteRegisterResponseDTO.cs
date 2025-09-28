using Common.Utilities;

namespace Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse
{
    public class MaskWriteRegisterResponseDTO:IMessageDTO
    {
        public ushort ReferenceAddress {  get; set; }

        public ushort AndMask {  get; set; }

        public ushort OrMask {  get; set; }
    }
}

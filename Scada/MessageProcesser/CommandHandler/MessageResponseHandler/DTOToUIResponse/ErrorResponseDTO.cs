using Common.Message;
using Common.Utilities;

namespace Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse
{
    public class ErrorResponseDTO:IMessageDTO
    {
        public ExceptionCode ExceptionCode { get; set; }
        public byte ErrorCode { get; set; }
    }
}
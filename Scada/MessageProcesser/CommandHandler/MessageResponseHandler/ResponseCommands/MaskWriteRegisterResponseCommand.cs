using Common.Command;
using Common.Message;
using Common.Utilities;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;

namespace Master.MessageProcesser
{
    public class MaskWriteRegisterResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public IMessageDTO Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusMaskWriteRegisterResponse res = response as ModbusMaskWriteRegisterResponse;

            MaskWriteRegisterResponseDTO responseDTO= new MaskWriteRegisterResponseDTO();

            responseDTO.OrMask = res.OrMask;
            responseDTO.AndMask= res.AndMask;
            responseDTO.ReferenceAddress = res.ReferenceAddress;

            return responseDTO;
        }
    }
}
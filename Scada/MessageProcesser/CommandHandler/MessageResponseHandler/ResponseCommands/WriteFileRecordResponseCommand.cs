using Common.Command;
using Common.Message;
using Common.Utilities;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;

namespace Master.MessageProcesser
{
    public class WriteFileRecordResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public IMessageDTO Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusWriteFileRecordResponse res= response as ModbusWriteFileRecordResponse;

            WriteFileRecordResponseDTO responseDTO= new WriteFileRecordResponseDTO();

            responseDTO.FileNumber = res.FileNumber;
            responseDTO.ReferenceType= res.ReferenceType;
            responseDTO.RecordData = res.RecordData;
            responseDTO.RecordNumber = res.RecordNumber;

            return responseDTO;
        }
    }
}
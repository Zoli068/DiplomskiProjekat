using Common.Command;
using Common.Message;
using Common.Utilities;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;

namespace Master.MessageProcesser
{
    public class ReadFileRecordResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public IMessageDTO Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusReadFileRecordResponse res= response as ModbusReadFileRecordResponse;
            ModbusReadFileRecordRequest req= request as ModbusReadFileRecordRequest;

            ReadFileRecordResponseDTO responseDTO= new ReadFileRecordResponseDTO();

            responseDTO.ReferenceType = res.ReferenceType;
            responseDTO.FileNumber = req.FileNumber;
            responseDTO.RecordNumber = req.RecordNumber;
            responseDTO.RecordData = res.RecordData;

            return responseDTO;
        }
    }
}
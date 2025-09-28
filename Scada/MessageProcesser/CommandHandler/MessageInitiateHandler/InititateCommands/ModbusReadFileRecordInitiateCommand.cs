using Common.Message;
using Common.Utilities;

namespace Master.MessageProcesser.MessageInitiateHandler
{
    public class ModbusReadFileRecordInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusPDUData>
    {
        public IModbusPDUData InitiateMessage(IMessageDTO messageDTO)
        {
           InitiateReadFileRecordModbusDTO DTO= messageDTO as InitiateReadFileRecordModbusDTO;

           ModbusReadFileRecordRequest req= new ModbusReadFileRecordRequest();

            req.ReferenceType = DTO.ReferenceType;
            req.FileNumber = DTO.FileNumber;
            req.RecordNumber=DTO.RecordNumber;
            req.RecordLength = DTO.RecordLength;

            req.ByteCount= (byte)(req.ReferenceType.Length+req.FileNumber.Length+req.RecordNumber.Length + req.RecordLength.Length);

            if (req.ReferenceType.Length == 0 || req.ReferenceType.Length != req.FileNumber.Length
            || req.ReferenceType.Length != req.RecordNumber.Length || req.ReferenceType.Length != req.RecordLength.Length)
            {
                throw new MessageDTOBadValuesException();
            }

            req.ByteCount = (byte)(7 * req.ReferenceType.Length);
            return req;
        }
    }
}
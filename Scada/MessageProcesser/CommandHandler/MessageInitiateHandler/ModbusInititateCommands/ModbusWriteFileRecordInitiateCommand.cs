using Common.Message;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.MessageProcesser.MessageInitiateHandler
{
    public class ModbusWriteFileRecordInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(IMessageDTO messageDTO)
        {
            InitiateWriteFileRecordModbusDTO DTO= messageDTO as InitiateWriteFileRecordModbusDTO;

            ModbusWriteFileRecordRequest req= new ModbusWriteFileRecordRequest();

            req.ReferenceType = DTO.ReferenceType;
            req.FileNumber = DTO.FileNumber;
            req.RecordNumber = DTO.RecordNumber;
            req.RecordLength= DTO.RecordLength;
            req.RecordData=DTO.RecordData;

            if (req.ReferenceType.Length == 0 || req.ReferenceType.Length != req.FileNumber.Length
                || req.ReferenceType.Length != req.RecordNumber.Length || req.ReferenceType.Length != req.RecordLength.Length
                || req.RecordData.Length != req.RecordNumber.Length)
            {
                throw new MessageDTOBadValuesException();
            }
            byte size = 0;

            for(int i=0;i<req.RecordData.Length;i++)
            {
                size = (byte)(size + 7 + req.RecordData[i].Length * 2);

            }

            req.RequestDataLength = size;

            return req;
        }
    }
}

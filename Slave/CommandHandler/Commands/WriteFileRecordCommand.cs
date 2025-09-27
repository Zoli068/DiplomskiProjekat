using Common.Command;
using Common.FileRecord;
using Common.Message;
using Common.PointsDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slave.CommandHandler
{
    public class WriteFileRecordCommand : IMessageDataCommand<IModbusData>
    {
        private IFileRecord fileRecord;

        public WriteFileRecordCommand(IFileRecord fileRecord)
        {
            this.fileRecord = fileRecord;
        }

        public IModbusData Execute(IModbusData data)
        {
            ModbusWriteFileRecordRequest request = data as ModbusWriteFileRecordRequest;

            if (request.RequestDataLength > 0xf5 || request.RequestDataLength < 0x07)
            {
                throw new ValueOutOfIntervalException();
            }

            if (request.ReferenceType.Length == 0 || request.ReferenceType.Length != request.FileNumber.Length
            || request.ReferenceType.Length != request.RecordNumber.Length || request.ReferenceType.Length != request.RecordLength.Length)
            {
                throw new ValueOutOfIntervalException();
            }

            for(int i = 0; i < request.RecordLength.Length; i++)
            {
                if (request.RecordLength[i] != request.RecordData[i].Length)
                {
                    throw new ValueOutOfIntervalException();
                }
            }

            short[] temp;
            try
            {
                for(int i=0; i< request.ReferenceType.Length; i++) 
                {
                    if (!fileRecord.CheckValues(request.ReferenceType[i], request.FileNumber[i], request.RecordNumber[i], request.RecordLength[i]))
                    {
                        throw new Exception();
                    }
                }

                for(int i=0;i< request.ReferenceType.Length; i++)
                {
                    temp = new short[request.RecordLength[i]];
                    
                    for(int j=0; j < request.RecordLength[i]; j++)
                    {
                        temp[j] = request.RecordData[i][j];
                    }
                    fileRecord.WriteFileRecord(request.FileNumber[i], request.RecordNumber[i],temp);
                }
            }
            catch (Exception)
            {
                throw new InvalidAddressException();
            }

            return new ModbusWriteFileRecordResponse(request.RequestDataLength, request.ReferenceType, request.FileNumber, request.RecordNumber, request.RecordLength, request.RecordData);
        }
    }
}

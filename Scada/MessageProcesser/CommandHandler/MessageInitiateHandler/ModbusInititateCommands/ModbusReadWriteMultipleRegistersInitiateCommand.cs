using Common.Message;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.MessageProcesser.MessageInitiateHandler
{
    public class ModbusReadWriteMultipleRegistersInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(IMessageDTO messageDTO)
        {
            InitiateReadWriteMultipleRegistersModbusDTO DTO = messageDTO as InitiateReadWriteMultipleRegistersModbusDTO;

            ModbusReadWriteMultipleRegistersRequest req=new ModbusReadWriteMultipleRegistersRequest();

            req.ReadStartingAddress = DTO.ReadStartingAddress;
            req.WriteStartingAddress= DTO.WriteStartingAddress;

            if(DTO.QuantityToRead<1 || DTO.QuantityToWrite < 1)
            {
                throw new MessageDTOBadValuesException();
            }

            if(DTO.QuantityToWrite != DTO.WriteRegistersValue.Length)
            {
                throw new MessageDTOBadValuesException();
            }

            req.QuantityToWrite = DTO.QuantityToWrite;
            req.QuantityToRead= DTO.QuantityToRead;
            req.WriteRegistersValue=DTO.WriteRegistersValue;

            req.WriteByteCount = (byte)(req.WriteRegistersValue.Length * 2);
            return req;
        }
    }
}

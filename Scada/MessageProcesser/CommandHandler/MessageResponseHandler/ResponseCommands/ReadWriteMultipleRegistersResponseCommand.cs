using Common.Command;
using Common.Message;
using Common.Utilities;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;

namespace Master.MessageProcesser
{
    public class ReadWriteMultipleRegistersResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public IMessageDTO Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusReadWriteMultipleRegistersRequest req= request as ModbusReadWriteMultipleRegistersRequest;
            ModbusReadWriteMultipleRegistersResponse res= response as ModbusReadWriteMultipleRegistersResponse;

            ReadWriteRegistersResponseDTO responseDTO = new ReadWriteRegistersResponseDTO();

            responseDTO.ReadAddress = req.ReadStartingAddress;
            responseDTO.ReadedValues = new short[req.QuantityToRead];

            responseDTO.WriteAddress = req.WriteStartingAddress;
            responseDTO.WriteValues = new short[req.QuantityToWrite];

            for (int i = 0; i < req.QuantityToWrite; i++)
                responseDTO.WriteValues[i] = req.WriteRegistersValue[i];

            for (int i=0;i< (res.ByteCount/2);i++)
            {
                responseDTO.ReadedValues[i]=res.ReadRegistersValue[i];
            }

            return responseDTO;
        }
    }
}
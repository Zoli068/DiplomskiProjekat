using Common.Command;
using Common.Message;
using Common.Utilities;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;

namespace Master.CommandHandler.ResponseCommands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusReadCoilsResponse"/>
    /// </summary>
    public class ReadCoilsResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public IMessageDTO Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusReadCoilsResponse res = response as ModbusReadCoilsResponse;
            ModbusReadCoilsRequest req = request as ModbusReadCoilsRequest;

            ReadCoilsResponseDTO  responseDTO= new ReadCoilsResponseDTO();

            responseDTO.Values = new byte[req.QuantityOfCoils];
            responseDTO.Address = req.StartingAddress;

            byte temp;

            for (int i = 0; i < req.QuantityOfCoils; i++)
            {
                int byteIndex = i / 8;
                int bitPosition = i % 8;

                temp = (byte)((res.CoilStatus[byteIndex] & (1 << bitPosition)));
                if (temp != 0)
                    temp = 1;

                responseDTO.Values[i]= temp;
            }

            return responseDTO;
        }
    }
}
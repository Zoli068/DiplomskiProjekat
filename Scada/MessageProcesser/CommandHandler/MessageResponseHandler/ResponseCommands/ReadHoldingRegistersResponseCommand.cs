using Common.Command;
using Common.Message;
using Common.Utilities;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;

namespace Master.CommandHandler.ResponseCommands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusReadHoldingRegistersResponse"/>
    /// </summary>
    public class ReadHoldingRegistersResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public IMessageDTO Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusReadHoldingRegistersResponse res = response as ModbusReadHoldingRegistersResponse;
            ModbusReadHoldingRegistersRequest req = request as ModbusReadHoldingRegistersRequest;

            ReadRegistersResponseDTO responseDTO = new ReadRegistersResponseDTO();

            responseDTO.Address = req.StartingAddress;
            responseDTO.Values = new short[req.QuantityOfRegisters];

            short temp;

            for (int i = 0; i < req.QuantityOfRegisters; i++)
            {
                temp = res.RegisterValue[i];
                responseDTO.Values[i] = temp;
            }

            return responseDTO;
        }
    }
}
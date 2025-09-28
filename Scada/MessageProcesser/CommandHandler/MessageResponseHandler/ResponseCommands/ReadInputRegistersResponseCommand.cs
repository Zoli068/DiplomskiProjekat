using Common.Command;
using Common.Message;
using Common.Utilities;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;

namespace Master.CommandHandler.ResponseCommands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusReadInputRegistersResponse"/>
    /// </summary>
    public class ReadInputRegistersResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public IMessageDTO Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusReadInputRegistersResponse res = response as ModbusReadInputRegistersResponse;
            ModbusReadInputRegistersRequest req = request as ModbusReadInputRegistersRequest;

            ReadRegistersResponseDTO responseDTO = new ReadRegistersResponseDTO();

            responseDTO.Address = req.StartingAddress;
            responseDTO.Values = new short[req.QuantityOfInputRegisters];

            short temp;
            for (int i = 0; i < req.QuantityOfInputRegisters; i++)
            {
                temp = res.InputRegisters[i];

                responseDTO.Values[i] = temp;
            }

            return responseDTO;
        }
    }
}
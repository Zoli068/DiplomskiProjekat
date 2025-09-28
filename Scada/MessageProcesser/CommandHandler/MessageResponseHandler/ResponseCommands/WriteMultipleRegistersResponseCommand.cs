using Common.Command;
using Common.Message;
using Common.Utilities;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;

namespace Master.CommandHandler.ResponseCommands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusWriteMultipleRegistersResponse"/>
    /// </summary>
    public class WriteMultipleRegistersResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public IMessageDTO Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusWriteMultipleRegistersResponse res = response as ModbusWriteMultipleRegistersResponse;
            ModbusWriteMultipleRegistersRequest req = request as ModbusWriteMultipleRegistersRequest;

            WriteRegistersResponseDTO responseDTO=new WriteRegistersResponseDTO();

            responseDTO.Address = req.StartingAddress;
            responseDTO.Values = new short[req.QuantityOfRegisters];

            for (int i = 0; i < res.QuantityOfRegisters; i++)
            {
                responseDTO.Values[i]=req.RegisterValue[i];
            }

            return responseDTO;
        }
    }
}
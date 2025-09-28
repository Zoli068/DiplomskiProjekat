using Common.Command;
using Common.Message;
using Common.Utilities;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;

namespace Master.CommandHandler.ResponseCommands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusWriteSingleRegisterResponse"/>
    /// </summary>
    public class WriteSingleRegisterResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public IMessageDTO Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusWriteSingleRegisterResponse res = response as ModbusWriteSingleRegisterResponse;
            ModbusWriteSingleRegisterRequest req = request as ModbusWriteSingleRegisterRequest;

            WriteRegistersResponseDTO responseDTO=new WriteRegistersResponseDTO();

            responseDTO.Address = req.RegisterAddress;
            responseDTO.Values = new short[1];

            responseDTO.Values[0]=res.RegisterValue;

            return responseDTO;
        }
    }
}
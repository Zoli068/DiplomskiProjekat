using Common.Command;
using Common.Message;
using Common.Utilities;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;

namespace Master.CommandHandler.ResponseCommands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusWriteSingleCoilResponse"/>
    /// </summary>
    public class WriteSingleCoilResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public IMessageDTO Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusWriteSingleCoilResponse res = response as ModbusWriteSingleCoilResponse;
            ModbusWriteSingleCoilRequest req = request as ModbusWriteSingleCoilRequest;

            WriteCoilsResponseDTO responseDTO=new WriteCoilsResponseDTO();

            responseDTO.Address = req.OutputAddress;
            responseDTO.Values = new byte[1];

            byte temp;
            if (req.OutputValue == 0)
            {
                temp = 0;
            }
            else
            {
                temp = 1;
            }

            responseDTO.Values[0] = temp;

            return responseDTO;
        }
    }
}
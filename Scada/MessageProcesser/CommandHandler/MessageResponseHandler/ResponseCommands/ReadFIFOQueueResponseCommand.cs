using Common.Command;
using Common.Message;
using Common.Utilities;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;

namespace Master.MessageProcesser
{
    public class ReadFIFOQueueResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public IMessageDTO Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusReadFIFOQueueResponse res = response as ModbusReadFIFOQueueResponse;
            ModbusReadFIFOQueueRequest req = request as ModbusReadFIFOQueueRequest;

            ReadFIFOQueueResponseDTO responseDTO = new ReadFIFOQueueResponseDTO();
            responseDTO.Address = req.PointerAddress;

            for (int i = 0; i < res.FIFOCount; i++)
            {
                responseDTO.Values[i] = res.FIFOValueRegister[i];
             }

            return responseDTO;
        }
    }
}
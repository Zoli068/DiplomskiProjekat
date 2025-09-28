using Common.Command;
using Common.Message;
using Common.Utilities;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;

namespace Master.CommandHandler.ResponseCommands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusWriteMultipleCoilsResponse"/>
    /// </summary>
    public class WriteMultipleCoilsResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public IMessageDTO Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusWriteMultipleCoilsResponse res = response as ModbusWriteMultipleCoilsResponse;
            ModbusWriteMultipleCoilsRequest req = request as ModbusWriteMultipleCoilsRequest;

            WriteCoilsResponseDTO responseDTO= new WriteCoilsResponseDTO();

            responseDTO.Address = req.StartingAddress;
            responseDTO.Values = new byte[req.QuantityOfOutputs];

            byte temp;

            for (int i = 0; i < req.QuantityOfOutputs; i++)
            {
                int byteIndex = i / 8;
                int bitPosition = i % 8;

                temp = (byte)((req.OutputsValue[byteIndex] & (1 << bitPosition)));

                if (temp != 0)
                    temp = 1;

                responseDTO.Values[i] = temp;
            }

            return responseDTO;
        }
    }
}
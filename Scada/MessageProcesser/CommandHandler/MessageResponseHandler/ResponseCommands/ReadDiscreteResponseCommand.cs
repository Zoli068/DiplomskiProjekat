using Common.Command;
using Common.Message;
using Common.Utilities;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;

namespace Master.CommandHandler.ResponseCommands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusReadDiscreteInputsResponse"/>
    /// </summary>
    public class ReadDiscreteResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public IMessageDTO Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusReadDiscreteInputsRequest req = (ModbusReadDiscreteInputsRequest)request;
            ModbusReadDiscreteInputsResponse res = (ModbusReadDiscreteInputsResponse)response;

            ReadCoilsResponseDTO responseDTO = new ReadCoilsResponseDTO();

            responseDTO.Address = req.StartingAddress;
            responseDTO.Values = new byte[req.QuantityOfInputs];

            byte temp;

            for (int i = 0; i < req.QuantityOfInputs; i++)
            {
                int byteIndex = i / 8;
                int bitPosition = i % 8;

                temp = (byte)((res.InputStatus[byteIndex] & (1 << bitPosition)));
                if (temp != 0)
                    temp = 1;

                responseDTO.Values[i] = temp;
            }

            return responseDTO;
        }    
    }
}
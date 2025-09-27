using Common.Message;
using Common.Utilities;

namespace Master.CommandHandler.MessageInitiateHandler
{
    /// <summary>
    /// Command for handling the ModbusDTO when we want to create a request: <see cref="ModbusReadDiscreteInputsRequest"/>
    /// </summary>
    public class ModbusReadDiscreteInputsInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(IMessageDTO messageDTO)
        {
            InitiateReadModbusDTO DTO = messageDTO as InitiateReadModbusDTO;

            ModbusReadDiscreteInputsRequest req = new ModbusReadDiscreteInputsRequest();

            if (DTO.Address < 1 || DTO.Quantity < 1 || DTO.Quantity > 2000)
            {
                throw new MessageDTOBadValuesException();
            }

            req.StartingAddress = DTO.Address;
            req.QuantityOfInputs = DTO.Quantity;

            return req;
        }
    }
}

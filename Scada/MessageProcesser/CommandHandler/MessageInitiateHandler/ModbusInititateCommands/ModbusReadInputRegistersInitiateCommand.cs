using Common.Message;
using Common.Utilities;

namespace Master.CommandHandler.MessageInitiateHandler
{
    /// <summary>
    /// Command for handling the ModbusDTO when we want to create a request: <see cref="ModbusReadInputRegistersRequest"/>
    /// </summary>
    public class ModbusReadInputRegistersInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(IMessageDTO messageDTO)
        {
            InitiateReadModbusDTO DTO = messageDTO as InitiateReadModbusDTO;

            ModbusReadInputRegistersRequest req = new ModbusReadInputRegistersRequest();

            if (DTO.Address < 1 || DTO.Quantity < 1 || DTO.Quantity > 125)
            {
                throw new MessageDTOBadValuesException();
            }

            req.StartingAddress = DTO.Address;
            req.QuantityOfInputRegisters = DTO.Quantity;

            return req;
        }
    }
}

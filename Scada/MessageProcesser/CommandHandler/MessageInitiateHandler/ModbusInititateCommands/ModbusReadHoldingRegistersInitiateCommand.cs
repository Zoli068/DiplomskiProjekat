using Common.Message;
using Common.Utilities;

namespace Master.CommandHandler.MessageInitiateHandler
{
    /// <summary>
    /// Command for handling the ModbusDTO when we want to create a request: <see cref="ModbusReadHoldingRegistersRequest"/>
    /// </summary>
    public class ModbusReadHoldingRegistersInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(IMessageDTO messageDTO)
        {
            InitiateReadModbusDTO DTO = messageDTO as InitiateReadModbusDTO;

            ModbusReadHoldingRegistersRequest req = new ModbusReadHoldingRegistersRequest();

            if (DTO.Address < 1 || DTO.Quantity < 1 || DTO.Quantity > 125)
            {
                throw new MessageDTOBadValuesException();
            }

            req.StartingAddress = DTO.Address;
            req.QuantityOfRegisters = DTO.Quantity;

            return req;
        }
    }
}

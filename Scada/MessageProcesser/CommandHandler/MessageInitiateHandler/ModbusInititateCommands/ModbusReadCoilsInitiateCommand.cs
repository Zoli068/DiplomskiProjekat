using Common.Message;
using Common.Utilities;

namespace Master.CommandHandler.MessageInitiateHandler
{
    /// <summary>
    /// Command for handling the ModbusDTO when we want to create a request: <see cref="ModbusReadCoilsRequest"/>
    /// </summary>
    public class ModbusReadCoilsInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusData>
    {
        IModbusData IMessageInitiateCommand<IMessageDTO, IModbusData>.InitiateMessage(IMessageDTO messageDTO)
        {
            InitiateReadModbusDTO DTO = messageDTO as InitiateReadModbusDTO;

            ModbusReadCoilsRequest req = new ModbusReadCoilsRequest();

            if (DTO.Address < 1 || DTO.Quantity < 1 || DTO.Quantity > 2000)
            {
                throw new MessageDTOBadValuesException();
            }

            req.StartingAddress = DTO.Address;
            req.QuantityOfCoils = DTO.Quantity;

            return req;
        }
    }
}

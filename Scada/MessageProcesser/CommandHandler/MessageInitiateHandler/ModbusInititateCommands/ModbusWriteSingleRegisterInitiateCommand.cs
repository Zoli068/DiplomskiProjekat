using Common.Message;
using Common.Utilities;

namespace Master.CommandHandler.MessageInitiateHandler
{
    /// <summary>
    /// Command for handling the ModbusDTO when we want to create a request: <see cref="ModbusWriteSingleRegisterRequest"/>
    /// </summary>
    public class ModbusWriteSingleRegisterInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(IMessageDTO messageDTO)
        {
            InitiateWriteSingleModbusDTO DTO = messageDTO as InitiateWriteSingleModbusDTO;

            ModbusWriteSingleRegisterRequest request = new ModbusWriteSingleRegisterRequest();

            if (DTO.Address < 1)
            {
                throw new MessageDTOBadValuesException();
            }
            request.RegisterAddress = DTO.Address;
            request.RegisterValue = DTO.Value;

            return request;
        }
    }
}

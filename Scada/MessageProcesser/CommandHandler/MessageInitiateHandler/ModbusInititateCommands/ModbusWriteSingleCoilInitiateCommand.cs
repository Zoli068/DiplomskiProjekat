using Common.Message;
using Common.Utilities;

namespace Master.CommandHandler.MessageInitiateHandler
{
    /// <summary>
    /// Command for handling the ModbusDTO when we want to create a request: <see cref="ModbusWriteSingleCoilRequest"/>
    /// </summary>
    public class ModbusWriteSingleCoilInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(IMessageDTO messageDTO)
        {
            InitiateWriteSingleModbusDTO DTO = messageDTO as InitiateWriteSingleModbusDTO;

            ModbusWriteSingleCoilRequest request = new ModbusWriteSingleCoilRequest();

            if (DTO.Address < 1)
            {
                throw new MessageDTOBadValuesException();
            }

            request.OutputAddress = DTO.Address;

            if (DTO.Value == 0)
            {
                request.OutputValue = 0;
            }
            else
            {
                request.OutputValue = 0xFF00;
            }

            return request;
        }
    }
}

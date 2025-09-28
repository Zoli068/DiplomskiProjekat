using Common.Message;
using Common.Utilities;

namespace Master.CommandHandler.MessageInitiateHandler
{
    public class ModbusMaskWriteRegisterInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusPDUData>
    {
        public IModbusPDUData InitiateMessage(IMessageDTO messageDTO)
        {
            InitiateMaskWriteModbusDTO DTO = messageDTO as InitiateMaskWriteModbusDTO;

            ModbusMaskWriteRegisterRequest req= new ModbusMaskWriteRegisterRequest();

            if (DTO.Address < 1)
            {
                throw new MessageDTOBadValuesException();
            }

            req.ReferenceAddress = DTO.Address;
            req.OrMask = DTO.OrMask;
            req.AndMask = DTO.AndMask;

            return req;
        }
    }
}
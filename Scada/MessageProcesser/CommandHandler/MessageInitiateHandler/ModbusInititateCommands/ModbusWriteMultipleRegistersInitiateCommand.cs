using Common.Message;
using Common.Utilities;


namespace Master.CommandHandler.MessageInitiateHandler
{
    /// <summary>
    /// Command for handling the ModbusDTO when we want to create a request: <see cref="ModbusWriteMultipleRegistersRequest"/>
    /// </summary>
    public class ModbusWriteMultipleRegistersInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(IMessageDTO messageDTO)
        {
            InitiateWriteMultipleModbusDTO DTO = messageDTO as InitiateWriteMultipleModbusDTO;

            ModbusWriteMultipleRegistersRequest request = new ModbusWriteMultipleRegistersRequest();

            if (DTO.Address < 1 || DTO.Quantity < 1 || DTO.Quantity > 123 || DTO.Values.Length < DTO.Quantity)
            {
                throw new MessageDTOBadValuesException();
            }

            request.StartingAddress = DTO.Address;
            request.QuantityOfRegisters = DTO.Quantity;
            request.ByteCount = (byte)(request.QuantityOfRegisters * 2);
            request.RegisterValue = new short[request.ByteCount];

            for (int i = 0; i < request.QuantityOfRegisters; i++)
            {
                request.RegisterValue[i] = DTO.Values[i];
            }

            return request;
        }
    }
}

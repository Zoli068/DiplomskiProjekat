using Common.Message;
using Common.Utilities;

namespace Master.CommandHandler.MessageInitiateHandler
{
    /// <summary>
    /// Command for handling the ModbusDTO when we want to create a request: <see cref="ModbusWriteMultipleCoilsRequest"/>
    /// </summary>
    public class ModbusWriteMultipleCoilsInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(IMessageDTO messageDTO)
        {
            InitiateWriteMultipleModbusDTO DTO = messageDTO as InitiateWriteMultipleModbusDTO;

            ModbusWriteMultipleCoilsRequest req = new ModbusWriteMultipleCoilsRequest();

            if (DTO.Address < 1 || DTO.Quantity < 1 || DTO.Quantity > 1968 || DTO.Values.Length < DTO.Quantity)
            {
                throw new MessageDTOBadValuesException();
            }

            req.StartingAddress = DTO.Address;
            req.QuantityOfOutputs = DTO.Quantity;

            byte byteCount = (byte)(req.QuantityOfOutputs / 8);

            if ((req.QuantityOfOutputs % 8) > 0)
            {
                byteCount++;
            }

            req.ByteCount = byteCount;
            req.OutputsValue = new byte[byteCount];

            for (ushort i = 0; i < DTO.Values.Length; i++)
            {
                int byteIndex = i / 8;
                int bitPosition = i % 8;

                if (DTO.Values[i] != 0)
                    req.OutputsValue[byteIndex] |= (byte)(1 << bitPosition);
            }

            return req;
        }
    }
}

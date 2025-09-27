using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using Common.PointsDataBase;

namespace Slave.CommandHandler.Commands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusWriteMultipleRegistersRequest"/>
    /// </summary>
    public class WriteMultipleRegistersCommand : IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;

        public WriteMultipleRegistersCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        }

        public IModbusData Execute(IModbusData data)
        {
            ModbusWriteMultipleRegistersRequest request = data as ModbusWriteMultipleRegistersRequest;

            byte byteCount = (byte)(2 * request.QuantityOfRegisters);

            if (request.QuantityOfRegisters < 1 || request.QuantityOfRegisters > 123)
            {
                throw new ValueOutOfIntervalException();
            }
            else if (byteCount != request.ByteCount)
            {
                throw new ValueOutOfIntervalException();
            }

            if (!(pointsDataBase.CheckAddress(request.StartingAddress) & pointsDataBase.CheckAddress((ushort)(request.StartingAddress + request.QuantityOfRegisters - 1))))
            {
                throw new InvalidAddressException();
            }

            for (int i = 0; i < request.QuantityOfRegisters; i++)
            {
                pointsDataBase.WriteRegisterValue((ushort)(request.StartingAddress + i), PointsType.HOLDING_REGISTERS, request.RegisterValue[i]);
            }

            return new ModbusWriteMultipleRegistersResponse(request.StartingAddress, request.QuantityOfRegisters);
        }
    }
}

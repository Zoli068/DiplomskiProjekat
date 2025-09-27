using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using Common.PointsDataBase;

namespace Slave.CommandHandler.Commands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusReadInputRegistersRequest"/>
    /// </summary>
    public class ReadInputRegistersCommand : IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;

        public ReadInputRegistersCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        }

        public IModbusData Execute(IModbusData data)
        {
            ModbusReadInputRegistersRequest request = data as ModbusReadInputRegistersRequest;

            if (request.QuantityOfInputRegisters < 1 || request.QuantityOfInputRegisters > 125)
            {
                throw new ValueOutOfIntervalException();
            }

            if (!(pointsDataBase.CheckAddress(request.StartingAddress) & pointsDataBase.CheckAddress((ushort)(request.StartingAddress + request.QuantityOfInputRegisters - 1))))
            {
                throw new InvalidAddressException();
            }

            byte byteCount = (byte)(2 * request.QuantityOfInputRegisters);
            short[] bytes = new short[byteCount];
            ushort address;

            for (int i = 0; i < request.QuantityOfInputRegisters; i++)
            {
                address = (ushort)(request.StartingAddress + i);
                bytes[i] = pointsDataBase.ReadRegisterValue(address, PointsType.INPUT_REGISTERS);
            }

            return new ModbusReadInputRegistersResponse(byteCount, bytes);
        }
    }
}

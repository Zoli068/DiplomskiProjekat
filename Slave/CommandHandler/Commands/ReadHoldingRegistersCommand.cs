using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using Common.PointsDataBase;

namespace Slave.CommandHandler.Commands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusReadHoldingRegistersRequest"/>
    /// </summary>
    public class ReadHoldingRegistersCommand : IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;

        public ReadHoldingRegistersCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        }


        public IModbusData Execute(IModbusData modbusData)
        {
            ModbusReadHoldingRegistersRequest request = modbusData as ModbusReadHoldingRegistersRequest;

            if (request.QuantityOfRegisters < 1 || request.QuantityOfRegisters > 125)
            {
                throw new ValueOutOfIntervalException();
            }

            if (!(pointsDataBase.CheckAddress(request.StartingAddress) & pointsDataBase.CheckAddress((ushort)(request.StartingAddress + request.QuantityOfRegisters - 1))))
            {
                throw new InvalidAddressException();
            }

            byte byteCount = (byte)(2 * request.QuantityOfRegisters);
            short[] bytes = new short[byteCount];
            ushort address;

            for (int i = 0; i < request.QuantityOfRegisters; i++)
            {
                address = (ushort)(request.StartingAddress + i);
                bytes[i] = pointsDataBase.ReadRegisterValue(address, PointsType.HOLDING_REGISTERS);
            }

            return new ModbusReadHoldingRegistersResponse(byteCount, bytes);
        }
    }
}

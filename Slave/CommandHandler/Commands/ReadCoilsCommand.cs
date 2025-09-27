using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using Common.PointsDataBase;

namespace Slave.CommandHandler.Commands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusReadCoilsRequest"/>
    /// </summary>
    public class ReadCoilsCommand : IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;

        public ReadCoilsCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        }

        public IModbusData Execute(IModbusData modbusData)
        {
            ModbusReadCoilsRequest data = modbusData as ModbusReadCoilsRequest;

            if (data.QuantityOfCoils < 1 || data.QuantityOfCoils > 2000)
            {
                throw new ValueOutOfIntervalException();
            }

            if (!(pointsDataBase.CheckAddress(data.StartingAddress) & pointsDataBase.CheckAddress((ushort)(data.StartingAddress + data.QuantityOfCoils - 1))))
            {
                throw new InvalidAddressException();
            }

            byte byteCount = (byte)(data.QuantityOfCoils / 8);

            if (data.QuantityOfCoils % 8 != 0)
            {
                byteCount += 1;
            }

            byte[] bytes = new byte[byteCount];
            ushort address;
            int byteIndex;
            int bitPosition;

            for (ushort i = 0; i < data.QuantityOfCoils; i++)
            {
                byteIndex = i / 8;
                bitPosition = i % 8;
                address = (ushort)(data.StartingAddress + i);

                if (pointsDataBase.ReadDiscreteValue(address, PointsType.COILS) != 0)
                {
                    bytes[byteIndex] |= (byte)(1 << bitPosition);
                }
            }

            return new ModbusReadCoilsResponse(byteCount, bytes);
        }
    }
}

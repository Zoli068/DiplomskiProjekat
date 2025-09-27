using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using Common.PointsDataBase;

namespace Slave.CommandHandler.Commands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusReadDiscreteInputsRequest"/>
    /// </summary>
    public class ReadDiscreteInputsCommand : IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;

        public ReadDiscreteInputsCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        }

        public IModbusData Execute(IModbusData modbusData)
        {
            ModbusReadDiscreteInputsRequest data = modbusData as ModbusReadDiscreteInputsRequest;

            if (data.QuantityOfInputs < 1 || data.QuantityOfInputs > 2000)
            {
                throw new ValueOutOfIntervalException();
            }

            if (!(pointsDataBase.CheckAddress(data.StartingAddress) & pointsDataBase.CheckAddress((ushort)(data.StartingAddress + data.QuantityOfInputs - 1))))
            {
                throw new InvalidAddressException();
            }

            byte byteCount = (byte)(data.QuantityOfInputs / 8);

            if (data.QuantityOfInputs % 8 != 0)
            {
                byteCount += 1;
            }

            byte[] bytes = new byte[byteCount];
            ushort address;
            int byteIndex;
            int bitPosition;

            for (ushort i = 0; i < data.QuantityOfInputs; i++)
            {
                byteIndex = i / 8;
                bitPosition = i % 8;
                address = (ushort)(data.StartingAddress + i);

                if (pointsDataBase.ReadDiscreteValue(address, PointsType.DISCRETE_INPUTS) != 0)
                {
                    bytes[byteIndex] |= (byte)(1 << bitPosition);
                }
            }

            return new ModbusReadDiscreteInputsResponse(byteCount, bytes);
        }
    }
}

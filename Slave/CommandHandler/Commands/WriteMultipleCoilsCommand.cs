using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using Common.PointsDataBase;

namespace Slave.CommandHandler.Commands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusWriteMultipleCoilsRequest"/>
    /// </summary>
    public class WriteMultipleCoilsCommand : IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;

        public WriteMultipleCoilsCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        }

        public IModbusData Execute(IModbusData data)
        {
            ModbusWriteMultipleCoilsRequest request = data as ModbusWriteMultipleCoilsRequest;

            byte byteCount = (byte)(request.QuantityOfOutputs / 8);

            if (request.QuantityOfOutputs % 8 != 0)
            {
                byteCount += 1;
            }

            if (request.QuantityOfOutputs < 1 || request.QuantityOfOutputs > 1968)
            {
                throw new ValueOutOfIntervalException();
            }
            else if (byteCount != request.ByteCount)
            {
                throw new ValueOutOfIntervalException();
            }

            if (!(pointsDataBase.CheckAddress(request.StartingAddress) & pointsDataBase.CheckAddress((ushort)(request.StartingAddress + request.QuantityOfOutputs - 1))))
            {
                throw new InvalidAddressException();
            }

            byte temp;

            for (int i = 0; i < request.QuantityOfOutputs; i++)
            {
                int byteIndex = i / 8;
                int bitPosition = i % 8;

                temp = (byte)((request.OutputsValue[byteIndex] & (1 << bitPosition)));

                pointsDataBase.WriteDiscreteValue((ushort)(request.StartingAddress + i), PointsType.COILS, temp);
            }

            return new ModbusWriteMultipleCoilsResponse(request.StartingAddress, request.QuantityOfOutputs);
        }
    }
}

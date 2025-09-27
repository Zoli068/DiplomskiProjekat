using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using Common.PointsDataBase;

namespace Slave.CommandHandler.Commands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusWriteSingleRegisterRequest"/>
    /// </summary>
    public class WriteSingleRegisterCommand : IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;

        public WriteSingleRegisterCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        }

        public IModbusData Execute(IModbusData data)
        {
            ModbusWriteSingleRegisterRequest request = data as ModbusWriteSingleRegisterRequest;

            if (!(pointsDataBase.CheckAddress(request.RegisterAddress)))
            {
                throw new InvalidAddressException();
            }

            pointsDataBase.WriteRegisterValue(request.RegisterAddress, PointsType.HOLDING_REGISTERS, request.RegisterValue);

            return new ModbusWriteSingleRegisterResponse(request.RegisterAddress, request.RegisterValue);
        }
    }
}

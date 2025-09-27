using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using Common.PointsDataBase;

namespace Slave.CommandHandler.Commands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusMaskWriteRegisterRequest"/>
    /// </summary>
    public class MaskWriteRegisterCommand:IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;

        public MaskWriteRegisterCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        }

        public IModbusData Execute(IModbusData data)
        {
            ModbusMaskWriteRegisterRequest request = data as ModbusMaskWriteRegisterRequest;

            if (!(pointsDataBase.CheckAddress(request.ReferenceAddress)))
            {
                throw new InvalidAddressException();
            }

            short oldValue = pointsDataBase.ReadRegisterValue(request.ReferenceAddress, PointsType.HOLDING_REGISTERS);

            short newValue= (short)((oldValue & request.AndMask) | (request.OrMask & ~request.AndMask));

            pointsDataBase.WriteRegisterValue(request.ReferenceAddress,PointsType.HOLDING_REGISTERS,newValue);

            return new ModbusMaskWriteRegisterResponse(request.ReferenceAddress,request.AndMask,request.OrMask);
        }
    }
}

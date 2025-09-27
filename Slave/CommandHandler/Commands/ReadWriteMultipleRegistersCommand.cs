using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using Common.PointsDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slave.CommandHandler.Commands
{
    public class ReadWriteMultipleRegistersCommand : IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;

        public ReadWriteMultipleRegistersCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        }

        public IModbusData Execute(IModbusData data)
        {
            ModbusReadWriteMultipleRegistersRequest req= data as ModbusReadWriteMultipleRegistersRequest;

            if(req.QuantityToRead<1 || req.QuantityToRead>125 || req.QuantityToWrite<1 
                || req.QuantityToWrite > 121 || req.WriteByteCount != (req.QuantityToWrite*2))
            {
                throw new ValueOutOfIntervalException();
            }

            if(!(pointsDataBase.CheckAddress(req.ReadStartingAddress)) || !(pointsDataBase.CheckAddress((ushort)(req.ReadStartingAddress + req.QuantityToRead -1))) 
                || !(pointsDataBase.CheckAddress(req.WriteStartingAddress)) || !(pointsDataBase.CheckAddress((ushort)(req.WriteStartingAddress + req.QuantityToWrite - 1))))
            {
                throw new InvalidAddressException();
            }
        
            for(int i=0;i<req.QuantityToWrite;i++)
            {
                pointsDataBase.WriteRegisterValue((ushort)(req.WriteStartingAddress + i), PointsType.HOLDING_REGISTERS, req.WriteRegistersValue[i]);
            }
        
            ModbusReadWriteMultipleRegistersResponse res=  new ModbusReadWriteMultipleRegistersResponse();

            res.ByteCount = (byte)(req.QuantityToRead * 2);

            res.ReadRegistersValue = new short[req.QuantityToRead];
            
            for(int i = 0; i < req.QuantityToRead; i++)
            {
                res.ReadRegistersValue[i] = pointsDataBase.ReadRegisterValue((ushort)(req.ReadStartingAddress + i), PointsType.HOLDING_REGISTERS);
            }

            return res;
        }
    }
}
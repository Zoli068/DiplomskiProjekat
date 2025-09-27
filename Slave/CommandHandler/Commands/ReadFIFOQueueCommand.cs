using Common.Command;
using Common.FIFOQueue;
using Common.Message;
using Common.PointsDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slave.CommandHandler.Commands
{
    internal class ReadFIFOQueueCommand : IMessageDataCommand<IModbusData>
    {
        private IFIFOQueue fIfoQueue;

        public ReadFIFOQueueCommand(IFIFOQueue fIfoQueue)
        {
            this.fIfoQueue = fIfoQueue;
        }

        public IModbusData Execute(IModbusData data)
        {
            ModbusReadFIFOQueueRequest requset = data as ModbusReadFIFOQueueRequest;
            
            ModbusReadFIFOQueueResponse response=new ModbusReadFIFOQueueResponse();

            if(fIfoQueue.CheckAddress(requset.PointerAddress))
            {
                response.FIFOValueRegister=fIfoQueue.GetValuesFromQueue(requset.PointerAddress);
                response.FIFOCount = (ushort)response.FIFOValueRegister.Length;
                response.ByteCount = (ushort)(response.FIFOValueRegister.Length * 2 + 2);

                return response;
            }
            else
            {
                throw new InvalidAddressException();
            }
        }
    }
}

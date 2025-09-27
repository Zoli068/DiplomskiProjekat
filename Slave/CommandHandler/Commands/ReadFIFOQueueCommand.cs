using Common.Command;
using Common.FIFOQueue;
using Common.Message;
using Common.PointsDataBase;

namespace Slave.CommandHandler.Commands
{
    internal class ReadFIFOQueueCommand : IMessageDataCommand<IModbusPDUData>
    {
        private IFIFOQueue fIfoQueue;

        public ReadFIFOQueueCommand(IFIFOQueue fIfoQueue)
        {
            this.fIfoQueue = fIfoQueue;
        }

        public IModbusPDUData Execute(IModbusPDUData data)
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
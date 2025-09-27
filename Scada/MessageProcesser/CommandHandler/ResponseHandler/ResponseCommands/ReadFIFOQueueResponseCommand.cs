using Common.Command;
using Common.Message;
using System;

namespace Master.MessageProcesser
{
    public class ReadFIFOQueueResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public void Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusReadFIFOQueueResponse res= response as ModbusReadFIFOQueueResponse;
            ModbusReadFIFOQueueRequest req= request as ModbusReadFIFOQueueRequest;

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Read FIFO Queue Response:");
            Console.WriteLine("--------------------------------------------------------------");

            Console.WriteLine("Pointer address:" + req.PointerAddress);
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Byte Counnt:" + res.ByteCount);
            Console.WriteLine("-------------------------------");
            Console.WriteLine("FIFO Counnt:" + res.FIFOCount);
            Console.WriteLine("-------------------------------");

            for(int i=0; i < res.FIFOCount; i++)
            {
                Console.WriteLine("Address:"+(req.PointerAddress+i)+" Value:" + res.FIFOValueRegister[i]);
                Console.WriteLine("-------------------------------");
            }

        }
    }
}
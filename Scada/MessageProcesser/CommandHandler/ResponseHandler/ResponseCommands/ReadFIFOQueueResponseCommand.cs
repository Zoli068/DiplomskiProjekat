using Common.Command;
using Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.MessageProcesser
{
    public class ReadFIFOQueueResponseCommand : IResponseMessageDataCommand<IModbusData>
    {
        public void Execute(IModbusData request, IModbusData response)
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

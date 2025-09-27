using Common.Command;
using Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.MessageProcesser
{
    public class ReadWriteMultipleRegistersResponseCommand : IResponseMessageDataCommand<IModbusData>
    {
        public void Execute(IModbusData request, IModbusData response)
        {
            ModbusReadWriteMultipleRegistersRequest req= request as ModbusReadWriteMultipleRegistersRequest;
            ModbusReadWriteMultipleRegistersResponse res= response as ModbusReadWriteMultipleRegistersResponse;

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Read Write Multiple Registers Response:");
            Console.WriteLine("--------------------------------------------------------------");

            Console.WriteLine("Byte Count:" + res.ByteCount);
            Console.WriteLine("-------------------------------");

            for (int i=0;i< (res.ByteCount/2);i++)
            {
                Console.WriteLine("Address" + (req.ReadStartingAddress + i) + " value:" + res.ReadRegistersValue[i]);
                Console.WriteLine("-------------------------------");
            }
        }
    }
}

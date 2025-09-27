using Common.Command;
using Common.Message;
using System;

namespace Master.CommandHandler.ResponseCommands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusWriteMultipleRegistersResponse"/>
    /// </summary>
    public class WriteMultipleRegistersResponseCommand : IResponseMessageDataCommand<IModbusData>
    {
        public void Execute(IModbusData request, IModbusData response)
        {
            ModbusWriteMultipleRegistersResponse res = response as ModbusWriteMultipleRegistersResponse;

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Write Multiple Registers Response:");
            Console.WriteLine("--------------------------------------------------------------");

            ModbusWriteMultipleRegistersRequest req = request as ModbusWriteMultipleRegistersRequest;

            Console.WriteLine("Quantity of registers" + req.QuantityOfRegisters);

            for (int i = 0; i < res.QuantityOfRegisters; i++)
            {

                Console.WriteLine("-------------------------------");

                Console.WriteLine((req.StartingAddress + i) + " address:" + req.RegisterValue[i]);
            }

            Console.WriteLine("-------------------------------");
        }
    }
}

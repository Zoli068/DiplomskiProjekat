using Common.Command;
using Common.Message;
using System;

namespace Master.CommandHandler.ResponseCommands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusReadHoldingRegistersResponse"/>
    /// </summary>
    public class ReadHoldingRegistersResponseCommand : IResponseMessageDataCommand<IModbusData>
    {
        public void Execute(IModbusData request, IModbusData response)
        {
            ModbusReadHoldingRegistersResponse res = response as ModbusReadHoldingRegistersResponse;

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Read Holding Registers Response:");
            Console.WriteLine("--------------------------------------------------------------");

            ModbusReadHoldingRegistersRequest req = request as ModbusReadHoldingRegistersRequest;

            Console.WriteLine("Quantity of registers:" + req.QuantityOfRegisters);

            short temp;
            for (int i = 0; i < req.QuantityOfRegisters; i++)
            {
                Console.WriteLine("-------------------------------");
                temp = res.RegisterValue[i];
                Console.WriteLine((req.StartingAddress + i) + " address:" + temp);

            }
            Console.WriteLine("-------------------------------");

        }
    }
}

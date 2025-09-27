using Common.Command;
using Common.Message;
using System;

namespace Master.CommandHandler.ResponseCommands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusReadInputRegistersResponse"/>
    /// </summary>
    public class ReadInputRegistersResponseCommand : IResponseMessageDataCommand<IModbusData>
    {
        public void Execute(IModbusData request, IModbusData response)
        {
            ModbusReadInputRegistersResponse res = response as ModbusReadInputRegistersResponse;

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Read Input Registers Response:");
            Console.WriteLine("--------------------------------------------------------------");

            ModbusReadInputRegistersRequest req = request as ModbusReadInputRegistersRequest;

            Console.WriteLine("Quantity of input registers:" + req.QuantityOfInputRegisters);

            short temp;
            for (int i = 0; i < req.QuantityOfInputRegisters; i++)
            {
                Console.WriteLine("-------------------------------");
                temp = res.InputRegisters[i];
                Console.WriteLine((req.StartingAddress + i) + " address:" + temp);
            }

            Console.WriteLine("-------------------------------");

        }
    }
}

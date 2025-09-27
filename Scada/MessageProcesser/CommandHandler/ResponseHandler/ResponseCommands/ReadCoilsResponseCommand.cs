using Common.Command;
using Common.Message;
using System;

namespace Master.CommandHandler.ResponseCommands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusReadCoilsResponse"/>
    /// </summary>
    public class ReadCoilsResponseCommand : IResponseMessageDataCommand<IModbusData>
    {
        public void Execute(IModbusData request, IModbusData response)
        {
            ModbusReadCoilsResponse res = response as ModbusReadCoilsResponse;

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Read Coils Response:");
            Console.WriteLine("--------------------------------------------------------------");

            ModbusReadCoilsRequest req = request as ModbusReadCoilsRequest;

            Console.WriteLine("Quantity of coils:" + req.QuantityOfCoils);

            byte temp;
            for (int i = 0; i < req.QuantityOfCoils; i++)
            {
                Console.WriteLine("-------------------------------");
                int byteIndex = i / 8;
                int bitPosition = i % 8;

                temp = (byte)((res.CoilStatus[byteIndex] & (1 << bitPosition)));
                if (temp != 0)
                    temp = 1;
                Console.WriteLine((req.StartingAddress + i) + " address:" + temp);

            }

            Console.WriteLine("-------------------------------");
        }
    }
}

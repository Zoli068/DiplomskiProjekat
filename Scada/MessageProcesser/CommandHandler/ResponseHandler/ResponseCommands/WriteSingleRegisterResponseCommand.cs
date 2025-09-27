using Common.Command;
using Common.Message;
using System;

namespace Master.CommandHandler.ResponseCommands
{
    /// <summary>
    /// Class that will handle the incoming <see cref="ModbusWriteSingleRegisterResponse"/>
    /// </summary>
    public class WriteSingleRegisterResponseCommand : IResponseMessageDataCommand<IModbusData>
    {
        public void Execute(IModbusData request, IModbusData response)
        {
            ModbusWriteSingleRegisterResponse res = response as ModbusWriteSingleRegisterResponse;

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Write Single Register Response:");
            Console.WriteLine("--------------------------------------------------------------");

            ModbusWriteSingleRegisterRequest req = request as ModbusWriteSingleRegisterRequest;

            Console.WriteLine(res.RegisterAddress + " address:" + res.RegisterValue);

            Console.WriteLine("-------------------------------");


        }
    }
}


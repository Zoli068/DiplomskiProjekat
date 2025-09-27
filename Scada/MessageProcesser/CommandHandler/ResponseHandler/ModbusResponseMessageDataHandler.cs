using Common.Command;
using Common.Message;
using Master.CommandHandler.ResponseCommands;
using Master.MessageProcesser;
using System;
using System.Collections.Generic;

namespace Master.CommandHandler
{
    /// <summary>
    /// Handles all the possible modbus responses
    /// </summary>
    public class ModbusResponseMessageDataHandler : IResponseMessageDataHandler
    {
        private readonly Dictionary<FunctionCode, IResponseMessageDataCommand<IModbusData>> commands;

        public ModbusResponseMessageDataHandler()
        {
            commands = new Dictionary<FunctionCode, IResponseMessageDataCommand<IModbusData>>()
            {
                {FunctionCode.ReadCoils,new ReadCoilsResponseCommand() },
                {FunctionCode.ReadDiscreteInputs,new ReadDiscreteResponseCommand() },
                {FunctionCode.ReadHoldingRegisters,new ReadHoldingRegistersResponseCommand() },
                {FunctionCode.ReadInputRegisters,new ReadInputRegistersResponseCommand() },
                {FunctionCode.WriteMultipleCoils,new WriteMultipleCoilsResponseCommand() },
                {FunctionCode.WriteMultipleRegisters,new WriteMultipleRegistersResponseCommand() },
                {FunctionCode.WriteSingleCoil, new WriteSingleCoilResponseCommand() },
                {FunctionCode.WriteSingleRegister,new WriteSingleRegisterResponseCommand() },
                {FunctionCode.MaskWriteRegister,new MaskWriteRegisterResponseCommand() },
                {FunctionCode.ReadFileRecord,new ReadFileRecordResponseCommand() },
                {FunctionCode.WriteFileRecord,new WriteFileRecordResponseCommand() },
                {FunctionCode.ReadWriteMultipleRegisters,new ReadWriteMultipleRegistersResponseCommand() },
                {FunctionCode.ReadFIFOQueue,new ReadFIFOQueueResponseCommand() },
            };
        }

        public void ProcessMessageData(IMessageData request, IMessageData response)
        {
            IResponseMessageDataCommand<IModbusData> command;

            if (((byte)(((IModbusPDU)response).FunctionCode) & 0x80) == 0)
            {
                if (commands.TryGetValue(((IModbusPDU)response).FunctionCode, out command))
                {
                    command.Execute(((IModbusPDU)request).Data, ((IModbusPDU)response).Data);
                }
            }
            else
            {
                HandleError(response);
            }
        }

        private void HandleError(IMessageData response)
        {
            ModbusPDU errorPdu = response as ModbusPDU;
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Error Happened:");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Function code error:" + errorPdu.FunctionCode);
            Console.WriteLine("Error Code:" + ((ModbusError)errorPdu.Data).ErrorCode);
            Console.WriteLine("Exception Code:" + ((ModbusError)errorPdu.Data).ExceptionCode);
            Console.WriteLine("--------------------------------------------------------------");
        }
    }
}

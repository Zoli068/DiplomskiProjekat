using Common.Message;
using Common.Utilities;
using Master.CommandHandler.MessageInitiateHandler;
using Master.MessageProcesser.MessageInitiateHandler;
using System;
using System.Collections.Generic;

namespace Master.CommandHandler
{
    /// <summary>
    /// Modbus Message Initiate Handler, this class from ModbusDTO object (from UI) will create modbus messages
    /// </summary>
    public class ModbusMessageInitiateHandler : IMessageInitiateHandler<FunctionCode>
    {
        private readonly Dictionary<FunctionCode, IMessageInitiateCommand<IMessageDTO, IModbusData>> messageInitiateCommands;
        private Action<IMessageData> sendMessage;

        public ModbusMessageInitiateHandler(Action<IMessageData> sendMessage)
        {
            messageInitiateCommands = new Dictionary<FunctionCode, IMessageInitiateCommand<IMessageDTO, IModbusData>>() {
                {FunctionCode.ReadCoils,new ModbusReadCoilsInitiateCommand() },
                {FunctionCode.ReadDiscreteInputs,new ModbusReadDiscreteInputsInitiateCommand() },
                {FunctionCode.ReadHoldingRegisters,new ModbusReadHoldingRegistersInitiateCommand()},
                {FunctionCode.ReadInputRegisters,new ModbusReadInputRegistersInitiateCommand() },
                {FunctionCode.WriteMultipleCoils,new ModbusWriteMultipleCoilsInitiateCommand()},
                {FunctionCode.WriteMultipleRegisters,new ModbusWriteMultipleRegistersInitiateCommand()},
                {FunctionCode.WriteSingleCoil,new ModbusWriteSingleCoilInitiateCommand()},
                {FunctionCode.WriteSingleRegister,new ModbusWriteSingleRegisterInitiateCommand()},
                {FunctionCode.MaskWriteRegister,new ModbusMaskWriteRegisterInitiateCommand()},
                {FunctionCode.ReadWriteMultipleRegisters,new ModbusReadWriteMultipleRegistersInitiateCommand() },
                {FunctionCode.ReadFileRecord,new ModbusReadFileRecordInitiateCommand()},
                {FunctionCode.WriteFileRecord,new ModbusWriteFileRecordInitiateCommand()},
                {FunctionCode.ReadFIFOQueue,new ModbusReadFIFOQueueInitiateCommand()},
            };
            this.sendMessage = sendMessage;
        }

        /// <summary>
        /// The method with we can initiate a message
        /// </summary>
        /// <param name="code">Function code for the message</param>
        /// <param name="modbusMessageDTO">The ModbusDTO object</param>
        public void InitiateMessage(FunctionCode code, IMessageDTO modbusMessageDTO)
        {
            IMessageInitiateCommand<IMessageDTO, IModbusData> message;
            IModbusData temp;

            if (messageInitiateCommands.TryGetValue(code, out message))
            {
                temp = message.InitiateMessage(modbusMessageDTO);
                ModbusPDU modbusPDU = new ModbusPDU();
                modbusPDU.FunctionCode = code;
                modbusPDU.Data = temp;
                sendMessage(modbusPDU);
            }
        }
    }
}

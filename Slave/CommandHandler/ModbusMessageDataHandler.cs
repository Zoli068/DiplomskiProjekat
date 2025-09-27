using Common.Command;
using Common.FIFOQueue;
using Common.FileRecord;
using Common.IPointsDataBase;
using Common.Message;
using Common.PointsDataBase;
using Slave.CommandHandler.Commands;
using System.Collections.Generic;

namespace Slave.CommandHandler
{
    /// <summary>
    /// Handles all the possible modbus requests 
    /// </summary>
    public class ModbusMessageDataHandler : IMessageDataHandler
    {
        private readonly Dictionary<FunctionCode, IMessageDataCommand<IModbusData>> commands;

        public ModbusMessageDataHandler(IPointsDataBase pointsDataBase,IFileRecord fileRecord,IFIFOQueue fIfoQueue)
        {
            commands = new Dictionary<FunctionCode, IMessageDataCommand<IModbusData>>()
            {
                {FunctionCode.ReadCoils , new ReadCoilsCommand(pointsDataBase)},
                {FunctionCode.ReadDiscreteInputs, new ReadDiscreteInputsCommand(pointsDataBase)},
                {FunctionCode.ReadHoldingRegisters,new ReadHoldingRegistersCommand(pointsDataBase)},
                {FunctionCode.ReadInputRegisters, new ReadInputRegistersCommand(pointsDataBase)},
                {FunctionCode.WriteMultipleCoils, new WriteMultipleCoilsCommand(pointsDataBase)},
                {FunctionCode.WriteMultipleRegisters, new WriteMultipleRegistersCommand(pointsDataBase)},
                {FunctionCode.WriteSingleCoil, new WriteSingleCoilCommand(pointsDataBase)},
                {FunctionCode.WriteSingleRegister, new WriteSingleRegisterCommand(pointsDataBase)},
                {FunctionCode.MaskWriteRegister,new MaskWriteRegisterCommand(pointsDataBase)},
                {FunctionCode.ReadWriteMultipleRegisters,new ReadWriteMultipleRegistersCommand(pointsDataBase)},
                {FunctionCode.ReadFileRecord,new ReadFileRecordCommand(fileRecord)},
                {FunctionCode.WriteFileRecord,new WriteFileRecordCommand(fileRecord)},
                {FunctionCode.ReadFIFOQueue,new ReadFIFOQueueCommand(fIfoQueue)},
            };
        }

        public IMessageData ProcessMessageData(IMessageData data)
        {
            IMessageDataCommand<IModbusData> command;

            if (commands.TryGetValue(((IModbusPDU)data).FunctionCode, out command))
            {

                try
                {
                    return CreateMessageData(command.Execute(((IModbusPDU)data).Data), ((IModbusPDU)data).FunctionCode);
                }
                catch (ValueOutOfIntervalException)
                {
                    return CreateErrorMessage(((IModbusPDU)data).FunctionCode, ExceptionCode.IllegalDataValue, 3);
                }
                catch (InvalidAddressException)
                {
                    return CreateErrorMessage(((IModbusPDU)data).FunctionCode, ExceptionCode.IllegalDataAddress, 2);
                }
                catch (PointTypeDifferenceException)
                {
                    return CreateErrorMessage(((IModbusPDU)data).FunctionCode, ExceptionCode.SlaveDeviceFailure, 4);
                }
            }
            else
            {
                return CreateErrorMessage(((IModbusPDU)data).FunctionCode, ExceptionCode.IllegalFunction, 1);
            }
        }

        private IMessageData CreateMessageData(IModbusData modbusData, FunctionCode functionCode)
        {
            ModbusPDU modbusPDU = new ModbusPDU();
            modbusPDU.FunctionCode = functionCode;
            modbusPDU.Data = modbusData;

            return modbusPDU;
        }

        private IMessageData CreateErrorMessage(FunctionCode code, ExceptionCode exceptionCode, byte errorCode)
        {
            byte errorFunctionCode = (byte)(((byte)code) + 0x80);
            ModbusPDU modbusPDU = new ModbusPDU();

            modbusPDU.FunctionCode = (FunctionCode)errorFunctionCode;                                                      
            modbusPDU.Data = new ModbusError(errorCode, exceptionCode);      

            return modbusPDU;
        }
    }
}

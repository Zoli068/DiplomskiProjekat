using System;
using System.Collections.Generic;
using System.Configuration;

namespace Common.Message
{
    /// <summary>
    /// Helper class for creating the right ModbusData for each function code
    /// For Slave side it will create a Request object
    /// For Master side it will create a Response object
    /// </summary>
    public static class ModbusFunctionFactory
    {
        public static Dictionary<FunctionCode, Func<IModbusData>> TypeMap;

        static ModbusFunctionFactory()
        {
            string mode = ConfigurationManager.AppSettings["ModbusMode"];

            if (mode == "Slave")
            {
                TypeMap = new Dictionary<FunctionCode, Func<IModbusData>>()
                {
                    { FunctionCode.ReadCoils, () => new ModbusReadCoilsRequest() },
                    { FunctionCode.ReadDiscreteInputs, () => new ModbusReadDiscreteInputsRequest() },
                    { FunctionCode.ReadHoldingRegisters, () => new ModbusReadHoldingRegistersRequest() },
                    { FunctionCode.ReadInputRegisters, () => new ModbusReadInputRegistersRequest() },
                    { FunctionCode.WriteSingleCoil, () => new ModbusWriteSingleCoilRequest() },
                    { FunctionCode.WriteSingleRegister, () => new ModbusWriteSingleRegisterRequest() },
                    { FunctionCode.WriteMultipleCoils, () => new ModbusWriteMultipleCoilsRequest() },
                    { FunctionCode.WriteMultipleRegisters, () => new ModbusWriteMultipleRegistersRequest() },
                    { FunctionCode.MaskWriteRegister, () => new ModbusMaskWriteRegisterRequest() },
                    { FunctionCode.ReadWriteMultipleRegisters, () =>new ModbusReadWriteMultipleRegistersRequest() },
                    { FunctionCode.ReadFileRecord, () =>new ModbusReadFileRecordRequest() },
                    { FunctionCode.WriteFileRecord, () =>new ModbusWriteFileRecordRequest() },
                    { FunctionCode.ReadFIFOQueue, () =>new ModbusReadFIFOQueueRequest() },
                };
            }
            else
            {
                TypeMap = new Dictionary<FunctionCode, Func<IModbusData>>()
                {
                    { FunctionCode.ReadCoils, () => new ModbusReadCoilsResponse() },
                    { FunctionCode.ReadDiscreteInputs, () => new ModbusReadDiscreteInputsResponse() },
                    { FunctionCode.ReadHoldingRegisters, () => new ModbusReadHoldingRegistersResponse() },
                    { FunctionCode.ReadInputRegisters, () => new ModbusReadInputRegistersResponse() },
                    { FunctionCode.WriteSingleCoil, () => new ModbusWriteSingleCoilResponse() },
                    { FunctionCode.WriteSingleRegister, () => new ModbusWriteSingleRegisterResponse() },
                    { FunctionCode.WriteMultipleCoils, () => new ModbusWriteMultipleCoilsResponse() },
                    { FunctionCode.WriteMultipleRegisters, () => new ModbusWriteMultipleRegistersResponse() },
                    { FunctionCode.MaskWriteRegister, () => new ModbusMaskWriteRegisterResponse() },
                    { FunctionCode.ReadWriteMultipleRegisters, () => new ModbusReadWriteMultipleRegistersResponse()},
                    { FunctionCode.ReadFileRecord, () =>new ModbusReadFileRecordResponse() },
                    { FunctionCode.WriteFileRecord, () =>new ModbusWriteFileRecordResponse() },
                    { FunctionCode.ReadFIFOQueue, () =>new ModbusReadFIFOQueueResponse() },
                };
            }
        }
    }
}

namespace Common.Message
{
    /// <summary>
    /// Enumeration of the possible function codes
    /// </summary>
    public enum FunctionCode : byte
    {
        ReadCoils = 0x01,
        ReadDiscreteInputs = 0x02,
        ReadHoldingRegisters = 0x03,
        ReadInputRegisters = 0x04,
        WriteSingleCoil = 0x05,
        WriteSingleRegister = 0x06,
        WriteMultipleCoils = 0x0F,
        WriteMultipleRegisters = 0x10,
        ReadFileRecord = 0x14,
        WriteFileRecord = 0x15,
        MaskWriteRegister = 0x16,
        ReadWriteMultipleRegisters=0x17,
        ReadFIFOQueue=0x18,
        
    }

    /// <summary>
    /// Enumeration of the possible exception codes
    /// </summary>
    public enum ExceptionCode : byte
    {
        IllegalFunction = 0x01,
        IllegalDataAddress = 0x02,
        IllegalDataValue = 0x03,
        SlaveDeviceFailure = 0x04,
        MemoryParityError = 0x08,
    }

}

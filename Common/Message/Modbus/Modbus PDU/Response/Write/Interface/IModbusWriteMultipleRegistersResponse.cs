namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Write Multiple Register Response attrbutes
    /// </summary>
    public interface IModbusWriteMultipleRegistersResponse : IModbusData
    {
        /// <summary>
        /// Address from we want to get the values
        /// </summary>
        ushort StartingAddress { get; set; }

        /// <summary>
        /// Indicates how many values we want to read 
        /// </summary>
        ushort QuantityOfRegisters { get; set; }
    }
}

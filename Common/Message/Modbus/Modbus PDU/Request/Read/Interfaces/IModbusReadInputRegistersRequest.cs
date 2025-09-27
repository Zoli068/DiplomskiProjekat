namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Read Input Registers Request attributes
    /// </summary>
    public interface IModbusReadInputRegistersRequest : IModbusData
    {
        /// <summary>
        /// Address from we want to get the values
        /// </summary>
        ushort StartingAddress { get; set; }

        /// <summary>
        /// Indicates how many values we want to read 
        /// </summary>
        ushort QuantityOfInputRegisters { get; set; }
    }
}

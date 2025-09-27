namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Read Holding Registers  Request attributes
    /// </summary>
    public interface IModbusReadHoldingRegistersRequest : IModbusPDUData
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
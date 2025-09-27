namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Write Multiple Registers Request attributes
    /// </summary>
    public interface IModbusWriteMultipleRegistersRequest : IModbusData
    {
        /// <summary>
        /// Address where we want to write the values
        /// </summary>
        ushort StartingAddress { get; set; }

        /// <summary>
        /// Indicates how many values we want to write
        /// </summary>
        ushort QuantityOfRegisters { get; set; }

        /// <summary>
        /// Indicates the number of bytes inside the <see cref="RegisterValue"/>
        /// </summary>
        byte ByteCount { get; set; }

        /// <summary>
        /// Contains the values that have to be written
        /// </summary>
        short[] RegisterValue { get; set; }
    }
}

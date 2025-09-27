namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Read Holding Registers attributes
    /// </summary>
    public interface IModbusReadHoldingRegistersResponse : IModbusData
    {
        /// <summary>
        /// Number of bytes contained inside the <see cref="RegisterValue"/>
        /// </summary>
        byte ByteCount { get; set; }

        /// <summary>
        /// Contains the readed Holding Registers values
        /// </summary>
        short[] RegisterValue { get; set; }
    }
}

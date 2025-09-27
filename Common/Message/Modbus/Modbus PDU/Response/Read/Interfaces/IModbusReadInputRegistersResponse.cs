namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Read Input Registers Response attributes
    /// </summary>
    public interface IModbusReadInputRegistersResponse : IModbusData
    {
        /// <summary>
        /// Number of bytes contained inside the <see cref="InputRegisters"/>
        /// </summary>
        byte Count { get; set; }

        /// <summary>
        /// Contains the readed Input Registers values
        /// </summary>
        short[] InputRegisters { get; set; }
    }
}

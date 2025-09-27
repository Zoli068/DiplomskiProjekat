namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Read Discrete Inputs Response attributes
    /// </summary>
    public interface IModbusReadDiscreteInputsResponse : IModbusData
    {
        /// <summary>
        /// Number of bytes contained inside the <see cref="InputStatus"/>
        /// </summary>
        byte ByteCount { get; set; }

        /// <summary>
        /// Contains the readed Discrete Inputs
        /// </summary>
        byte[] InputStatus { get; set; }
    }
}

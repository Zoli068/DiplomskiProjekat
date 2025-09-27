namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Read Coils Response attributes
    /// </summary>
    public interface IModbusReadCoilsResponse : IModbusData
    {
        /// <summary>
        /// Number of bytes contained inside the <see cref="CoilStatus"/>
        /// </summary>
        byte ByteCount { get; set; }

        /// <summary>
        /// Contains the readed Coils Values
        /// </summary>
        byte[] CoilStatus { get; set; }
    }
}

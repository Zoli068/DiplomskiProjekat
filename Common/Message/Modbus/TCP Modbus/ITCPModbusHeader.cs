namespace Common.Message
{
    /// <summary>
    /// Describes all the attributes that we can find inside a TCP Modbus Header
    /// </summary>
    public interface ITCPModbusHeader : IMessageHeader
    {
        /// <summary>
        /// Unique identificator for each modbus message
        /// </summary>
        ushort TransactionID { get; set; }

        /// <summary>
        /// Identificator for protocol, for modbus by default is 0
        /// </summary>
        ushort ProtocolID { get; set; }

        /// <summary>
        /// The number of bytes inside the Modbus Message
        /// </summary>
        ushort Length { get; set; }

        /// <summary>
        /// Unit Identificator, unused in TCP Modbus, by default 255 or 0
        /// </summary>
        byte UnitID { get; set; }
    }
}

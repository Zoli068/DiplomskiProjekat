namespace Common.Message
{
    /// <summary>
    /// The IMessageData for the Modbus Messages, Contains the Function Code and the Modbus Data
    /// </summary>
    public interface IModbusPDU : IMessageData
    {
        /// <summary>
        /// Indicates the type of the modbus message
        /// </summary>
        FunctionCode FunctionCode { get; set; }

        /// <summary>
        /// Contains the Data for the speicifed function code
        /// </summary>
        IModbusData Data { get; set; }
    }
}

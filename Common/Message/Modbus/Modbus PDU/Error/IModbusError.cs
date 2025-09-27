namespace Common.Message
{
    /// <summary>
    /// Describes a ModbusError which is the ModbusData for the ModbusPDU
    /// </summary>
    public interface IModbusError : IModbusData
    {
        /// <summary>
        /// The code of the error
        /// </summary>
        byte ErrorCode { get; set; }


        /// <summary>
        /// The exception code of the error
        /// </summary>
        ExceptionCode ExceptionCode { get; set; }
    }
}

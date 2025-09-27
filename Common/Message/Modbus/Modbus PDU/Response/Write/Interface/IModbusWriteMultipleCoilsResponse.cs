namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Write Multiple Coils Response attributes
    /// </summary>
    public interface IModbusWriteMultipleCoilsResponse : IModbusPDUData
    {
        /// <summary>
        /// Address where we wrote the values
        /// </summary>
        ushort StartingAddress { get; set; }

        /// <summary>
        /// Number of values that got written
        /// </summary>
        ushort QuantityOfOutputs { get; set; }
    }
}
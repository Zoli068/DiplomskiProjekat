namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Write Single Coil Response attributes
    /// </summary>
    public interface IModbusWriteSingleCoilResponse : IModbusData
    {
        /// <summary>
        /// Address where we wrote our value
        /// </summary>
        ushort OutputAddress { get; set; }

        /// <summary>
        /// The value that got written
        /// </summary>
        ushort OutputValue { get; set; }
    }
}

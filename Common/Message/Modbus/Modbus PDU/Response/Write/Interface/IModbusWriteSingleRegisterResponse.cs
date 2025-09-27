namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Write Single Register Response attributes
    /// </summary>
    public interface IModbusWriteSingleRegisterResponse : IModbusData
    {
        /// <summary>
        /// Address where we wrote our value
        /// </summary>
        ushort RegisterAddress { get; set; }

        /// <summary>
        /// The value that got written
        /// </summary>
        short RegisterValue { get; set; }
    }
}

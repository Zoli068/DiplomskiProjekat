namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Write Single Coil Request attributes
    /// </summary>
    public interface IModbusWriteSingleCoilRequest : IModbusData
    {
        /// <summary>
        /// Address where we want to write the value
        /// </summary>
        ushort OutputAddress { get; set; }

        /// <summary>
        /// Contains the value that have to be written
        /// </summary>
        ushort OutputValue { get; set; }
    }
}

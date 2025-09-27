namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Write Single Register Request attributes
    /// </summary>
    public interface IModbusWriteSingleRegisterRequest : IModbusData
    {
        /// <summary>
        /// Address where we want to write the value
        /// </summary>
        ushort RegisterAddress { get; set; }

        /// <summary>
        /// Contains the value that have to be written
        /// </summary>
        short RegisterValue { get; set; }
    }
}

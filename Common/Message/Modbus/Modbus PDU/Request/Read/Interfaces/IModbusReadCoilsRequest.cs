namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Read Coils Request attributes
    /// </summary>
    public interface IModbusReadCoilsRequest : IModbusData
    {
        /// <summary>
        /// Address from we want to get the values
        /// </summary>
        ushort StartingAddress { get; set; }

        /// <summary>
        /// Indicates how many values we want to read 
        /// </summary>
        ushort QuantityOfCoils { get; set; }
    }
}

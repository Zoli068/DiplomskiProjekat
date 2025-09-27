namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Read Discrete Inputs attributes
    /// </summary>
    public interface IModbusReadDiscreteInputsRequest : IModbusData
    {
        /// <summary>
        /// Address from we want to get the values
        /// </summary>
        ushort StartingAddress { get; set; }

        /// <summary>
        /// Indicates how many values we want to read
        /// </summary>
        ushort QuantityOfInputs { get; set; }
    }
}

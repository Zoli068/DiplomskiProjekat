namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Write Multiple Coils Request attributes
    /// </summary>
    public interface IModbusWriteMultipleCoilsRequest : IModbusData
    {
        /// <summary>
        /// Address where we want to write the values
        /// </summary>
        ushort StartingAddress { get; set; }

        /// <summary>
        /// Indicates how many values we want to write
        /// </summary>
        ushort QuantityOfOutputs { get; set; }

        /// <summary>
        /// Indicates the number of bytes inside the <see cref="OutputsValue"/>
        /// </summary>
        byte ByteCount { get; set; }

        /// <summary>
        /// Contains the values that have to be written
        /// </summary>
        byte[] OutputsValue { get; set; }
    }
}

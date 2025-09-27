namespace Common.Message.Modbus
{
    /// <summary>
    /// Describes a Modbus Mask Write Register Response attributes
    /// </summary>
    public interface IModbusMaskWriteRegisterResponse:IModbusPDUData
    {
        /// <summary>
        /// Address where we want to write
        /// </summary>
        ushort ReferenceAddress { get; set; }

        /// <summary>
        /// The AND Mask which will be use for the write
        /// </summary>
        ushort AndMask { get; set; }

        /// <summary>
        /// The OR Mask which will be use for the write
        /// </summary>
        ushort OrMask { get; set; }
    }
}
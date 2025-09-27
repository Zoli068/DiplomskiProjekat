using Common.Utilities;

namespace Master.CommandHandler.MessageInitiateHandler
{
    /// <summary>
    /// DTO object for Write Single Modbus, this object is sent from UI layer to the Message layer
    /// Contains just the most important values for a request
    /// </summary>
    public class InitiateWriteSingleModbusDTO : IMessageDTO
    {
        /// <summary>
        /// Address where to write
        /// </summary>
        public ushort Address { get; set; }

        /// <summary>
        /// Value that will be written
        /// </summary>
        public short Value { get; set; }
    }
}
